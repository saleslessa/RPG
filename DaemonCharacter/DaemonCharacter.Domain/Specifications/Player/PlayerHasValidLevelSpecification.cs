using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Player
{
    public class PlayerHasValidLevelSpecification : ISpecification<Entities.Player>
    {
        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return entity.PlayerLevel > 0;
        }
    }
}
