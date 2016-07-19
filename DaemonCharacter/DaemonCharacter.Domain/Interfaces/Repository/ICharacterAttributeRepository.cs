using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface ICharacterAttributeRepository : IRepository<CharacterAttribute>
    {
        IEnumerable<CharacterAttribute> ListFromCharacter(Guid? CharacterId);

        IEnumerable<CharacterAttribute> ListFromAttribute(Guid? AttributeId);

        CharacterAttribute Get(Guid? CharacterId, Guid? AttributeId);

    }
}
