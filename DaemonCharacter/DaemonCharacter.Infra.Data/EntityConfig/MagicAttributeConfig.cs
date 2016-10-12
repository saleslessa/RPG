using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class MagicAttributeConfig : EntityTypeConfiguration<MagicAttribute>
    {
        public MagicAttributeConfig()
        {
            HasKey(k => k.MagicAttributeId);

            HasRequired(r => r.Magic)
                .WithMany()
                .Map(m => m.MapKey("MagicId"));

            HasRequired(r => r.Attribute)
                .WithMany()
                .Map(m => m.MapKey("AttributeId"));

            Ignore(i => i.ValidationResult);
        }
    }
}
