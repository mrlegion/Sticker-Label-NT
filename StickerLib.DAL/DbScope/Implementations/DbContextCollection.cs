using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using StickerLib.DAL.DbScope.Interfaces;

namespace StickerLib.DAL.DbScope.Implementations
{
    public class DbContextCollection : IDbContextCollection
    {
        private Dictionary<Type, DbContext> _initializedDbContexts;
        private Dictionary<DbContext, DbContextTransaction> _transactions;
        private IsolationLevel? _isolationLevel;
        private readonly IDbContextFactory _dbContextFactory;
        private bool _disposed;
        private bool _completed;
        private bool _readOnly;

        internal Dictionary<Type, DbContext> InitializedDbContexts => _initializedDbContexts;

        public DbContextCollection(bool readOnly, IsolationLevel? isolationLevel, IDbContextFactory dbContextFactory = null)
        {
            _disposed = false;
            _completed = false;

            _initializedDbContexts = new Dictionary<Type, DbContext>();
            _transactions = new Dictionary<DbContext, DbContextTransaction>();

            _readOnly = readOnly;
            _isolationLevel = isolationLevel;
            _dbContextFactory = dbContextFactory;
        }

        public void Dispose()
        {
            if (_disposed) return;
            if (!_completed)
            {
                try
                {
                    if (_readOnly) Commit();
                    else Rollback();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    dbContext.Dispose();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            _initializedDbContexts.Clear();
            _disposed = true;
        }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));
            var requestedType = typeof(TDbContext);
            if (!_initializedDbContexts.ContainsKey(requestedType))
            {
                var dbContext = _dbContextFactory != null
                    ? _dbContextFactory.CreateDbContext<TDbContext>()
                    : Activator.CreateInstance<TDbContext>();

                _initializedDbContexts.Add(requestedType, dbContext);
                if (_readOnly)
                    dbContext.Configuration.AutoDetectChangesEnabled = false;
                if (_isolationLevel.HasValue)
                {
                    var transaction = dbContext.Database.BeginTransaction(_isolationLevel.Value);
                    _transactions.Add(dbContext, transaction);
                }
            }

            return _initializedDbContexts[requestedType] as TDbContext;
        }

        public int Commit()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            ExceptionDispatchInfo lastError = null;
            var c = 0;
            foreach (DbContext dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    if (!_readOnly)
                        c += dbContext.SaveChanges();
                    DbContextTransaction transition = GetValueOrDefault(_transactions, dbContext);
                    if (transition != null)
                    {
                        transition.Commit();
                        transition.Dispose();
                    }
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            _transactions.Clear();
            _completed = true;

            if (lastError != null) lastError.Throw();

            return c;
        }

        public Task<int> CommitAsync() => CommitAsync(CancellationToken.None);

        public async Task<int> CommitAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null) throw new ArgumentNullException(nameof(cancelToken));
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            ExceptionDispatchInfo lastError = null;
            var c = 0;
            foreach (DbContext dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    if (_readOnly)
                        c += await dbContext.SaveChangesAsync(cancelToken).ConfigureAwait(false);
                    DbContextTransaction transition = GetValueOrDefault(_transactions, dbContext);
                    if (transition != null)
                    {
                        transition.Commit();
                        transition.Dispose();
                    }
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            _transactions.Clear();
            _completed = true;

            if (lastError != null) lastError.Throw();

            return c;

        }

        public void Rollback()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DbContextCollection));
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            ExceptionDispatchInfo lastError = null;
            foreach (var dbContext in _initializedDbContexts.Values)
            {
                DbContextTransaction transition = GetValueOrDefault(_transactions, dbContext);
                if (transition != null)
                {
                    try
                    {
                        transition.Rollback();
                        transition.Dispose();
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                }
            }

            _transactions.Clear();
            _completed = true;

            if (lastError != null) lastError.Throw();
        }

        private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default(TValue);
        }
    }
}