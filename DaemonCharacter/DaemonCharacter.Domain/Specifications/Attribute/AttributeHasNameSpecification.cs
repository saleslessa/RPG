using DaemonCharacter.Domain.Entities;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Attribute
{
    public class AttributeHasNameSpecification : ISpecification<Attributes>
    {
        public bool IsSatisfiedBy(Attributes entity)
        {
            return entity.AttributeName.Trim().Length > 0 && entity.AttributeName.Trim().Length <= 50;
        }
    }
}
