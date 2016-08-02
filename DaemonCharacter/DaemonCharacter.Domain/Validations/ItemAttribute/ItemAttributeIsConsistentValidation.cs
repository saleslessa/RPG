using DaemonCharacter.Domain.Specifications.ItemAttribute;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.ItemAttribute
{
    public class ItemAttributeIsConsistentValidation : Validator<Entities.ItemAttribute>
    {
        public ItemAttributeIsConsistentValidation()
        {
            var hasValue = new ItemAttributeHasValueSpecification();
            var hasAttribute = new ItemAttributeHasAttributeSpecification();
            var hasItem = new ItemAttributeHasItemSpecification();

            base.Add("HasValue", new Rule<Entities.ItemAttribute>(hasValue, "Value invalid."));
            base.Add("HasAttribute", new Rule<Entities.ItemAttribute>(hasAttribute, "Attribute not selected. Please select one."));
            base.Add("HasItem", new Rule<Entities.ItemAttribute>(hasItem, "Item not selected. Please select one."));
        }
    }
}
