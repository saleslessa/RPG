using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class NonPlayerConfig : EntityTypeConfiguration<NonPlayers>
    {
        public NonPlayerConfig()
        {
            HasKey(k=>k.CharacterId);

            Property(p => p.CharacterId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(r => r.CharacterUser)
                .WithMany()
                .Map(m => m.MapKey("CharacterUserId"));

            Property(p => p.NonPlayerChalengeLevel)
                .IsRequired();

            Property(p => p.NonPlayerPublicAnnotations)
                .IsOptional()
                .HasMaxLength(1000);

            Property(p => p.NonPlayerType)
                .IsRequired();

            Property(p => p.CharacterName)
               .IsRequired()
               .HasMaxLength(50);

            Property(p => p.CharacterRemainingLife)
                .IsRequired();

            Property(p => p.CharacterMaxLife)
                .IsRequired();

            Property(p => p.CharacterUser.UserId)
                .IsRequired();

            Property(p => p.CharacterRace)
                .IsRequired();

            Property(p => p.CharacterGender)
                .IsRequired();
        }
    }
}
