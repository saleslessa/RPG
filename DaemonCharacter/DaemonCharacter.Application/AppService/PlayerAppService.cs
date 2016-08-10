using DaemonCharacter.Application.Interfaces;
using System;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Entities;
using System.Collections.Generic;
using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using AutoMapper;
using DaemonCharacter.Application.AutoMapper;

namespace DaemonCharacter.Application.AppService
{
    public class PlayerAppService : ApplicationService, IPlayerAppService
    {

        private readonly IPlayerService _playerService;
        private readonly ICampaignService _campaignService;
        private readonly ICharacterAttributeService _characterAttributeService;
        private readonly IPlayerItemService _playerItemService;
        private readonly IItemService _itemService;
        private readonly IAttributeService _attributeService;

        public PlayerAppService(IPlayerService playerService, ICampaignService campaignService, IPlayerItemService playerItemService
            , ICharacterAttributeService characterAttributeService, IItemService itemService, IAttributeService attributeService
            , IUnitOfWork uow) : base(uow)
        {
            _playerService = playerService;
            _campaignService = campaignService;
            _characterAttributeService = characterAttributeService;
            _playerItemService = playerItemService;
            _itemService = itemService;
            _attributeService = attributeService;
        }

        public PlayerViewModel Add(PlayerViewModel model)
        {
            var player = Mapper.Map<PlayerViewModel, Player>(model);
            player.Campaign = _campaignService.Get(model.SelectedCampaignId);

            player = _playerService.Add(player);

            if (!player.ValidationResult.IsValid)
                return Mapper.Map<Player, PlayerViewModel>(player);

            foreach (var attribute in model.SelectedAttributes)
            {
                attribute.CharacterId = player.CharacterId;
                _characterAttributeService.Add(new SelectedCharacterAttributeViewModelToCharacterAttribute(_attributeService, _playerService).Map(attribute));
            }

            foreach (var item in model.SelectedItems)
            {
                item.CharacterId = player.CharacterId;
                _playerItemService.Add(new SelectedPlayerItemViewModelToPlayerItem(_itemService, _playerService).Map(item));
            }

            Commit();

            return Mapper.Map<Player, PlayerViewModel>(player);
        }

        public void Dispose()
        {
            _playerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public PlayerViewModel Get(Guid? id)
        {
            var Player = _playerService.Get(id);
            var result = Mapper.Map<Player, PlayerViewModel>(Player);
            result.SelectedCampaignId = Player.Campaign.CampaignId;
            result.SelectedCampaign = Player.Campaign;

            result.SelectedItems = new PlayerItemToSelectedPlayerItemViewModel().Map(_playerItemService.ListFromPlayer(result.CharacterId));

            result.SelectedAttributes = new CharacterAttributeToSelectedCharacterAttributeViewModel(_attributeService, _playerService)
                .Map(_characterAttributeService.ListFromCharacter(result.CharacterId));

            return result;
        }

        public IEnumerable<PlayerViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<Player>, IEnumerable<PlayerViewModel>>(_playerService.ListAll());
        }

        public void Remove(Guid id)
        {
            _playerService.Remove(id);
            Commit();
        }

        public PlayerViewModel Update(PlayerViewModel model)
        {
            var p = Mapper.Map<PlayerViewModel, Player>(model);
            p.Campaign = model.SelectedCampaign;
            var result = _playerService.Update(p);

            Commit();

            return Mapper.Map<Player, PlayerViewModel>(result);
        }
    }
}
