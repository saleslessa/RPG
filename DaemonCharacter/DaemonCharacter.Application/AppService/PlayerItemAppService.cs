using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Interfaces.Service;
using AutoMapper;
using DaemonCharacter.Domain.Entities;
using System.Linq;

namespace DaemonCharacter.Application.AppService
{
    public class PlayerItemAppService : ApplicationService, IPlayerItemAppService
    {
        private readonly IPlayerItemService _playerItemService;
        private readonly IItemAttributeService _itemAttributeService;

        public PlayerItemAppService(IPlayerItemService playerItemService, IItemAttributeService itemAttributeService, IUnitOfWork uow) : base(uow)
        {
            _playerItemService = playerItemService;
            _itemAttributeService = itemAttributeService;
        }

        public PlayerItemViewModel Add(PlayerItemViewModel model)
        {
            var playerItem = Mapper.Map<PlayerItemViewModel, PlayerItem>(model);

            playerItem = _playerItemService.Add(playerItem);

            if (!playerItem.ValidationResult.IsValid)
                return Mapper.Map<PlayerItem, PlayerItemViewModel>(playerItem);

            Commit();
            return Mapper.Map<PlayerItem, PlayerItemViewModel>(playerItem);
        }

        public void Dispose()
        {
            _playerItemService.Dispose();
            GC.SuppressFinalize(this);
        }

        public PlayerItemViewModel Get(Guid id)
        {
            return Mapper.Map<PlayerItem, PlayerItemViewModel>(_playerItemService.Get(id));
        }

        public IEnumerable<PlayerItemViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<PlayerItem>, IEnumerable<PlayerItemViewModel>>(_playerItemService.ListAll());
        }

        public Dictionary<string, int> ListBonusOfAllUsedItemsFromAttribute(Guid CharacterId, Guid AttributeId)
        {
            var result = new Dictionary<string, int>();
            var items = _playerItemService.ListFromPlayer(CharacterId)
                .Where(t => t.PlayerItemUsingItem == true)
                .ToList();

            foreach (var playerItem in items)
            {
                var bonus = _itemAttributeService.Get(playerItem.Item.ItemId, AttributeId);
                if (bonus != null)
                    result.Add("Item: " + playerItem.Item.ItemName, bonus.ItemAttributeValue);
            }

            return result;
        }

        public IEnumerable<PlayerItemViewModel> ListFromPlayer(Guid CharacterId)
        {
            return Mapper.Map<IEnumerable<PlayerItem>, IEnumerable<PlayerItemViewModel>>(_playerItemService.ListFromPlayer(CharacterId));
        }

        public void Remove(Guid id)
        {
            _playerItemService.Remove(id);
        }

        public PlayerItemViewModel Update(PlayerItemViewModel model)
        {
            var playerItem = Mapper.Map<PlayerItemViewModel, PlayerItem>(model);

            playerItem = _playerItemService.Update(playerItem);

            if (!playerItem.ValidationResult.IsValid)
                return Mapper.Map<PlayerItem, PlayerItemViewModel>(playerItem);

            Commit();
            return Mapper.Map<PlayerItem, PlayerItemViewModel>(playerItem);
        }
    }
}
