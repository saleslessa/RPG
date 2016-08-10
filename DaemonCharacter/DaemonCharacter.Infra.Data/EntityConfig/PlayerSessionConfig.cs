using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class PlayerSessionConfig : EntityTypeConfiguration<PlayerSessions>
    {
        public PlayerSessionConfig()
        {

            HasRequired(r => r.Player)
                .WithMany()
                .Map(m => m.MapKey("CharacterId"));

            HasRequired(r => r.Session)
                .WithMany()
                .Map(m => m.MapKey("SessionId"));

            Property(p => p.PlayerSessionPrivateAnnotations)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.PlayerSessionPublicAnnotations)
                .IsOptional()
                .HasMaxLength(500);
        }
    }
}
