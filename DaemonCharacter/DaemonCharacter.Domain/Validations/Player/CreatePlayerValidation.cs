using DomainValidation.Validation;
using DaemonCharacter.Domain.Specifications.Character;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Validations.Player
{
    public class CreatePlayerValidation : Validator<Entities.Player>
    {
        public CreatePlayerValidation(IPlayerRepository playerRepository)
        {
            var duplicatedName = new CharacterUniqueNameSpecification(playerRepository);
            var hasCampaign = new PlayerHasCampaignSpecification();

            base.Add("Duplicated name", new Rule<Entities.Player>(duplicatedName, "This attribute name already exists. Please chose another."));
            base.Add("Without campaign", new Rule<Entities.Player>(hasCampaign, "You must select a campaign to your player"));
        }
    }
}
