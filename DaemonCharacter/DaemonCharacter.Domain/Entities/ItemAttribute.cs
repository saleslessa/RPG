using DaemonCharacter.Domain.Validations.ItemAttribute;
using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{

    public class ItemAttribute
    {
        public Guid ItemAttributeId { get; set; }

        public virtual Item Item { get; set; }

        public virtual Attributes Attribute { get; set; }

        public int ItemAttributeValue { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public ItemAttribute()
        {
            ItemAttributeId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }

        public bool IsValid()
        {
            ValidationResult = new ItemAttributeIsConsistentValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}