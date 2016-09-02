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
        private readonly IAttributeService _attributeService;

        public CharacterAttributeService(ICharacterAttributeRepository characterAttributeRepository, IAttributeRepository attributeRepository, IAttributeService attributeService)
        {
            _characterAttributeRepository = characterAttributeRepository;
            _attributeRepository = attributeRepository;
            _attributeService = attributeService;
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

        public int GetTotalBonus(Guid CharacterId, Guid AttributeId)
        {
            var listOfAttributesRelatedToThisAttribute = _attributeService.ListBonusAttribute(AttributeId);
            var result = 0;

            foreach (var item in listOfAttributesRelatedToThisAttribute)
            {
                var selectedItem = _characterAttributeRepository.Get(CharacterId, item.AttributeId);
                result += selectedItem == null ? 0 : selectedItem.CharacterAttributeValue;
            }

            return result;
        }

        public Dictionary<string, int> GetTotalBonusAttributes(Guid CharacterId, Guid AttributeId)
        {
            var listOfAttributesRelatedToThisAttribute = _attributeService.ListBonusAttributeIds(AttributeId);
            var result = new Dictionary<string, int>();

            foreach (var item in listOfAttributesRelatedToThisAttribute)
            {
                var selectedItem = _characterAttributeRepository.Get(CharacterId, item);

                if (selectedItem != null)
                    result.Add(selectedItem.Attribute.AttributeName, selectedItem.CharacterAttributeValue);
            }

            return result;
        }

        public CharacterAttribute Update(CharacterAttribute model)
        {
            if (!model.IsValid())
                return model;

            return _characterAttributeRepository.Update(model);
        }
    }
}
