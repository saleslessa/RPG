using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Application.ViewModels.PlayerItem;
using DaemonCharacter.Domain.Interfaces.Service;
using AutoMapper;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AppService
{
    public class PlayerItemAppService : ApplicationService, IPlayerItemAppService
    {
        private readonly IPlayerItemService _playerItemService;

        public PlayerItemAppService(IPlayerItemService playerItemService, IUnitOfWork uow) : base(uow)
        {
            _playerItemService = playerItemService;
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
