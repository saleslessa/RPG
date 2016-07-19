using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{

    public class Item
    {
        public Guid ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemEffect { get; set; }

        public int ItemPrice { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public Item()
        {
            ItemId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }
    }

}