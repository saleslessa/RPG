using DaemonCharacter.Domain.Specifications.Character;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.NonPlayer
{
    public class NonPlayerConsistentValidator : Validator<Entities.NonPlayer>
    {
        public NonPlayerConsistentValidator()
        {
            var hasName = new CharacterHasNameSpecification();

            base.Add("HasName", new Rule<Entities.NonPlayer>(hasName, "Non Player Name must be filled."));
        }
    }
}
