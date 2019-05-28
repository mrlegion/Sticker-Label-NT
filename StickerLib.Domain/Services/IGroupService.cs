using System.Collections.Generic;
using System.Collections.ObjectModel;
using StickerLib.Infrastructure;

namespace StickerLib.Domain.Services
{
    public interface IGroupService
    {
        ObservableCollection<Group> Groups { get; }
        Group Get(int count);
        IEnumerable<Group> GetAll();
        void Add(Group group);
        void Update(Group group);
        void Remove(Group group);
        void Clear();
    }
}