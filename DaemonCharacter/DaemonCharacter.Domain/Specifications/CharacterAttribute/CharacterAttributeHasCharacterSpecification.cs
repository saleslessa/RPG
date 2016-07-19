using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.CharacterAttribute
{
    public class CharacterAttributeHasCharacterSpecification : ISpecification<Entities.CharacterAttribute>
    {
        public bool IsSatisfiedBy(Entities.CharacterAttribute entity)
        {
            return entity.Character != null;
        }
    }
}
