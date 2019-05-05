using System.Collections.Generic;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Domain.Services
{
    public interface IStickerService
    {
        Sticker Get(int id);
        IEnumerable<Sticker> GetAll();
        bool Add(Sticker sticker);
        bool AddRange(IEnumerable<Sticker> stickers);
        bool Update(Sticker sticker);
        bool Delete(Sticker sticker);
    }
}