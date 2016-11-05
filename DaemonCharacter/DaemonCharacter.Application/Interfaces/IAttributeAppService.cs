using DaemonCharacter.Application.ViewModels.Attribute;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IAttributeAppService : IDisposable
    {
        AttributeViewModel Add(AttributeViewModel model);

        AttributeViewModel Get(Guid? AttributeId);

        IEnumerable<AttributeViewModel> ListAll();

        AttributeViewModel Update(AttributeViewModel attributeViewModel);

        void Remove(Guid attributeId);

        List<AttributeBonusViewModel> ListAvailableForBonus(Guid? SelectedAttribute);

        List<AttributeViewModel> ListWithPagination(int skip, int take);

        List<AttributeViewModel> SearchByNameWithPagination(int skip, int take, string name);

        List<AttributeViewModel> SearchByName(string name);

        List<AttributeViewModel> SearchByAttributeType(AttributeType? type);

    }
}
