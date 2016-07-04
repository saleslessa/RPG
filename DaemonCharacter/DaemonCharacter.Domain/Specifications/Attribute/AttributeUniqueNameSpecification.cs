using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Attribute
{
    public class AttributeUniqueNameSpecification : ISpecification<Attributes>
    {
        private readonly IAttributeRepository _attributeRepository;

        public AttributeUniqueNameSpecification(IAttributeRepository repository)
        {
            _attributeRepository = repository;
        }

        public bool IsSatisfiedBy(Attributes entity)
        {
            //TODO: Adjust validation
            //return _attributeRepository.GetUpdateable(entity.AttributeId, entity.AttributeName) == null;

            return true;
        }
    }
}
