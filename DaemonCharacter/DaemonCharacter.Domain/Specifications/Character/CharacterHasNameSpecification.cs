using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Character
{
    public class CharacterHasNameSpecification : ISpecification<Entities.Character>
    {
        public bool IsSatisfiedBy(Entities.Character entity)
        {
            return entity.CharacterName != null && entity.CharacterName.Trim().Length > 0 && entity.CharacterName.Trim().Length <= 50;
        }
    }
}
