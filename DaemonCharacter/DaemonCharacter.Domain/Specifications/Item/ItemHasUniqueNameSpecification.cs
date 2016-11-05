using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Service;

namespace DaemonCharacter.Domain.Specifications.Item
{
    public class ItemHasUniqueNameSpecification : ISpecification<Entities.Item>
    {
        private readonly IItemService _itemService;

        public ItemHasUniqueNameSpecification(IItemService itemService)
        {
            _itemService = itemService;
        }

        public bool IsSatisfiedBy(Entities.Item entity)
        {
            var obj = _itemService.SearchByName(entity.ItemName);
            return obj == null || obj.ItemId == entity.ItemId;
        }
    }
}
