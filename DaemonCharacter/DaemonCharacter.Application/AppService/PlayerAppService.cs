﻿using DaemonCharacter.Application.Interfaces;
using System;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Application.ViewModels.Player;
using DaemonCharacter.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DaemonCharacter.Application.AutoMapper;
using DaemonCharacter.Application.ViewModels.Attribute;
using System.Threading.Tasks;

namespace DaemonCharacter.Application.AppService
{
    public class PlayerAppService : ApplicationService, IPlayerAppService
    {

        private readonly IPlayerService _playerService;
        private readonly ICampaignService _campaignService;
        private readonly ICharacterAttributeService _characterAttributeService;
        private readonly IPlayerItemService _playerItemService;
        private readonly IPlayerItemAppService _playerItemAppService;
        private readonly IItemService _itemService;
        private readonly IAttributeService _attributeService;

        public PlayerAppService(IPlayerService playerService, ICampaignService campaignService, IPlayerItemService playerItemService
            , ICharacterAttributeService characterAttributeService, IItemService itemService, IPlayerItemAppService playerItemAppService, IAttributeService attributeService
            , IUnitOfWork uow) : base(uow)
        {
            _playerService = playerService;
            _campaignService = campaignService;
            _characterAttributeService = characterAttributeService;
            _playerItemService = playerItemService;
            _itemService = itemService;
            _attributeService = attributeService;
            _playerItemAppService = playerItemAppService;
        }

        public PlayerViewModel Add(PlayerViewModel model)
        {
            var player = Mapper.Map<PlayerViewModel, Player>(model);
            player.Campaign = _campaignService.Get(model.SelectedCampaignId);

            player = _playerService.Add(player);

            if (!player.ValidationResult.IsValid)
                return Mapper.Map<Player, PlayerViewModel>(player);

            player.PlayerMoney = player.PlayerMoney -
                                 model.SelectedItems.Sum(s => s.PlayerItemQtd * s.PlayerItemUnitPrice);

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

            result.SelectedAttributes = new CharacterAttributeToSelectedCharacterAttributeViewModel(_attributeService, _playerService, _characterAttributeService, _playerItemAppService)
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

        public PlayerViewModel GetBasicInfo(Guid? id)
        {
            var Player = _playerService.Get(id);
            var result = Mapper.Map<Player, PlayerViewModel>(Player);
            result.SelectedCampaignId = Player.Campaign.CampaignId;
            result.SelectedCampaign = Player.Campaign;

            return result;
        }

        public async Task<IEnumerable<SelectedCharacterAttributeViewModel>> GetAttributesAsync(Guid id)
        {
            return await new CharacterAttributeToSelectedCharacterAttributeViewModel(_attributeService, _playerService, _characterAttributeService, _playerItemAppService)
                .MapAsync(_characterAttributeService.ListFromCharacter(id));
        }

        public async Task<IEnumerable<SelectedPlayerItemViewModel>> GetItemsAsync(Guid id)
        {
            return await new PlayerItemToSelectedPlayerItemViewModel().MapAsync(_playerItemService.ListFromPlayer(id));
        }

        public IEnumerable<SelectedCharacterAttributeViewModel> GetAttributes(Guid id)
        {
            return new CharacterAttributeToSelectedCharacterAttributeViewModel(_attributeService, _playerService, _characterAttributeService, _playerItemAppService)
                 .Map(_characterAttributeService.ListFromCharacter(id));
        }

        public IEnumerable<SelectedPlayerItemViewModel> GetItems(Guid id)
        {
            return new PlayerItemToSelectedPlayerItemViewModel().Map(_playerItemService.ListFromPlayer(id));
        }

        public PlayerViewModel ChangePlayerField(Guid id, string field, string value)
        {

            lock (_playerService)
            {

                //var player = new Player() { CharacterId = id };
                var player = _playerService.Get(id);

                try
                {
                    switch (field)
                    {
                        case "PlayerAge":
                            player.PlayerAge = int.Parse(value);
                            _playerService.ChangePlayerAge(player);
                            break;
                        case "PlayerExperience":
                            player.PlayerExperience = int.Parse(value);
                            _playerService.ChangePlayerExperience(player);
                            break;
                        case "CharacterMaxLife":
                            if(int.Parse(value) < player.CharacterRemainingLife) throw new Exception("Cannot change max life to lower than remaining");
                            player.CharacterMaxLife = int.Parse(value);
                            _playerService.ChangeCharacterMaxLife(player);
                            break;
                        case "PlayerMoney":
                            player.PlayerMoney = int.Parse(value);
                            _playerService.ChangePlayerMoney(player);
                            break;
                        case "PlayerLevel":
                            player.PlayerLevel = int.Parse(value);
                            _playerService.ChangePlayerLevel(player);
                            break;
                        case "CharacterRemainingLife":
                            player.CharacterRemainingLife = int.Parse(value);
                            _playerService.ChangeCharacterRemainingLife(player);
                            break;
                        case "PrivateAnnotations":
                            player.PrivateAnnotations = value;
                            _playerService.ChangePrivateAnnotations(player);
                            break;
                        default:
                            throw new Exception("Invalid field to update");
                    }
                    Commit();

                    return Mapper.Map<Player, PlayerViewModel>(player);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
