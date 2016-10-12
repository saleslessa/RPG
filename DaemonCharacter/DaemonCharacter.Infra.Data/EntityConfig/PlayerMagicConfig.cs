using System.Data.Entity.ModelConfiguration;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class PlayerMagicConfig : EntityTypeConfiguration<PlayerMagic>
    {
        public PlayerMagicConfig()
        {
            HasKey(k => k.PlayerMagicId);

            HasRequired(r => r.Player)
                .WithMany()
                .Map(m => m.MapKey("PlayerId"));

            HasRequired(r => r.Magic)
                .WithMany()
                .Map(m => m.MapKey("MagicId"));

            Ignore(i => i.ValidationResult);
        }
    }
}
