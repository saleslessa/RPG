using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class CharacterAttributeConfig : EntityTypeConfiguration<CharacterAttribute>
    {

        public CharacterAttributeConfig()
        {
            HasKey(k => k.CharacterAttributeId);

            HasRequired(r => r.Character)
                .WithMany()
                .Map(m => m.MapKey("CharacterId"));

            HasRequired(r => r.Attribute)
                .WithMany()
                .Map(m => m.MapKey("AttributeId"));

            Property(p => p.CharacterAttributeValue)
                .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
