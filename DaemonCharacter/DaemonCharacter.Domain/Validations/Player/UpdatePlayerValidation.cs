using DomainValidation.Validation;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Specifications.Character;

namespace DaemonCharacter.Domain.Validations.Player
{
    public class UpdatePlayerValidation : Validator<Entities.Player>
    {
        public UpdatePlayerValidation(ICharacterRepository playerRepository)
        {
            var duplicatedName = new CharacterUpdateUniqueNameSpecification(playerRepository);
            var hasCampaign = new PlayerHasCampaignSpecification();

            base.Add("Without campaign", new Rule<Entities.Player>(hasCampaign, "You must select a campaign to your player"));
            base.Add("Duplicated name", new Rule<Entities.Player>(duplicatedName, "this name is already used by another player. Please select another"));
        }
    }
}
