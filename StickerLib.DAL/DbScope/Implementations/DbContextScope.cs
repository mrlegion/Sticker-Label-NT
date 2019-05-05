using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using StickerLib.DAL.DbScope.Enums;
using StickerLib.DAL.DbScope.Interfaces;

namespace StickerLib.DAL.DbScope.Implementations
{
    public class DbContextScope : IDbContextScope
    {
        private bool _disposed;
        private bool _readOnly;
        private bool _completed;
        private bool _nested;
        private DbContextScope _parentScope;
        private DbContextCollection _dbContexts;


        public IDbContextCollection DbContexts => _dbContexts;

        public DbContextScope(IDbContextFactory dbContextFactory = null) :
            this(DbContextScopeOption.JoinExisting, false, null, dbContextFactory)
        {}

        public DbContextScope(bool readOnly, IDbContextFactory dbContextFactory = null) :
            this(DbContextScopeOption.JoinExisting, readOnly, null, dbContextFactory)
        {}

        public DbContextScope(DbContextScopeOption joiningOption, bool readOnly, IsolationLevel? isolationLevel, IDbContextFactory dbContextFactory = null)
        {
            if (isolationLevel.HasValue && joiningOption == DbContextScopeOption.JoinExisting)
                throw new ArgumentException("Cannot join an ambient DbContextScope when an explicit database transaction is required. When requiring explicit database transactions to be used (i.e. when the 'isolationLevel' parameter is set), you must not also ask to join the ambient context (i.e. the 'joinAmbient' parameter must be set to false).");

            _disposed = false;
            _completed = false;
            _readOnly = readOnly;

            _parentScope = GetAmbientScope();

            if (_parentScope != null && joiningOption == DbContextScopeOption.JoinExisting)
            {
                if (_parentScope._readOnly && !this._readOnly)
                    throw new InvalidOperationException("Cannot nest a read/write DbContextScope within a read-only DbContextScope.");

                _nested = true;
                _dbContexts = _parentScope._dbContexts;
            }
            else
            {
                _nested = false;
                _dbContexts = new DbContextCollection(readOnly, isolationLevel, dbContextFactory);
            }

            SetAmbientScope(this);
        }

