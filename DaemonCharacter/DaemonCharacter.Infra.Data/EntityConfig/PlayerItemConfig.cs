﻿using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class PlayerItemConfig : EntityTypeConfiguration<PlayerItem>
    {
        public PlayerItemConfig()
        {
            HasKey(k => k.PlayerItemId);

            HasRequired(r => r.Player)
                .WithMany()
                .Map(m => m.MapKey("CharacterId"));

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

            Property(p => p.Modified)
                .HasColumnType("smalldatetime")
                .IsOptional();

            Property(p => p.PlayerItemActive)
                .HasColumnName("Active")
                .HasColumnType("bit")
                .IsRequired();

            Ignore(i => i.ValidationResult);
        }
    }
}
