using DaemonCharacter.Application.Interfaces;
using System;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Application.ViewModels.Player;
using AutoMapper;
using DaemonCharacter.Domain.Entities;
using System.Collections.Generic;

namespace DaemonCharacter.Application.AppService
{
    public class PlayerAppService : ApplicationService, IPlayerAppService
    {

        private readonly IPlayerService _playerService;
        private readonly ICampaignService _campaignService;

        public PlayerAppService(IPlayerService playerService, ICampaignService campaignService, IUnitOfWork uow) : base(uow)
        {
            _playerService = playerService;
            _campaignService = campaignService;
        }

        public PlayerViewModel Add(PlayerViewModel player)
        {
            var p = Mapper.Map<PlayerViewModel, Player>(player);
            p.Campaign = _campaignService.Get(player.SelectedCampaign);

            var result = _playerService.Add(p);

            Commit();

            return Mapper.Map<Player, PlayerViewModel>(result);
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
            result.SelectedCampaign = Player.Campaign.CampaignId;

            return result;
        }

        public IEnumerable<PlayerViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<Player>, IEnumerable<PlayerViewModel>>(_playerService.ListAll());
        }

        public void Remove(Guid id)
        {
            _playerService.Remove(id);
        }

        public PlayerViewModel Update(PlayerViewModel player)
        {
            var p = Mapper.Map<PlayerViewModel, Player>(player);
            p.Campaign = _campaignService.Get(player.SelectedCampaign);
            var result = _playerService.Update(p);

            Commit();

            return Mapper.Map<Player, PlayerViewModel>(result);
        }
    }
}
