using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class NonPlayerConfig : EntityTypeConfiguration<NonPlayer>
    {
        public NonPlayerConfig()
        {
            HasKey(k=>k.CharacterId);

            Property(p => p.NonPlayerChalengeLevel)
                .IsRequired();

            Property(p => p.NonPlayerPublicAnnotations)
                .IsOptional()
                .HasMaxLength(1000);

            Property(p => p.CharacterImage)
               .IsOptional();

            Property(p => p.NonPlayerType)
                .IsRequired();

            Property(p => p.CharacterName)
               .IsRequired()
               .HasMaxLength(50);

            Property(p => p.CharacterRemainingLife)
                .IsRequired();

            Property(p => p.CharacterMaxLife)
                .IsRequired();

            Property(p => p.CharacterUser)
                .IsRequired();

            Property(p => p.CharacterRace)
                .IsRequired();

            Property(p => p.CharacterGender)
                .IsRequired();

            Ignore(i => i.ValidationResult);
        }
    }
}
