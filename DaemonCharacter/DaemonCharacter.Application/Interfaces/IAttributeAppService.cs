using DaemonCharacter.Application.ViewModels.Attribute;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IAttributeAppService : IDisposable
    {
        AttributeViewModel Add(AttributeViewModel _AttributeViewModel);

        AttributeViewModel Get(Guid? AttributeId);

        IEnumerable<AttributeViewModel> ListAll();

        AttributeViewModel Update(AttributeViewModel _AttributeViewModel);

        void Remove(Guid AttributeId);

        List<AttributeBonusViewModel> ListAvailableForBonus(Guid? SelectedAttribute);

        List<AttributeViewModel> ListWithPagination(int skip, int take);

        List<AttributeViewModel> SearchByNameWithPagination(int skip, int take, string name);

    }
}
