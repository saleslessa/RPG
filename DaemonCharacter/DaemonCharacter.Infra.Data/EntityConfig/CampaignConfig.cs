using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class CampaignConfig : EntityTypeConfiguration<Campaign>
    {
        public CampaignConfig()
        {
            //HasRequired(r => r.CampaignUserMaster)
            //    .WithMany()
            //    .Map(m => m.MapKey("CampaignIdMaster"));

            HasRequired(r => r.CampaignUserMaster)
               .WithMany();

            Property(p => p.CampaignUserMaster.UserId)
                .HasColumnName("CampaignIdMaster");

            Property(p => p.CampaignName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.CampaignMaxPlayers)
                .IsRequired()
                .HasColumnType("tinyint");

            Property(p => p.CampaignBriefing)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.CampaignShortDescription)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.CampaignRemainingPlayers)
                .IsOptional()
                .HasColumnType("tinyint");

            Property(p => p.CampaignImg)
                .IsOptional()
                .HasColumnType("varbinary");

            Property(p => p.CampaignStatus)
                .IsRequired();
        }
    }
}
