using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class ItemConfig : EntityTypeConfiguration<Items>
    {

        public ItemConfig()
        {
            HasKey(k => k.ItemId);

            Property(p => p.ItemId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            Property(p => p.ItemName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.ItemPrice)
                .IsRequired();

            Property(p => p.ItemEffect)
                .IsOptional()
                .HasMaxLength(255);
        }
    }
}
