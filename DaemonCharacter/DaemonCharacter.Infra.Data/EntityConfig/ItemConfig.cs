using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class ItemConfig : EntityTypeConfiguration<Item>
    {

        public ItemConfig()
        {
            HasKey(k => k.ItemId);

            Property(p => p.ItemName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.ItemPrice)
                .HasColumnType("float")
                .IsRequired();

            Property(p => p.ItemEffect)
                .IsOptional()
                .HasMaxLength(255);

            Property(p => p.ItemCategory)
                .IsRequired();
        }
    }
}
