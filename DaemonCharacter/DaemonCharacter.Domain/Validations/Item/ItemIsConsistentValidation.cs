using DaemonCharacter.Domain.Specifications.Item;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Item
{
    public class ItemIsConsistentValidation : Validator<Entities.Item>
    {
        public ItemIsConsistentValidation()
        {
            var hasPrice = new ItemHasPriceSpecification();
            var hasName = new ItemHasNameSpecification();

            base.Add("HasPrice", new Rule<Entities.Item>(hasPrice, "All item must have a price. Please chose a valid value."));
            base.Add("HasName", new Rule<Entities.Item>(hasName, "The Item name is invalid. Please chose one."));
        }
    }
}
