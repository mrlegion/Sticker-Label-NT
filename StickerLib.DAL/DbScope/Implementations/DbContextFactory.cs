using System;
using System.Data.Entity;
using StickerLib.DAL.DbScope.Interfaces;

namespace StickerLib.DAL.DbScope.Implementations
{
    public class DbContextFactory : IDbContextFactory
    {
        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return Activator.CreateInstance<TDbContext>();
        }
    }
}