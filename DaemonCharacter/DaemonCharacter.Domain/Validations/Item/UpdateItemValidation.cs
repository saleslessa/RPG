using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Specifications.Item;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Item
{
    public class UpdateItemValidation : Validator<Entities.Item>
    {

        public UpdateItemValidation(IItemService _itemService)
        {
            var hasUniqueName = new ItemHasUniqueNameSpecification(_itemService);
            var hasEffect = new ItemHasEffectSpecification();
            var hasName = new ItemHasNameSpecification();
            var hasPrice = new ItemHasPriceSpecification();


            base.Add("hasUniqueName", new Rule<Entities.Item>(hasUniqueName, "This name is already used by another item. Please chose another."));
            base.Add("hasEffect", new Rule<Entities.Item>(hasEffect, "Effect invalid."));

            base.Add("hasName", new Rule<Entities.Item>(hasName, "Invalid name."));
            base.Add("hasPrice", new Rule<Entities.Item>(hasPrice, "Invalid price."));
        }
    }
}
