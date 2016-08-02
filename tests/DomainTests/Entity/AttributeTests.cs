using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaemonCharacter.Domain.Entities;
using System.Linq;

namespace DomainTests.Entity
{
    [TestClass]
    public class AttributeTests
    {
        //AAA - ARRANGE, ACT, ASSERT
        [TestMethod]
        public void Attribute_IsConsistent_True()
        {
            //Arrange
            var attributeCharacteristic = new Attributes()
            {
                AttributeId = Guid.NewGuid(),
                AttributeName = "Name Test",
                AttributeDescription = "Attribute Descripton",
                AttributeType = AttributeType.Characteristic,
                AttributeMinimum = null
            };
            var attributeNotCharacteristic = new Attributes()
            {
                AttributeId = Guid.NewGuid(),
                AttributeName = "Name Test",
                AttributeDescription = "Attribute Descripton",
                AttributeType = AttributeType.Misc,
                AttributeMinimum = 1
            };

            //Act
            var resultCharacteristic = attributeCharacteristic.IsValid();
            var resultNotCharacteristic = attributeNotCharacteristic.IsValid();

            //Assert
            Assert.IsTrue(resultCharacteristic);
            Assert.IsTrue(resultNotCharacteristic);
        }

        //AAA - ARRANGE, ACT, ASSERT
        [TestMethod]
        public void Attribute_IsConsistent_False()
        {
            //Arrange
            var attributeCharacteristic = new Attributes()
            {
                AttributeDescription = "Attribute Descripton",
                AttributeType = AttributeType.Characteristic,
                AttributeMinimum = 1
            };
            var attributeNotCharacteristic = new Attributes()
            {
                AttributeName = "Name Test",
                AttributeType = AttributeType.Misc,
                AttributeMinimum = null
            };

            //Act
            var resultCharacteristic = attributeCharacteristic.IsValid();
            var resultNotCharacteristic = attributeNotCharacteristic.IsValid();

            //Assert
            Assert.IsFalse(resultCharacteristic);
            Assert.IsTrue(attributeCharacteristic.ValidationResult.Erros.Any(e => e.Message == "Attribute Name must be filled"));
            Assert.IsTrue(attributeCharacteristic.ValidationResult.Erros.Any(e=> e.Message == "Attribute Minimum must be greater than zero or has invalid value"));
            
            Assert.IsFalse(resultNotCharacteristic);
            Assert.IsTrue(attributeNotCharacteristic.ValidationResult.Erros.Any(e => e.Message == "Attribute Description must be filled"));
            Assert.IsTrue(attributeCharacteristic.ValidationResult.Erros.Any(e => e.Message == "Attribute Minimum must be greater than zero or has invalid value"));
        }
    }
}
