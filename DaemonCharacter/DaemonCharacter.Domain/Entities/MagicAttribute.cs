using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Entities
{
    public class MagicAttribute
    {
        public Guid MagicAttributeId { get; set; }

        public virtual Magic Magic { get; set; }

        public virtual Attributes Attribute { get; set; }

        public int Value { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public MagicAttribute()
        {
            MagicAttributeId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }
    }
}
