using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using StickerLib.DAL.DbScope.Interfaces;
using StickerLib.DAL.Repository;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Domain.Services
{
    public class StickerService : IStickerService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Sticker> _repository;

        public StickerService(IDbContextScopeFactory dbContextScopeFactory, IRepository<Sticker> repository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException(nameof(dbContextScopeFactory));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Sticker Get(int id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
                return _repository.Get(id);
        }

        public IEnumerable<Sticker> GetAll()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
                return _repository.GetAll();
        }

        public Task<IEnumerable<Sticker>> GetAllAsync()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
                return _repository.GetAllAsync();
        }

        public bool Add(Sticker sticker)
        {
            bool result = false;
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                result = _repository.Add(sticker);
                Log.Debug("Start save chages in DbContext");
                dbContextScope.SaveChanges();
            }

            return result;
        }

        public bool AddRange(IEnumerable<Sticker> stickers)
        {
            bool result = false;
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                result = _repository.AddRange(stickers);
                Log.Debug("Start save chages in DbContext");
                dbContextScope.SaveChanges();
            }

            return result;
        }

        public bool Update(Sticker sticker)
        {
            bool result = false;
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                result = _repository.Update(sticker);
                Log.Debug("Start save chages in DbContext");
                dbContextScope.SaveChanges();
            }

            return result;
        }

        public bool Delete(Sticker sticker)
        {
            bool result = false;
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                result = _repository.Delete(sticker);
                Log.Debug("Start save chages in DbContext");
                dbContextScope.SaveChanges();
            }

            return result;
        }
    }
}