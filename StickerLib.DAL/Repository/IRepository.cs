using System.Collections.Generic;
using System.Threading.Tasks;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.DAL.Repository
{
    public interface IRepository<T>
    {
        T Get(int id);

        IEnumerable<T> GetAll();

        bool Add(T entity);

        bool AddRange(IEnumerable<T> entities);

        bool Update(T entity);

        bool Delete(T entity);
        Task<IEnumerable<Sticker>> GetAllAsync();
    }
}