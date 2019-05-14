using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StickerLib.Domain.Common;
using StickerLib.Domain.Helpers;
using StickerLib.Infrastructure;
using Config = StickerLib.Infrastructure.Common.Properties;

namespace StickerLib.Domain.Build.Interfaces
{
    public interface IStickerBuilder
    {
        void DublicatedPages();
        void ShufflePages();
        void NupPages();
        void SafeDocument();

        void SetGroup(Group group);
    }

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
            int remainded = dublicated.Length % Config.GetInstance().Groups;
            _groupDao.DublicatedPages = remainded != 0
                ? _pageManager.AddEmptyPages(dublicated, Config.GetInstance().Groups - remainded)
                : dublicated;
        }

        public async void DublicatedPagesAsync()
        {
            var dublicated = await _pageManager.DuplicatePageAsync(_groupDao.SourcePages, _groupDao.Group.Count);
            int remainded = dublicated.Length % Config.GetInstance().Groups;
            _groupDao.DublicatedPages = remainded != 0
                ? await _pageManager.AddEmptyPagesAsync(dublicated, Config.GetInstance().Groups - remainded)
                : dublicated;
        }

        public void ShufflePages()
        {
            var shuffles = _shuffler.Shuffle(_groupDao.SourcePages, Config.GetInstance().Groups, Config.GetInstance(), )
        }

        public void NupPages()
        {
            throw new System.NotImplementedException();
        }

        public void SafeDocument()
        {
            throw new System.NotImplementedException();
        }

        public void SetGroup(Group group)
        {
            _groupDao = new GroupDao(group);
        }
    }

    public class Creator
    {
        private readonly IStickerBuilder _builder;

        public Creator(IStickerBuilder builder)
        {
            _builder = builder;
        }

        public void Create(IEnumerable<Group> groups)
        {
            var groupList = groups.ToList();
            foreach (Group group in groupList)
            {
                _builder.SetGroup(group);

                _builder.DublicatedPages();
                _builder.ShufflePages();
                _builder.NupPages();
                _builder.SafeDocument();
            }
        }
    }

    internal class GroupDao
    {
        private Group _group;
        public string File { get; set; }

        public Group Group
        {
            get => _group;
            set
            {
                _group = value;
                if (SourcePages.Length != _group.StickerCountInGroup)
                    GenerateSourcePagesArray();
            }
        }

        private void GenerateSourcePagesArray()
        {
            SourcePages = new int[_group.StickerCountInGroup];
            for (int i = 0; i < _group.StickerCountInGroup; i++)
                SourcePages[i] = i;
        }

        public int[] SourcePages { get; private set; }
        public int[] DublicatedPages { get; set; }
        public int[] SufflePages { get; set; }
        public Queue<Page> NupPages { get; set; }

        public GroupDao(Group group)
        {
            Group = group;
            File = Path.Combine(Config.GetInstance().DirectoryForSaving, Group.Count + ".pdf");
        }
    }
}