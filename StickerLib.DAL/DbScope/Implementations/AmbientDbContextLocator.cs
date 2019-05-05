using System.Data.Entity;
using StickerLib.DAL.DbScope.Interfaces;

namespace StickerLib.DAL.DbScope.Implementations
{
    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();
            return ambientDbContextScope == null
                ? null
                : ambientDbContextScope.DbContexts.Get<TDbContext>();
        }
    }
}