using System.Data.Entity;

namespace StickerLib.DAL.DbScope.Interfaces
{
    public interface IDbContextFactory
    {
        TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext;
    }
}