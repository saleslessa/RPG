using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Validations.Item;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System.Linq;

namespace DomainTests.Validation
{
    [TestClass]
    public class ItemValidationTests
    {

        [TestMethod]
        public void Item_Create_True()
        {
            var item = new Item()
            {
                ItemName = "test",
                ItemEffect = "test",
                ItemCategory = ItemCategory.Amulets
            };

            //ACT
            var repo = MockRepository.GenerateStub<IItemService>();
            repo.Stub(s => s.SearchByName(item.ItemName)).Return(null);

            var validationResult = new CreateItemValidation(repo).Validate(item);

            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void Item_Create_False()
        {
            var item = new Item();

            //ACT
            var repo = MockRepository.GenerateStub<IItemService>();
            repo.Stub(s => s.SearchByName(item.ItemName)).Return(item);

            var validationResult = new CreateItemValidation(repo).Validate(item);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "This name is already used by another item. Please chose another."));
            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "Effect invalid."));
        }

        [TestMethod]
        public void Item_Update_True()
        {
            var item = new Item()
            {
                ItemName = "test",
                ItemCategory = ItemCategory.Amulets,
                ItemEffect = "test effect",
                UniqueUse = true,
                ItemPrice = 2
            };

            //ACT
            var repo = MockRepository.GenerateStub<IItemService>();
            repo.Stub(s => s.SearchByName(item.ItemName)).Return(item);

            var validationResult = new UpdateItemValidation(repo).Validate(item);

            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void Item_Update_False()
        {
            var item = new Item();

            //ACT
            var repo = MockRepository.GenerateStub<IItemService>();
            repo.Stub(s => s.SearchByName(item.ItemName)).Return(new Item());

            var validationResult = new UpdateItemValidation(repo).Validate(item);

            Assert.IsFalse(validationResult.IsValid);

            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "This name is already used by another item. Please chose another."));
            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "Effect invalid."));
            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "Invalid name."));
            Assert.IsTrue(validationResult.Erros.Any(e => e.Message == "Invalid price."));
        }

    }
}
