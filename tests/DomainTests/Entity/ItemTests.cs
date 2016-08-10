using DaemonCharacter.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DomainTests.Entity
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void Item_IsConsistent_True()
        {
            //Arrange
            var item = new Item()
            {
                ItemName = "test",
                ItemEffect = "asd",
                ItemPrice = 29.95
            };

            //Act
            var ValidationResult = item.IsValid();

            //Assert
            Assert.IsTrue(ValidationResult);
        }

        [TestMethod]
        public void Item_IsConsistent_False()
        {
            //Arrange
            var item = new Item()
            {
                ItemEffect = "asd"
            };

            //Act
            var ValidationResult = item.IsValid();

            //Assert
            Assert.IsFalse(ValidationResult);

            Assert.IsTrue(item.ValidationResult.Erros.Any(e => e.Message == "All item must have a price. Please chose a valid value."));
            Assert.IsTrue(item.ValidationResult.Erros.Any(e => e.Message == "The Item name is invalid. Please chose one."));
        }

        
    }
}
