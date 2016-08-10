using DaemonCharacter.Application.ViewModels.PlayerItem;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;

namespace DaemonCharacter.Application.AutoMapper
{
    public class SelectedPlayerItemViewModelToPlayerItem
    {
        private readonly IItemService _itemService;
        private readonly IPlayerService _playerService;

        public SelectedPlayerItemViewModelToPlayerItem(IItemService itemService, IPlayerService playerService)
        {
            _itemService = itemService;
            _playerService = playerService;
        }

        public PlayerItem Map(SelectedPlayerItemViewModel ViewModel)
        {
            var result = new PlayerItem();

            result.Item = _itemService.Get(ViewModel.ItemId);
            result.Player = _playerService.Get(ViewModel.CharacterId);

            result.PlayerItemApprovedByMaster = ViewModel.PlayerItemApprovedByMaster;
            result.PlayerItemDateBought = ViewModel.PlayerItemDateBought;
            result.PlayerItemQtd = ViewModel.PlayerItemQtd;
            result.PlayerItemUnitPrice = ViewModel.PlayerItemUnitPrice;

            return result;
        }
    }
}
