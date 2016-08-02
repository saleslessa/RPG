using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Item
{
    public class ItemHasPriceSpecification : ISpecification<Entities.Item>
    {
        public bool IsSatisfiedBy(Entities.Item entity)
        {
            return entity.ItemPrice > 0;
        }
    }
}
