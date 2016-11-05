using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Specifications.Item;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Item
{
    public class CreateItemValidation : Validator<Entities.Item>
    {
        public CreateItemValidation(IItemService _itemService)
        {
            var hasUniqueName = new ItemHasUniqueNameSpecification(_itemService);
            var hasEffect = new ItemHasEffectSpecification();

            base.Add("hasUniqueName", new Rule<Entities.Item>(hasUniqueName, "This name is already used by another item. Please chose another."));
            base.Add("hasEffect", new Rule<Entities.Item>(hasEffect, "Effect invalid."));
        }

        

    }
}
