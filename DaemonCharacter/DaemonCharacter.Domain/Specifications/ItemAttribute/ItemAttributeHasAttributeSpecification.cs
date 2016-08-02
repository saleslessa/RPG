using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.ItemAttribute
{
    public class ItemAttributeHasAttributeSpecification : ISpecification<Entities.ItemAttribute>
    {
        public bool IsSatisfiedBy(Entities.ItemAttribute entity)
        {
            return entity.Attribute != null;
        }
    }
}
