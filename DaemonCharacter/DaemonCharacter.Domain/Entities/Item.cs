using DaemonCharacter.Domain.Validations.Item;
using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{

    public class Item
    {
        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemEffect { get; set; }

        public double ItemPrice { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public Item()
        {
            ItemId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }

        public bool IsValid()
        {
            ValidationResult = new ItemIsConsistentValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

}