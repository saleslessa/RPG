using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace ApplicationTests.AppService
{
    [TestClass]
    public class AttributeAppService
    {
        //ARRANGE, ACT, ASSERT
        [TestMethod]
        public void AttributeAppService_Add_True()
        {
            var viewModel = new AttributeViewModel()
            {
                AttributeName = "Test",
                AttributeMinimum = 1,
                AttributeType = AttributeType.Misc,
                AttributeDescription = "test"
            };

            viewModel.AttributeBonus.Add(new AttributeBonusViewModel() {
                AttributeName = "teste2",
                Selected = true
            });

            viewModel.AttributeBonus.Add(new AttributeBonusViewModel()
            {
                AttributeName = "teste3",
                Selected = false
            });

            var appService = MockRepository.GenerateMock<IAttributeAppService>();

            appService.Stub(s => s.Add(viewModel)).Return(viewModel);
            

            var resultAppService = viewModel.ValidationResult;


            Assert.IsTrue(resultAppService.IsValid);
        }

        //ARRANGE, ACT, ASSERT
        [TestMethod]
        public void AttributeAppService_Add_False()
        {
            var viewModel = new AttributeViewModel()
            {
                AttributeType = AttributeType.Misc,
                AttributeDescription = "test"
            };

            viewModel.AttributeBonus.Add(new AttributeBonusViewModel());

            var appService = MockRepository.GenerateMock<IAttributeAppService>();

            appService.Stub(s => s.Add(viewModel)).Return(viewModel);

            viewModel = appService.Add(viewModel);
            var resultAppService = viewModel.ValidationResult;

            Assert.IsFalse(resultAppService.IsValid);
        }

        ////ARRANGE, ACT, ASSERT
        //[TestMethod]
        //public void AttributeAppService_Add_False()
        //{
        //    var obj1 = new AttributeViewModel()
        //    {
        //        AttributeName = "Test",
        //        AttributeMinimum = 1,
        //        AttributeType = AttributeType.Misc,
        //        AttributeDescription = "test"
        //    };

        //    var obj2 = new AttributeViewModel()
        //    {
        //        AttributeName = "Test",
        //        AttributeMinimum = 1,
        //        AttributeType = AttributeType.Misc,
        //        AttributeDescription = "test"
        //    };


        //    var appService = MockRepository.GenerateMock<IAttributeAppService>();
        //    appService.Stub(s => s.Add(obj1));
        //    appService.Stub(s => s.Add(obj2)).Return();

        //    var resultObj1 = obj1.ValidationResult;
        //    var resultObj2 = obj2.ValidationResult;

        //    Assert.IsTrue(resultObj1.IsValid);
        //    Assert.IsFalse(resultObj2.IsValid);
        //}
    }
}
