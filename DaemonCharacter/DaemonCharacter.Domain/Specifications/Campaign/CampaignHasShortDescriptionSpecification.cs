using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Campaign
{
    public class CampaignHasShortDescriptionSpecification : ISpecification<Entities.Campaign>
    {
        public bool IsSatisfiedBy(Entities.Campaign entity)
        {
            return entity.CampaignShortDescription != null && entity.CampaignShortDescription.Trim().Length > 0 && entity.CampaignShortDescription.Trim().Length <= 500;
        }
    }
}
