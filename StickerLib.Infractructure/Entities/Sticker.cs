using System;

namespace StickerLib.Infrastructure.Entities
{
    public class Sticker : IEquatable<Sticker>
    {
        /// <summary>
        /// Uniqual identifyre for sticker on database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title for Sticker
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Pdf file in byte array
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// Initialize new object of Sticker class
        /// </summary>
        /// <param name="name">Title for sticker</param>
        /// <param name="file">Pdf file on byte array</param>
        public Sticker(string name, byte[] file)
        {
            Name = name;
            File = file;
        }

        /// <summary>
        /// Initialize new object of Sticker class
        /// </summary>
        public Sticker()
        {
        }

        public bool Equals(Sticker other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name) && Equals(File, other.File);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Sticker) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (File != null ? File.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
