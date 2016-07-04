using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class PlayerItemConfig : EntityTypeConfiguration<PlayerItems>
    {
        public PlayerItemConfig()
        {

            HasRequired(r => r.Player)
                .WithMany()
                .Map(m => m.MapKey("PlayerId"));

            HasRequired(r => r.Item)
                .WithMany()
                .Map(m => m.MapKey("ItemId"));

            Property(p => p.PlayerItemQtd)
                .IsRequired();

            Property(p => p.PlayerItemUnitPrice)
                .IsRequired();

            Property(p => p.PlayerItemDateBought)
                .IsRequired()
                .HasColumnType("date");

            Property(p => p.PlayerItemApprovedByMaster)
                .IsOptional();
        }
    }
}
