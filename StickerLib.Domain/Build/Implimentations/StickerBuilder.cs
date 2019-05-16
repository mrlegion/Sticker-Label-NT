using StickerLib.Domain.Build.Interfaces;
using StickerLib.Domain.Common;
using StickerLib.Domain.Helpers;
using StickerLib.Infrastructure;

namespace StickerLib.Domain.Build.Implimentations
{
    internal class StickerBuilder : IStickerBuilder
    {
        private GroupDao _groupDao;

        private readonly INuper _nuper;
        private readonly IShuffler _shuffler;
        private readonly IPageManager _pageManager;

        public StickerBuilder(INuper nuper, IShuffler shuffler, IPageManager pageManager)
        {
            _nuper = nuper;
            _shuffler = shuffler;
            _pageManager = pageManager;
        }

        public void DublicatedPages()
        {
            var dublicated = _pageManager.DublicatePages(_groupDao.SourcePages, _groupDao.Group.Count);
            int remainded = dublicated.Length % Infrastructure.Common.Properties.GetInstance().Groups;
            _groupDao.DublicatedPages = remainded != 0
                ? _pageManager.AddEmptyPages(dublicated, Infrastructure.Common.Properties.GetInstance().Groups - remainded)
                : dublicated;
        }

        public async void DublicatedPagesAsync()
        {
            var dublicated = await _pageManager.DuplicatePageAsync(_groupDao.SourcePages, _groupDao.Group.Count);
            int remainded = dublicated.Length % Infrastructure.Common.Properties.GetInstance().Groups;
            _groupDao.DublicatedPages = remainded != 0
                ? await _pageManager.AddEmptyPagesAsync(dublicated, Infrastructure.Common.Properties.GetInstance().Groups - remainded)
                : dublicated;
        }

        public void ShufflePages()
        {
            _groupDao.SufflePages = _shuffler.Shuffle(_groupDao.DublicatedPages, Infrastructure.Common.Properties.GetInstance().Groups);
        }

        public void NupPages()
        {
            _groupDao.NupPages = _nuper.Nup(_groupDao.SufflePages);
        }

        public void SafeDocument(bool saveEachFile)
        {
            PdfWriteHelper.WriteToFileWithNup(_groupDao.Group.Stickers, _groupDao.NupPages, _groupDao.File);
        }

        public void SetGroup(Group group)
        {
            _groupDao = new GroupDao(group);
        }
    }
}