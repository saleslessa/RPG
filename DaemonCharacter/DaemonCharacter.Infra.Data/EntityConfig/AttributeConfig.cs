using DaemonCharacter.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class AttributeConfig : EntityTypeConfiguration<Attributes>
    {
        public AttributeConfig()
        {
            HasKey(k => k.AttributeId);

            HasMany(m => m.ParentAttribute)
                .WithMany(m => m.AttributeBonus)
                .Map(m => m.ToTable("AttributeBonus")
                    .MapLeftKey("AttributeId")
                    .MapRightKey("AttributeBonusId"));

            Property(p => p.AttributeName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.AttributeMinimum)
                .IsOptional();

            Property(p => p.AttributeDescription)
                .IsOptional()
                .HasMaxLength(500);

            Property(p => p.AttributeType)
                .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
