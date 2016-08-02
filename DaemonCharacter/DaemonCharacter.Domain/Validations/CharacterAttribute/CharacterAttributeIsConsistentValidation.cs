using DaemonCharacter.Domain.Specifications.CharacterAttribute;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.CharacterAttribute
{
    public class CharacterAttributeIsConsistentValidation : Validator<Entities.CharacterAttribute>
    {
        public CharacterAttributeIsConsistentValidation()
        {
            var hasCharacter = new CharacterAttributeHasCharacterSpecification();
            var hasAttribute = new CharacterAttributeHasAttributeSpecification();

            base.Add("HasCharacter", new Rule<Entities.CharacterAttribute>(hasCharacter, "You must select a Character for this Attribute"));
            base.Add("HasCharacter", new Rule<Entities.CharacterAttribute>(hasCharacter, "You must select a Attribute for this Character"));
        }
    }
}
