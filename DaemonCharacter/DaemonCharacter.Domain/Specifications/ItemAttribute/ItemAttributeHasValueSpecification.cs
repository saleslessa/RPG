using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.ItemAttribute
{
    public class ItemAttributeHasValueSpecification : ISpecification<Entities.ItemAttribute>
    {
        public bool IsSatisfiedBy(Entities.ItemAttribute entity)
        {
            return entity.ItemAttributeValue != 0;
        }
    }
}
