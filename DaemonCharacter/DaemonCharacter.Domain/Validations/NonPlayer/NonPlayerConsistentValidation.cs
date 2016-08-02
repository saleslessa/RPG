using DaemonCharacter.Domain.Specifications.Character;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.NonPlayer
{
    public class NonPlayerConsistentValidation : Validator<Entities.NonPlayer>
    {
        public NonPlayerConsistentValidation()
        {
            var hasName = new CharacterHasNameSpecification();

            base.Add("HasName", new Rule<Entities.NonPlayer>(hasName, "Non Player Name must be filled."));
        }
    }
}
