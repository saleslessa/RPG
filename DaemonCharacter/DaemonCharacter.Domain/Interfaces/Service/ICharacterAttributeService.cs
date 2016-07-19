using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface ICharacterAttributeService : IDisposable
    {
        CharacterAttribute Add(CharacterAttribute model);

        void Remove(Guid id);

        IEnumerable<CharacterAttribute> ListFromCharacter(Guid? CharacterId);

        IEnumerable<CharacterAttribute> ListFromAttribute(Guid? AttributeId);

        CharacterAttribute Get(Guid? CharacterId, Guid? AttributeId);

        IEnumerable<CharacterAttribute> ListAllAttributes();

        void RemoveFromCharacter(Guid CharacterId);

    }
}
