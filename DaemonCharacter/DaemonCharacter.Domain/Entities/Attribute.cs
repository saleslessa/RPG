using DomainValidation.Validation;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Entities
{
    public class Attributes
    {
        public Guid AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public AttributeType AttributeType { get; set; }

        public short? AttributeMinimum { get; set; }

        public virtual ICollection<Attributes> ParentAttribute { get; set; }

        public virtual ICollection<Attributes> AttributeBonus { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public Attributes()
        {
            AttributeId = Guid.NewGuid();
            ParentAttribute = new List<Attributes>();
            AttributeBonus = new List<Attributes>();

            ValidationResult = new ValidationResult();
        }

    }

   
}