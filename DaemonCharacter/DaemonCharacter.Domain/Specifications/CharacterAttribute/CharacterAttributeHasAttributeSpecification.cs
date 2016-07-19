using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.CharacterAttribute
{
    public class CharacterAttributeHasAttributeSpecification : ISpecification<Entities.CharacterAttribute>
    {
        public bool IsSatisfiedBy(Entities.CharacterAttribute entity)
        {
            return entity.Attribute != null;
        }
    }
}
