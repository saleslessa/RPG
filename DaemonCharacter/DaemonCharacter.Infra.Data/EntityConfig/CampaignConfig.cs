using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class CampaignConfig : EntityTypeConfiguration<Campaign>
    {
        public CampaignConfig()
        {

            Property(p => p.CampaignName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.CampaignMaxPlayers)
                .IsRequired()
                .HasColumnType("smallint");

            Property(p => p.CampaignBriefing)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.CampaignShortDescription)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.CampaignRemainingPlayers)
                .IsOptional()
                .HasColumnType("smallint");

            Property(p => p.CampaignImg)
                .IsOptional()
                .HasColumnType("varbinary");

            Property(p => p.CampaignStatus)
                .IsRequired();

            Property(p => p.CampaignStartYear)
                .IsOptional();

            Ignore(p => p.ValidationResult);
        }
    }
}