        public int SaveChanges()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextScope));
            if (_completed)
                throw new InvalidOperationException("You cannot call SaveChanges() more than once on a DbContextScope. A DbContextScope is meant to encapsulate a business transaction: create the scope at the start of the business transaction and then call SaveChanges() at the end. Calling SaveChanges() mid-way through a business transaction doesn't make sense and most likely mean that you should refactor your service method into two separate service method that each create their own DbContextScope and each implement a single business transaction.");
            var c = 0;
            if (!_nested)
                c = CommitInternal();
            _completed = true;
            return c;
        }

        public Task<int> SaveChangeAsync()
        {
            return SaveChangeAsync(CancellationToken.None);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null) throw new ArgumentNullException(nameof(cancelToken));
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextScope));
            if (_completed)
                throw new InvalidOperationException("You cannot call SaveChanges() more than once on a DbContextScope. A DbContextScope is meant to encapsulate a business transaction: create the scope at the start of the business transaction and then call SaveChanges() at the end. Calling SaveChanges() mid-way through a business transaction doesn't make sense and most likely mean that you should refactor your service method into two separate service method that each create their own DbContextScope and each implement a single business transaction.");
            var c = 0;
            if (!_nested)
                c = await CommitInternalAsync(cancelToken).ConfigureAwait(false);
            _completed = true;
            return c;
        }

        public void RefreshEntitiesInParentScope(IEnumerable entities)
        {
            if (entities == null) return;
            if (_parentScope == null) return;
            if (_nested) return;

            foreach(IObjectContextAdapter contextInCurrentScope in _dbContexts.InitializedDbContexts.Values)
            {
                var correspondingParentContext =
                    _parentScope._dbContexts.InitializedDbContexts.Values.SingleOrDefault(parentContent =>
                        parentContent.GetType() == contextInCurrentScope.GetType()) as IObjectContextAdapter;
                if (correspondingParentContext == null) continue;

                foreach (var toRefresh in entities)
                {
                    if (contextInCurrentScope.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toRefresh,
                        out var stateInCurrentScope))
                    {
                        var key = stateInCurrentScope.EntityKey;
                        if (correspondingParentContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(key, out var stateInParentScope))
                            if (stateInParentScope.State == EntityState.Unchanged)
                                correspondingParentContext.ObjectContext.Refresh(RefreshMode.StoreWins, stateInParentScope.Entity);
                    }
                }
            }
        }

        public async Task RefreshEntitiesInParentScopeAsync(IEnumerable entities)
        {

            if (entities == null) return;
            if (_parentScope == null) return;
            if (_nested) return;

            foreach (IObjectContextAdapter contextInCurrentScope in _dbContexts.InitializedDbContexts.Values)
            {
                var correspondingParentContext =
                    _parentScope._dbContexts.InitializedDbContexts.Values.SingleOrDefault(parentContent =>
                        parentContent.GetType() == contextInCurrentScope.GetType()) as IObjectContextAdapter;
                if (correspondingParentContext == null) continue;

                foreach (var toRefresh in entities)
                {
                    if (contextInCurrentScope.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(toRefresh,
                        out var stateInCurrentScope))
                    {
                        var key = stateInCurrentScope.EntityKey;
                        if (correspondingParentContext.ObjectContext.ObjectStateManager.TryGetObjectStateEntry(key, out var stateInParentScope))
                            if (stateInParentScope.State == EntityState.Unchanged)
                                await correspondingParentContext.ObjectContext.RefreshAsync(RefreshMode.StoreWins, stateInParentScope.Entity);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            if (!_nested)
            {
                if (!_completed)
                {
                    try
                    {
                        if (_readOnly) CommitInternal();
                        else RollbackInternal();
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }

                    _completed = true;
                }

                _dbContexts.Dispose();
            }

            var currentAmbientScope = GetAmbientScope();
            if (currentAmbientScope != this)
                throw new InvalidOperationException("DbContextScope instances must be disposed of in the order in which they were created!");

            RemoveAmbientScope();

            if (_parentScope != null)
            {
                if (_parentScope._disposed)
                {
                    var message = @"PROGRAMMING ERROR - When attempting to dispose a DbContextScope, we found that our parent DbContextScope has already been disposed! This means that someone started a parallel flow of execution (e.g. created a TPL task, created a thread or enqueued a work item on the ThreadPool) within the context of a DbContextScope without suppressing the ambient context first. 

In order to fix this:
1) Look at the stack trace below - this is the stack trace of the parallel task in question.
2) Find out where this parallel task was created.
3) Change the code so that the ambient context is suppressed before the parallel task is created. You can do this with IDbContextScopeFactory.SuppressAmbientContext() (wrap the parallel task creation code block in this). 

Stack Trace:
" + Environment.StackTrace;

                    System.Diagnostics.Debug.WriteLine(message);
                }
                else
                {
                    SetAmbientScope(_parentScope);
                }
            }

            _disposed = true;
        }

        private int CommitInternal() => _dbContexts.Commit();

        private Task<int> CommitInternalAsync(CancellationToken cancelToken) => _dbContexts.CommitAsync(cancelToken);

        private void RollbackInternal() => _dbContexts.Rollback();

        #region Ambient Logic

        private static readonly string AmbientDbContextScopeKey = "AmbientDbContext_" + Guid.NewGuid();
        private static readonly ConditionalWeakTable<InstanceIdentifier, DbContextScope> DbContextScopeInstance = new ConditionalWeakTable<InstanceIdentifier, DbContextScope>();
        private InstanceIdentifier _instanceIdentifier = new InstanceIdentifier();

        internal static void SetAmbientScope(DbContextScope newAmbientScope)
        {
            if (newAmbientScope == null) throw new ArgumentNullException(nameof(newAmbientScope));
            var current = CallContext.LogicalGetData(AmbientDbContextScopeKey) as InstanceIdentifier;
            if (current == newAmbientScope._instanceIdentifier) return;

            CallContext.LogicalSetData(AmbientDbContextScopeKey, newAmbientScope._instanceIdentifier);
            DbContextScopeInstance.GetValue(newAmbientScope._instanceIdentifier, key => newAmbientScope);
        }

        internal static void RemoveAmbientScope()
        {
            var current = CallContext.LogicalGetData(AmbientDbContextScopeKey) as InstanceIdentifier;
            CallContext.LogicalSetData(AmbientDbContextScopeKey, null);

            if (current != null) DbContextScopeInstance.Remove(current);
        }

        internal static void HideAmbientScope()
        {
            CallContext.LogicalSetData(AmbientDbContextScopeKey, null);
        }

        internal static DbContextScope GetAmbientScope()
        {
            var instanceIdentifier = CallContext.LogicalGetData(AmbientDbContextScopeKey) as InstanceIdentifier;
            if (instanceIdentifier == null) return null;

            if (DbContextScopeInstance.TryGetValue(instanceIdentifier, out var ambientScope))
                return ambientScope;

            System.Diagnostics.Debug.WriteLine("Programming error detected. Found a reference to an ambient DbContextScope in the CallContext but didn't have an instance for it in our DbContextScopeInstances table. This most likely means that this DbContextScope instance wasn't disposed of properly. DbContextScope instance must always be disposed. Review the code for any DbContextScope instance used outside of a 'using' block and fix it so that all DbContextScope instances are disposed of.");
            return null;
        }


        #endregion

        internal class InstanceIdentifier: MarshalByRefObject { }
    }
}