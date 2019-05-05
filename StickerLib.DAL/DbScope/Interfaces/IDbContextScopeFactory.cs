﻿using System;
using System.Data;
using StickerLib.DAL.DbScope.Enums;

namespace StickerLib.DAL.DbScope.Interfaces
{
    public interface IDbContextScopeFactory
    {
        IDbContextScope Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting);
        IDbContextReadOnlyScope CreateReadOnly(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting);
        IDbContextScope CreateWithTransaction(IsolationLevel isolationLevel);
        IDbContextReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel);
        IDisposable SuppressAmbientContext();
    }
}