using DaemonCharacter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Validations.Player;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Player Add(Player _player)
        {
            _player.ValidationResult = new CreatePlayerValidation(_playerRepository).Validate(_player);
            if (!_player.ValidationResult.IsValid)
                return _player;

            _player.ValidationResult.Message = "Player created successfully";
            return _playerRepository.Add(_player);
        }

        public void Dispose()
        {
            _playerRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Player Get(Guid? id)
        {
            return _playerRepository.Get(id);
        }

        public IEnumerable<Player> ListAll()
        {
            return _playerRepository.ListAll();
        }

        public void Remove(Guid id)
        {
            _playerRepository.Remove(id);
        }

        public IEnumerable<Player> SearchByName(string name)
        {
            return _playerRepository.Search(p => p.CharacterName.ToUpper().Trim() == name.ToUpper().Trim());
        }

        public Player Update(Player _player)
        {
            _player.ValidationResult = new UpdatePlayerValidation(_playerRepository).Validate(_player);
            if (!_player.ValidationResult.IsValid)
                return _player;

            return _playerRepository.Update(_player);
        }
    }
}
