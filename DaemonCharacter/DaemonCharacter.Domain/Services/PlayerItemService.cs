using DaemonCharacter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Services
{
    public class PlayerItemService : IPlayerItemService
    {

        private readonly IPlayerItemRespository _playerItemRespository;

        public PlayerItemService(IPlayerItemRespository playerItemRespository)
        {
            _playerItemRespository = playerItemRespository;
        }

        public PlayerItem Add(PlayerItem model)
        {
            if (!model.IsValid())
                return model;

            //TODO: MAKE VALIDATIONS AND SPECIFICATIONS

            model.ValidationResult.Message = "Item added to player successfully";
            return _playerItemRespository.Add(model);
        }

        public void Dispose()
        {
            _playerItemRespository.Dispose();
            GC.SuppressFinalize(this);
        }

        public PlayerItem Get(Guid id)
        {
            return _playerItemRespository.Get(id);
        }

        public IEnumerable<PlayerItem> ListAll()
        {
            return _playerItemRespository.ListAll();
        }

        public IEnumerable<PlayerItem> ListFromPlayer(Guid CharacterId)
        {
            return _playerItemRespository.ListFromPlayer(CharacterId);
        }

        public void Remove(Guid id)
        {
            _playerItemRespository.Remove(id);
        }

        public PlayerItem Update(PlayerItem model)
        {
            if (!model.IsValid())
                return model;

            //TODO: MAKE VALIDATIONS AND SPECIFICATIONS

            model.ValidationResult.Message = "Item added to player successfully";
            return _playerItemRespository.Update(model);

        }
    }
}
