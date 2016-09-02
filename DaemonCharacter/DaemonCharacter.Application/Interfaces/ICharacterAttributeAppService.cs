using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface ICharacterAttributeAppService  : IDisposable
    {
        CharacterAttributeViewModel Add(CharacterAttributeViewModel model);

        IEnumerable<CharacterAttributeViewModel> ListFromCharacter(Guid CharacterId);

        void RemoveFromCharacter(Guid CharacterId);

        void Remove(Guid CharacterAttributeId);

        IEnumerable<CharacterAttributeViewModel> ListAllAttributes();

        int GetTotalBonus(Guid? CharacterId, Guid? AttributeId);

        Dictionary<string, int> GetTotalBonusAttributes(Guid? CharacterId, Guid? AttributeId);

        void SetValue(Guid CharacterId, Guid AttributeId, int Value);

    }
}
