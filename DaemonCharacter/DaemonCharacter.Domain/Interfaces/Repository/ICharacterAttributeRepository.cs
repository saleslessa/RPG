using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface ICharacterAttributeRepository : IRepository<CharacterAttributes>
    {
        IEnumerable<CharacterAttributes> ListFromCharacter(Guid? CharacterId);

        IEnumerable<CharacterAttributes> ListFromAttribute(Guid? AttributeId);

        CharacterAttributes Get(Guid? CharacterId, Guid? AttributeId);

    }
}
