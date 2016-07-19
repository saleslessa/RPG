using DaemonCharacter.Domain.Specifications.Campaign;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Campaign
{
    public class CampaignConsistentValidator : Validator<Entities.Campaign>
    {
        public CampaignConsistentValidator()
        {
            var hasName = new CampaignHasNameSpecification();
            var hasShortDescription = new CampaignHasShortDescriptionSpecification();
            var hasUserMaster = new CampaignHasUserMasterSpecification();

            base.Add("HasName", new Rule<Entities.Campaign>(hasName, "Campaign Name must be filled."));
            base.Add("HasShortDescription", new Rule<Entities.Campaign>(hasShortDescription, "Campaign Short Description must be filled."));
            base.Add("HasUserMaster", new Rule<Entities.Campaign>(hasUserMaster, "User Master must be filled."));

        }
    }
}
