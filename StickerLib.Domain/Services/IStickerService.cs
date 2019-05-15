using System.Collections.Generic;
using System.Threading.Tasks;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Domain.Services
{
    public interface IStickerService
    {
        Sticker Get(int id);
        IEnumerable<Sticker> GetAll();
        Task<IEnumerable<Sticker>> GetAllAsync();
        bool Add(Sticker sticker);
        bool AddRange(IEnumerable<Sticker> stickers);
        bool Update(Sticker sticker);
        bool Delete(Sticker sticker);
    }
}