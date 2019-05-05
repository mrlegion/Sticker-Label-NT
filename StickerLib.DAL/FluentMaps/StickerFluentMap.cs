using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StickerLib.Infrastructure.Entities;

namespace StickerLib.DAL.FluentMaps
{
    public class StickerFluentMap : EntityTypeConfiguration<Sticker>
    {
        public StickerFluentMap()
        {
            ToTable("stickers").HasKey(s => s.Id);
            Property(s => s.Id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(s => s.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired();
            Property(s => s.File)
                .HasColumnName("content")
                .HasColumnType("blob")
                .IsRequired();
        }
    }
}