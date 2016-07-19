using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;
using System.Linq;

namespace DaemonCharacter.Domain.Services
{
    public class CharacterAttributeService : ICharacterAttributeService
    {
        private readonly ICharacterAttributeRepository _characterAttributeRepository;
        private readonly IAttributeRepository _attributeRepository;

        public CharacterAttributeService(ICharacterAttributeRepository characterAttributeRepository, IAttributeRepository attributeRepository)
        {
            _characterAttributeRepository = characterAttributeRepository;
            _attributeRepository = attributeRepository;
        }

        public CharacterAttribute Add(CharacterAttribute model)
        {
            if (!model.IsValid())
                return model;

            return _characterAttributeRepository.Add(model);
        }

        public void Dispose()
        {
            _characterAttributeRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public CharacterAttribute Get(Guid? CharacterId, Guid? AttributeId)
        {
            return _characterAttributeRepository.Get(CharacterId, AttributeId);
        }

        public IEnumerable<CharacterAttribute> ListAllAttributes()
        {
            var att = _attributeRepository.ListAll().OrderBy(o => o.AttributeType);
            var result = new List<CharacterAttribute>();
            

            foreach (var item in att)
            {
                result.Add(new CharacterAttribute()
                {
                    Attribute = item
                });
            }

            return result;
        }

        public IEnumerable<CharacterAttribute> ListFromAttribute(Guid? AttributeId)
        {
            return _characterAttributeRepository.ListFromAttribute(AttributeId);
        }

        public IEnumerable<CharacterAttribute> ListFromCharacter(Guid? CharacterId)
        {
            return _characterAttributeRepository.ListFromCharacter(CharacterId);
        }

        public void Remove(Guid id)
        {
            _characterAttributeRepository.Remove(id);
        }

        public void RemoveFromCharacter(Guid CharacterId)
        {
            var list = ListFromCharacter(CharacterId);

            foreach (var item in list)
            {
                Remove(item.CharacterAttributeId);
            }
        }
    }
}
