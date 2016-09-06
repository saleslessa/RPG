using DaemonCharacter.Application.ViewModels.Item;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IItemAppService : IDisposable
    {
        ItemViewModel Add(ItemViewModel model);

        ItemViewModel Get(Guid? id);

        IEnumerable<ItemViewModel> ListAll();

        ItemViewModel Update(ItemViewModel model);

        IEnumerable<ItemAttributeViewModel> ListAvailableForBonus(Guid? itemId);

        void Remove(Guid id);
    }
}
