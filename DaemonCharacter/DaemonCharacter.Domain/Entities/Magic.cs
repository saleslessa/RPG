using System;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Entities
{
    public class Magic
    {
        public Guid MagicId { get; set; }

        public string MagicName { get; set; }

        public string MagicEffect { get; set; }

        public int MagicLevel { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public Magic()
        {
            ValidationResult = new ValidationResult();
            MagicId = Guid.NewGuid();
        }
    }
}
