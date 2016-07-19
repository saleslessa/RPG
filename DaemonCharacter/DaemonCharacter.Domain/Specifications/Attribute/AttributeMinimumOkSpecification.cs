using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Attribute
{
    public class AttributeMinimumOkSpecification : ISpecification<Attributes>
    {
        public bool IsSatisfiedBy(Attributes model)
        {
            return model.AttributeMinimum > 10;
        }
    }
}
