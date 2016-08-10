using DaemonCharacter.Domain.Specifications.Player;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Player
{
    public class PlayerConsistentValidation : Validator<Entities.Player>
    {
        public PlayerConsistentValidation()
        {
            var hasCampaign = new PlayerHasCampaignSpecification();
            var validAge = new PlayerHasValidAgeSpecification();
            var validLevel = new PlayerHasValidLevelSpecification();

            base.Add("hasCampaign", new Rule<Entities.Player>(hasCampaign, "You must select a campaign to your player."));
            base.Add("validAge", new Rule<Entities.Player>(validAge, "The age selected is invalid."));
            base.Add("validLevel", new Rule<Entities.Player>(validLevel, "The level selected is invalid."));
        }
    }
}
