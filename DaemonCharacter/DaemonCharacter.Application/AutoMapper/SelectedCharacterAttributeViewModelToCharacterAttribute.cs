using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;

namespace DaemonCharacter.Application.AutoMapper
{
    public class SelectedCharacterAttributeViewModelToCharacterAttribute
    {
        private readonly IAttributeService _attributeService;
        private readonly IPlayerService _playerService;

        public SelectedCharacterAttributeViewModelToCharacterAttribute(IAttributeService attributeService, IPlayerService playerService)
        {
            _attributeService = attributeService;
            _playerService = playerService;
        }

        public CharacterAttribute Map(SelectedCharacterAttributeViewModel viewModel)
        {
            var result = new CharacterAttribute();

            result.Character = _playerService.Get(viewModel.CharacterId);
            result.Attribute = _attributeService.Get(viewModel.AttributeId);

            result.CharacterAttributeValue = viewModel.Value;

            return result;
        }
    }
}
