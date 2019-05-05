using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StickerLib.Infrastructure.Annotations;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.Infrastructure
{
    public class Group : INotifyPropertyChanged, IEquatable<Group>
    {
        private string _name;
        private int _count;
        private IEnumerable<Sticker> _stickers;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int Count
        {
            get => _count;
            set => Set(ref _count, value);
        }

        public IEnumerable<Sticker> Stickers
        {
            get => _stickers;
            set => Set(ref _stickers, value);
        }

        public Group(string name, int count, IEnumerable<Sticker> stickers)
        {
            Name = name;
            Count = count;
            Stickers = stickers;
        }

        #region INotifyPropertyChanged implimintation

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }
        
        #endregion

        #region IEquatable<Group> implimentation

        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_name, other._name) && _count == other._count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Group) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_name != null ? _name.GetHashCode() : 0) * 397) ^ _count;
            }
        }

        #endregion
    }
}
