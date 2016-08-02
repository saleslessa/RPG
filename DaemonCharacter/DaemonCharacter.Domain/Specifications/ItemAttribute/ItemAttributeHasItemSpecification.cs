using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.ItemAttribute
{
    public class ItemAttributeHasItemSpecification : ISpecification<Entities.ItemAttribute>
    {
        public bool IsSatisfiedBy(Entities.ItemAttribute entity)
        {
            return entity.Item != null;
        }
    }
}
