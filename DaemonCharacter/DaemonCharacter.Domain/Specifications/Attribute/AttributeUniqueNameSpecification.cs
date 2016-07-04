using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DomainValidation.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return _attributeRepository.GetByName(entity.AttributeName) == null;
        }
    }
}
