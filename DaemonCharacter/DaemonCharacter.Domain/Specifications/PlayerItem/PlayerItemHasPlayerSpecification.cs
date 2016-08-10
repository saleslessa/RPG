using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.PlayerItem
{
    public class PlayerItemHasPlayerSpecification : ISpecification<Entities.PlayerItem>
    {
        public bool IsSatisfiedBy(Entities.PlayerItem entity)
        {
            return entity.Player != null;
        }
    }
}
