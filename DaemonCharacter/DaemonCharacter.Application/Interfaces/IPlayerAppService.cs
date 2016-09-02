using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Application.ViewModels.PlayerItem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IPlayerAppService : IDisposable
    {
        PlayerViewModel Add(PlayerViewModel model);

        PlayerViewModel Update(PlayerViewModel model);

        void Remove(Guid id);

        PlayerViewModel Get(Guid? id);

        PlayerViewModel GetBasicInfo(Guid? id);

        IEnumerable<PlayerViewModel> ListAll();

        Task<IEnumerable<SelectedCharacterAttributeViewModel>> GetAttributesAsync(Guid id);

        Task<IEnumerable<SelectedPlayerItemViewModel>> GetItemsAsync(Guid id);

        IEnumerable<SelectedCharacterAttributeViewModel> GetAttributes(Guid id);

        IEnumerable<SelectedPlayerItemViewModel> GetItems(Guid id);
    }
}
