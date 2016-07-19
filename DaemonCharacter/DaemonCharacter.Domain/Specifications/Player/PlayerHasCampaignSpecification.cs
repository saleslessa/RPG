using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Character
{
    public class PlayerHasCampaignSpecification : ISpecification<Entities.Player>
    {
        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return entity.Campaign != null;
        }
    }
}
