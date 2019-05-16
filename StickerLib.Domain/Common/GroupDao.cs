using System.Collections.Generic;
using System.IO;
using StickerLib.Infrastructure;

namespace StickerLib.Domain.Common
{
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
                if (SourcePages == null) GenerateSourcePagesArray();
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
            File = Path.Combine(Infrastructure.Common.Properties.GetInstance().DirectoryForSaving, Group.Count + ".pdf");
        }
    }
}