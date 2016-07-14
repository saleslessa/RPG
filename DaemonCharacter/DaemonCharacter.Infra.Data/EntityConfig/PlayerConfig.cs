using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class PlayerConfig : EntityTypeConfiguration<Player>
    {

        public PlayerConfig()
        {
            HasKey(p => p.CharacterId);

            HasRequired(r => r.Campaign)
                .WithMany(m => m.Players)
                .Map(m => m.MapKey("PlayerCampaignId"));

            Property(p => p.CharacterName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.PlayerBackground)
                .IsOptional()
                .HasMaxLength(5000);

            Property(p => p.PlayerLevel)
                .IsRequired();

            Property(p => p.PlayerAge)
                .IsOptional();

            Property(p => p.PlayerMoney)
                .IsOptional();

            Property(p => p.PlayerPointsToDistribute)
                .IsOptional();

            Property(p => p.PlayerRemainingPoints)
                .IsOptional();

            Property(p => p.CharacterRemainingLife)
                .IsRequired();

            Property(p => p.CharacterMaxLife)
                .IsRequired();

            Property(p => p.PlayerExperience)
                .IsOptional();

            Property(p => p.CharacterUser)
                .IsRequired();

            Property(p => p.CharacterRace)
                .IsRequired();

            Property(p => p.CharacterGender)
                .IsRequired();
        }
    }
}
