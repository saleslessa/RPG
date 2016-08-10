using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.PlayerItem
{
    public class PlayerItemHasItemSpecification : ISpecification<Entities.PlayerItem>
    {
        public bool IsSatisfiedBy(Entities.PlayerItem entity)
        {
            return entity.Item != null;
        }
    }
}
