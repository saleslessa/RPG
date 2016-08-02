using DaemonCharacter.Domain.Validations.Campaign;
using DomainValidation.Validation;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Entities
{
    public class Campaign
    {
        public Guid CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string CampaignShortDescription { get; set; }

        public string CampaignBriefing { get; set; }

        public short CampaignStartYear { get; set; }

        public short CampaignMaxPlayers { get; set; }

        public short CampaignRemainingPlayers { get; set; }

        public byte[] CampaignImg { get; set; }

        public CampaignStatus CampaignStatus { get; set; }

        public string CampaignUserMaster { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public Campaign()
        {
            CampaignId = new Guid();
            ValidationResult = new ValidationResult();
            Players = new List<Player>();
        }

        public bool IsValid()
        {
            ValidationResult = new CampaignConsistentValidation().Validate(this);

            return ValidationResult.IsValid;
        }

    }

}