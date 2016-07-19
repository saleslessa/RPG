using DaemonCharacter.Domain.Specifications.PlayerItem;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.PlayerItem
{
    public class PlayerItemConsistentValidation : Validator<Entities.PlayerItem>
    {
        public PlayerItemConsistentValidation()
        {
            var dateValidated = new PlayerItemValidateDateBoughtSpecification();

            base.Add("DateValidated", new Rule<Entities.PlayerItem>(dateValidated, "Date of bought is invalid. Please try again."));
        }
    }
}
