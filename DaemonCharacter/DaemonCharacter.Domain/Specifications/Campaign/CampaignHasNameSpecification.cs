using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Campaign
{
    public class CampaignHasNameSpecification : ISpecification<Entities.Campaign>
    {
        public bool IsSatisfiedBy(Entities.Campaign entity)
        {
            return entity.CampaignName != null && entity.CampaignName.Trim().Length > 0 && entity.CampaignName.Trim().Length <= 50;
        }
    }
}
