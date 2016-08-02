using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Validations.Attribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Linq;

namespace DomainTests.Validation
{
    [TestClass]
    public class CreateAttributeValidationTests
    {

        [TestMethod]
        public void CreateAttribute_Validation_True()
        {
            //Arrange
            var attribute = new Attributes()
            {
                AttributeName = "Test"
            };

            //ACT
            var repo = MockRepository.GenerateStub<IAttributeRepository>();
            repo.Stub(s => s.GetUpdateable(attribute.AttributeId, attribute.AttributeName)).Return(null);

            var validationResult = new CreateAttributeValidation(repo).Validate(attribute);

            //ASSERT
            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void CreateAttribute_Validation_False()
        {
            //Arrange
            var attribute = new Attributes()
            {
                AttributeName = "Test"
            };

            //ACT
            var repo = MockRepository.GenerateStub<IAttributeRepository>();
            repo.Stub(s => s.GetUpdateable(attribute.AttributeId, attribute.AttributeName)).Return(attribute);

            var validationResult = new CreateAttributeValidation(repo).Validate(attribute);

            //ASSERT
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "This attribute name already exists. Please chose another."));
        }
    }
}
