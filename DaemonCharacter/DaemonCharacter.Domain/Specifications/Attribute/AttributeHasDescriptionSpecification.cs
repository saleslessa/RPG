using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Attribute
{
    public class AttributeHasDescriptionSpecification : ISpecification<Attributes>
    {
        public bool IsSatisfiedBy(Attributes model)
        {
            return model.AttributeDescription != null && model.AttributeDescription.Trim().Length > 0 && model.AttributeDescription.Trim().Length <= 50;
        }
    }
}
