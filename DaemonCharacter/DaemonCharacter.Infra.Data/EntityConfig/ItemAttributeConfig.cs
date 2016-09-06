using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class ItemAttributeConfig : EntityTypeConfiguration<ItemAttribute>
    {

        public ItemAttributeConfig()
        {
            HasKey(k => k.ItemAttributeId);

            HasRequired(r => r.Item)
                .WithMany()
                .Map(m => m.MapKey("ItemId"));

            HasRequired(r => r.Attribute)
                .WithMany()
                .Map(m => m.MapKey("AttributeId"));

            Property(p => p.ItemAttributeValue)
                .IsRequired();

            Ignore(i => i.ValidationResult);
        }
    }
}
