using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Specifications.Attribute;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Attribute
{
    public class CreateAttributeValidator : Validator<Attributes>
    {
        public CreateAttributeValidator(IAttributeRepository attributeRepository)
        {
            var duplicatedName = new AttributeUniqueNameSpecification(attributeRepository);

            base.Add("Duplicated name", new Rule<Attributes>(duplicatedName, "This attribute name already exists. Please chose another."));
        }
    }
}
