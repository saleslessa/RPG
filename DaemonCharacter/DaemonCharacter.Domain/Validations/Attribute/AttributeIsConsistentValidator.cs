using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Specifications.Attribute;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Validations.Attribute
{
    public class AttributeIsConsistentValidator : Validator<Attributes>
    {
        public AttributeIsConsistentValidator()
        {
            var hasName = new AttributeHasNameSpecification();
            var hasDescription = new AttributeHasDescriptionSpecification();
            var minimumOk = new AttributeMinimumOkSpecification();


            base.Add("hasName", new Rule<Attributes>(hasName, "Attribute Name must be filled"));
            base.Add("hasDescription", new Rule<Attributes>(hasDescription, "Attribute Description must be filled"));
            base.Add("minimumOk", new Rule<Attributes>(minimumOk, "Attribute Minimum must be greater than zero"));
        }
    }
}
