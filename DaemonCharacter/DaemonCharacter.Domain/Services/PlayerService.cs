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
        private readonly ICharacterRepository _characterRepository;

        public PlayerService(IPlayerRepository playerRepository, ICharacterRepository characterRepository)
        {
            _playerRepository = playerRepository;
            _characterRepository = characterRepository;
        }

        public Player Add(Player _player)
        {
            if (!_player.IsValid())
                return _player;

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

        public void ChangePlayerAge(Player p)
        {
            _playerRepository.ChangePlayerAge(p);
            p.ValidationResult.Message = "Age updated successfully";
        }

        public void ChangePlayerExperience(Player p)
        {
            _playerRepository.ChangePlayerExperience(p);
            p.ValidationResult.Message = "Experience updated successfully";
        }

        public Player Update(Player _player)
        {
            if (!_player.IsValid()) return _player;
            _player.ValidationResult = new UpdatePlayerValidation(_characterRepository).Validate(_player);
            return (!_player.ValidationResult.IsValid) ? _player : _playerRepository.Update(_player);
        }

        public void ChangeCharacterMaxLife(Player p)
        {
            _playerRepository.ChangeCharacterMaxLife(p);
            p.ValidationResult.Message = "Max Life updated successfully";
        }

        public void ChangePlayerMoney(Player p)
        {
            _playerRepository.ChangePlayerMoney(p);
            p.ValidationResult.Message = "Money updated successfully";
        }

        public void ChangePlayerLevel(Player p)
        {
            _playerRepository.ChangePlayerLevel(p);
            p.ValidationResult.Message = "Level updated successfully";
        }

        public void ChangeCharacterRemainingLife(Player p)
        {
            _playerRepository.ChangeCharacterRemainingLife(p);
            p.ValidationResult.Message = "Life updated successfully";
        }

        public void ChangePrivateAnnotations(Player p)
        {
            _playerRepository.ChangePrivateAnnotations(p);
            p.ValidationResult.Message = "Private Annotations updated successfully";
        }
    }
}
