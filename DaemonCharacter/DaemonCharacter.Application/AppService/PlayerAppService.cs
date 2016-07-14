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

        public PlayerAppService(IPlayerService playerService, IUnitOfWork uow) : base(uow)
        {
            _playerService = playerService;
        }

        public PlayerViewModel Add(PlayerViewModel player)
        {
            var p = Mapper.Map<PlayerViewModel, Player>(player);
            return Mapper.Map<Player, PlayerViewModel>(_playerService.Add(p));
        }

        public void Dispose()
        {
            _playerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public PlayerViewModel Get(Guid? id)
        {
            return Mapper.Map<Player, PlayerViewModel>(_playerService.Get(id));
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
            return Mapper.Map<Player, PlayerViewModel>(_playerService.Update(p));
        }
    }
}
