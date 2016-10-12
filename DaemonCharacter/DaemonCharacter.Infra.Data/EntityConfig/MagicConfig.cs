using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Infra.Data.EntityConfig
{
    public class MagicConfig : EntityTypeConfiguration<Magic>
    {
        public MagicConfig()
        {
            HasKey(k => k.MagicId);

            Ignore(i => i.ValidationResult);
        }
    }
}
