using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using StickerLib.DAL.DbContexts;
using StickerLib.DAL.DbScope.Interfaces;
using StickerLib.Infrastructure.Annotations;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.DAL.Repository
{
    internal class StickerRepository : IRepository<Sticker>
    {
        [NotNull]
        private readonly IAmbientDbContextLocator _locator;

        public StickerRepository([NotNull] IAmbientDbContextLocator locator)
        {
            _locator = locator ?? throw new ArgumentNullException(nameof(locator));
        }

        private StickerDbContext DbContext
        {
            get
            {
                Log.Debug("Get StickerDbContext object");
                var context = _locator.Get<StickerDbContext>();
                if (context == null)
                {
                    string errorMessage =
                        "No ambient DbContext of type StickerDbContext found. " + 
                        "This means that this repository method has been called outside of the scope of a DbContextScope. " +
                        "A repository must only be accessed within the scope of a DbContextScope, which takes care of creating " +
                        "the DbContext instances that the repositories need and making them available as ambient contexts. " +
                        "This is what ensures that, for any given DbContext-derived type, the same instance is used throughout " +
                        "the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level " +
                        "business logic service method to create a DbContextScope that wraps the entire business transaction that your " +
                        "service method implements. Then access this repository within that scope. Refer to the comments " +
                        "in the IDbContextScope.cs file for more details.";
                    Log.Fatal(errorMessage);
                    throw new InvalidOperationException(errorMessage);
                }
                return context;
            }
        }

        public Sticker Get(int id)
        {
            if (id < 0)
            {
                Log.Error("Id can not be less zero! ID => {@id}", id);
                return null;
            }
            Log.Debug("Returned selected entity in Stickers by Id ({@id})", id);
            return DbContext.Stickers.FirstOrDefault(sticker => sticker.Id == id);
        }

        public IEnumerable<Sticker> GetAll()
        {
            Log.Debug("Returned all entity in Stickers");
            return DbContext.Stickers.ToList();
        }

        public bool Add(Sticker entity)
        {
            if (entity == null)
            {
                Log.Error("Cannot adding empty entity to database. Please check sending entity to method Add().");
                return false;
            }

            Log.Debug("Added new sticker into Database. Sticker name : {@name}", entity.Name);
            return DbContext.Stickers.Add(entity) != null;
        }

        public bool AddRange(IEnumerable<Sticker> entities)
        {
            var list = entities.ToList();
            if (!list.Any())
            {
                Log.Error("Adding list ( {@name} ) cannot be empty. Check adding list parameter in method AddRange().", nameof(entities));
                return false;
            }
            Log.Debug("Adding stickers into Database");
            return DbContext.Stickers.AddRange(list).Any();
        }

        public bool Update(Sticker entity)
        {
            if (entity == null)
            {
                Log.Error("Sended entity cannot be NULL. Please check sending entity to method Update().");
                return false;
            }
            Log.Debug("Update entity: {@name}", entity.Name);
            DbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public bool Delete(Sticker entity)
        {
            if (entity == null)
            {
                Log.Error("Sended entity cannot be NULL. Please check sending entity to method Delete().");
                return false;
            }
            Log.Debug("Deleted entity: {@name}", entity.Name);
            DbContext.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public Task<IEnumerable<Sticker>> GetAllAsync()
        {
            return Task<IEnumerable<Sticker>>.Factory.StartNew(() => DbContext.Stickers.ToList());
        }
    }
}