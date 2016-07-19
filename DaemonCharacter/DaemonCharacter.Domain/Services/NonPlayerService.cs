using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;
using System.Linq;

namespace DaemonCharacter.Domain.Services
{
    public class NonPlayerService : INonPlayerService
    {
        private INonPlayerRepository _nonPlayerRepository;

        public NonPlayerService(INonPlayerRepository nonPlayerRepository)
        {
            _nonPlayerRepository = nonPlayerRepository;
        }

        public NonPlayer Add(NonPlayer model)
        {
            if (!model.IsValid())
                return model;

            return _nonPlayerRepository.Add(model);
        }

        public void Dispose()
        {
            _nonPlayerRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public NonPlayer GetByName(string name)
        {
            return _nonPlayerRepository.Search(n => n.CharacterName.Trim().ToUpper() == name.Trim().ToUpper()).FirstOrDefault();
        }

        public IEnumerable<NonPlayer> ListAll()
        {
            return _nonPlayerRepository.ListAll();
        }

        public void Remove(Guid id)
        {
            _nonPlayerRepository.Remove(id);
        }

        public NonPlayer Update(NonPlayer model)
        {
            if (!model.IsValid())
                return model;

            return _nonPlayerRepository.Update(model);
        }
    }
}
