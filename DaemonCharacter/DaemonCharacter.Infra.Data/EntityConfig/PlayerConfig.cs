using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class PlayerConfig : EntityTypeConfiguration<Player>
    {

        public PlayerConfig()
        {
            HasKey(k => k.CharacterId);

            Property(p => p.CharacterId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(r => r.CharacterUser)
                .WithMany()
                .Map(m => m.MapKey("CharacterUserId"));

            HasRequired(r => r.Campaign)
                .WithMany()
                .Map(m=>m.MapKey("PlayerCampaignId"));

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

            Property(p => p.CharacterUser.UserId)
                .IsRequired();

            Property(p => p.Campaign.CampaignId)
                .IsRequired();

            Property(p => p.CharacterRace)
                .IsRequired();

            Property(p => p.CharacterGender)
                .IsRequired();

        }
    }
}
