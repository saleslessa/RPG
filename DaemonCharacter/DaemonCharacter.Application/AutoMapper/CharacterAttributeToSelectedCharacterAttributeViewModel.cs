using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaemonCharacter.Application.AutoMapper
{
    public class CharacterAttributeToSelectedCharacterAttributeViewModel
    {
        private readonly IAttributeService _attributeService;
        private readonly IPlayerService _playerService;
        private readonly ICharacterAttributeService _characterAttributeService;
        private readonly IPlayerItemAppService _playerItemAppService;

        public CharacterAttributeToSelectedCharacterAttributeViewModel(IAttributeService attributeService, IPlayerService playerService
            , ICharacterAttributeService characterAttributeService, IPlayerItemAppService playerItemAppService)
        {
            _attributeService = attributeService;
            _playerService = playerService;
            _characterAttributeService = characterAttributeService;
            _playerItemAppService = playerItemAppService;
        }

        public IEnumerable<SelectedCharacterAttributeViewModel> Map(IEnumerable<CharacterAttribute> model)
        {
            var result = new List<SelectedCharacterAttributeViewModel>();

            foreach (var item in model)
            {
                var obj = new SelectedCharacterAttributeViewModel()
                {
                    AttributeId = item.Attribute.AttributeId,
                    AttributeName = item.Attribute.AttributeName,
                    CharacterId = item.Character.CharacterId,
                    Value = item.CharacterAttributeValue,
                    AttributeType = item.Attribute.AttributeType,
                    AttributeDescription = item.Attribute.AttributeDescription,
                    BonusValue = _characterAttributeService.GetTotalBonus(item.Character.CharacterId, item.Attribute.AttributeId)
                };

                var BonusFromUsedItems = _playerItemAppService.ListBonusOfAllUsedItemsFromAttribute(obj.CharacterId, obj.AttributeId);
                foreach (var bonus in BonusFromUsedItems)
                {
                    obj.BonusValue += bonus.Value;
                }

                obj.TotalValue = obj.Value + obj.BonusValue;

                result.Add(obj);
            }

            return result;
        }

        public async Task<IEnumerable<SelectedCharacterAttributeViewModel>> MapAsync(IEnumerable<CharacterAttribute> model)
        {
            return await Task.Run(() => Map(model));
        }
    }
}
