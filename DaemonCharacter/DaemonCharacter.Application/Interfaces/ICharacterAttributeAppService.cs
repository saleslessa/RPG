using DaemonCharacter.Application.ViewModels.CharacterAttribute;
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

    }
}
