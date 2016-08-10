using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace DaemonCharacter.Application.AutoMapper
{
    public class CharacterAttributeToSelectedCharacterAttributeViewModel
    {
        private readonly IAttributeService _attributeService;
        private readonly IPlayerService _playerService;

        public CharacterAttributeToSelectedCharacterAttributeViewModel(IAttributeService attributeService, IPlayerService playerService)
        {
            _attributeService = attributeService;
            _playerService = playerService;
        }

        public IEnumerable<SelectedCharacterAttributeViewModel> Map(IEnumerable<CharacterAttribute> model)
        {
            var result = new List<SelectedCharacterAttributeViewModel>();

            foreach (var item in model)
            {
                result.Add(new SelectedCharacterAttributeViewModel()
                {
                    AttributeId = item.Attribute.AttributeId,
                    AttributeName = item.Attribute.AttributeName,
                    CharacterId = item.Character.CharacterId,
                    Value = item.CharacterAttributeValue,
                    AttributeType = item.Attribute.AttributeType,
                    AttributeDescription = item.Attribute.AttributeDescription
                });

            }

            return result;
        }
    }
}
