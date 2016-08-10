using DaemonCharacter.Domain.Specifications.Item;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Item
{
    public class ItemIsConsistentValidation : Validator<Entities.Item>
    {
        public ItemIsConsistentValidation()
        {
            var hasName = new ItemHasNameSpecification();
            base.Add("HasName", new Rule<Entities.Item>(hasName, "The Item name is invalid. Please chose one."));
        }
    }
}
