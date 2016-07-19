using System;
using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Campaign
{
    public class CampaignHasUserMasterSpecification : ISpecification<Entities.Campaign>
    {
        public bool IsSatisfiedBy(Entities.Campaign entity)
        {
            return entity.CampaignUserMaster != null && entity.CampaignUserMaster.Trim().Length > 0;
        }
    }
}
