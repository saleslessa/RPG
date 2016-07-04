using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class AttributeConfig : EntityTypeConfiguration<Attributes>
    {
        public AttributeConfig()
        {

            Property(p => p.AttributeId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            HasMany(m => m.ParentAttribute)
                .WithMany(m => m.AttributeBonus)
                .Map(m => m.ToTable("AttributeBonus")
                    .MapLeftKey("AttributeId")
                    .MapRightKey("AttributeBonusId"));

            Property(p => p.AttributeName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.AttributeMinimum)
                .HasColumnType("tinyint")
                .IsOptional();

            Property(p => p.AttributeDescription)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.AttributeType)
                .IsRequired();
        }
    }
}
