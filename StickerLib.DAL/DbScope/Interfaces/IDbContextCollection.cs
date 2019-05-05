using System;
using System.Data.Entity;

namespace StickerLib.DAL.DbScope.Interfaces
{
    public interface IDbContextCollection : IDisposable
    {
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}