using System;
using System.Collections.Generic;
using System.Linq;
using StickerLib.Infrastructure;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Domain.Services
{
    public class GroupService : IGroupService
    {
        private readonly List<Group> _groups;

        public GroupService()
        {
            _groups = new List<Group>();
        }

        public Group Get(int count)
        {
            return _groups.FirstOrDefault(g => g.Count == count);
        }

        public IEnumerable<Group> GetAll()
        {
            return new List<Group>(_groups);
        }

        public void Add(Group group)
        {
            if (_groups.Contains(group))
            {
                int index = _groups.IndexOf(group);
                var stickers = _groups[index].Stickers.ToList();
                foreach (Sticker sticker in stickers)
                    if (stickers.Contains(sticker)) continue;
                    else stickers.Add(sticker);
                _groups[index].Stickers = stickers;
                return;
            }

            _groups.Add(group);
        }

        public void Update(Group group)
        {
            if (_groups.Contains(group))
            {
                int index = _groups.IndexOf(group);
                _groups[index] = group;
            }

            throw new ArgumentException("Cannot found select group in collection!", nameof(group));
        }

        public void Remove(Group group)
        {
            _groups.Remove(group);
        }
    }
}