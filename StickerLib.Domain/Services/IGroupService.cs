using System.Collections.Generic;
using StickerLib.Infrastructure;

namespace StickerLib.Domain.Services
{
    public interface IGroupService
    {
        Group Get(int count);
        IEnumerable<Group> GetAll();
        void Add(Group group);
        void Update(Group group);
        void Remove(Group group);
    }
}