﻿using StickerLib.Infrastructure;

namespace StickerLib.Domain.Build.Interfaces
{
    public interface IStickerBuilder
    {
        void DublicatedPages();
        void ShufflePages();
        void NupPages();
        void SafeDocument(bool saveEachFile);

        void SetGroup(Group group);
    }
}