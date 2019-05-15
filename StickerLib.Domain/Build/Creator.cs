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
}