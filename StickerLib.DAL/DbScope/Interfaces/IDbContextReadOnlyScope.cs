using System;

namespace StickerLib.DAL.DbScope.Interfaces
{
    public interface IDbContextReadOnlyScope : IDisposable
    {
        IDbContextCollection DbContexts { get; }
    }
}