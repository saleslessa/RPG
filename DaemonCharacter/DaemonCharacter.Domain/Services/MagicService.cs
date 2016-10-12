using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Services
{
    public class MagicService : IMagicService
    {
        private readonly IMagicRepository _magicRepository;

        public MagicService(IMagicRepository magicRepository)
        {
            _magicRepository = magicRepository;
        }

        public void Dispose()
        {
            _magicRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Magic Add(Magic model)
        {
            throw new NotImplementedException();
        }

        public Magic Get(Guid id)
        {
            return _magicRepository.Get(id);
        }

        public IEnumerable<Magic> ListAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Magic> Search(Expression<Func<Magic, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Magic> SearchByName(string name)
        {
            throw new NotImplementedException();
        }

        public Magic Update(Magic model)
        {
            throw new NotImplementedException();
        }
    }
}
