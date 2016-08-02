using DaemonCharacter.Domain.Validations.CharacterAttribute;
using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{
    public class CharacterAttribute
    {
        public Guid CharacterAttributeId { get; set; }

        public virtual Player Character { get; set; }

        public virtual Attributes Attribute { get; set; }

        public int CharacterAttributeValue { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public CharacterAttribute()
        {
            CharacterAttributeId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }
        
        public bool IsValid()
        {
            ValidationResult = new CharacterAttributeIsConsistentValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}