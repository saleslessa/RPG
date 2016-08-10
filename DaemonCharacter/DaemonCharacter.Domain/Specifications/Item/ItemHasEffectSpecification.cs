using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Item
{
    public class ItemHasEffectSpecification : ISpecification<Entities.Item>
    {
        public bool IsSatisfiedBy(Entities.Item entity)
        {
            return entity.ItemEffect != null && entity.ItemEffect.Trim().Length > 0;
        }
    }
}
