using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Player
{
    public class PlayerHasCampaignSpecification : ISpecification<Entities.Player>
    {
        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return entity.Campaign != null;
        }
    }
}
