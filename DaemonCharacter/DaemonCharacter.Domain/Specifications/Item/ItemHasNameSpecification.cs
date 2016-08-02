using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Item
{
    public class ItemHasNameSpecification : ISpecification<Entities.Item>
    {
        public bool IsSatisfiedBy(Entities.Item entity)
        {
            return entity.ItemName != null && entity.ItemName.Trim().Length > 0;
        }
    }
}
