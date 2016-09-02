using DaemonCharacter.Application.ViewModels.PlayerItem;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IPlayerItemAppService : IDisposable
    {
        PlayerItemViewModel Add(PlayerItemViewModel model);

        PlayerItemViewModel Update(PlayerItemViewModel model);

        IEnumerable<PlayerItemViewModel> ListAll();

        PlayerItemViewModel Get(Guid id);

        void Remove(Guid id);

        IEnumerable<PlayerItemViewModel> ListFromPlayer(Guid CharacterId);

        Dictionary<string, int> ListBonusOfAllUsedItemsFromAttribute(Guid CharacterId, Guid AttributeId);
    }
}
