using System.Collections.Generic;
using System.Linq;
using StickerLib.Domain.Build.Interfaces;
using StickerLib.Infrastructure;

namespace StickerLib.Domain.Build
{
    public class Creator
    {
        private readonly IStickerBuilder _builder;

        public Creator(IStickerBuilder builder)
        {
            _builder = builder;
        }

        public void Create(IEnumerable<Group> groups, bool saveEachFile)
        {
            var groupList = groups.ToList();
            foreach (var group in groupList)
                Create(group, saveEachFile);
        }

        public void Create(IEnumerable<Group> groups)
        {
            Create(groups, false);
        }

        public void Create(Group group)
        {
            Create(group, false);
        }

        public void Create(Group group, bool saveEachFile)
        {
            _builder.SetGroup(group);

            _builder.DublicatedPages();
            _builder.ShufflePages();
            _builder.NupPages();
            _builder.SafeDocument(saveEachFile);
        }
    }
}