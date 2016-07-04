using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class SessionConfig : EntityTypeConfiguration<Sessions>
    {
        public SessionConfig()
        {
            HasKey(k => k.SessionId);

            Property(p => p.SessionId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasRequired(r => r.Campaign)
                .WithMany()
                .Map(m => m.MapKey("CampaignId"));

            Property(p => p.SessionBriefing)
                .IsRequired()
                .HasMaxLength(500);

            Property(p => p.SessionDateScheduled)
                .IsRequired()
                .HasColumnType("smalldatetime");

            Property(p => p.SessionDuringAnnotations)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.SessionPrivateBeforeAnnotations)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.SessionStatus)
                .IsRequired();
            
        }
    }
}
