using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Attribute
{
    public class AttributeMinimumOkSpecification : ISpecification<Attributes>
    {
        public bool IsSatisfiedBy(Attributes model)
        {
            if (model.AttributeType == AttributeType.Characteristic)
                return model.AttributeMinimum == null || model.AttributeMinimum == 0;

            return model.AttributeMinimum != null && model.AttributeMinimum > 0;

        }
    }
}
