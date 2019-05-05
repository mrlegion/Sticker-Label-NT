using System.Data.Entity;

namespace StickerLib.DAL.DbScope.Interfaces
{
    public interface IAmbientDbContextLocator
    {
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}