using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class CharacterAttributeRepository : Repository<CharacterAttributes>, ICharacterAttributeRepository
    {
        public CharacterAttributeRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public CharacterAttributes Get(Guid? CharacterId, Guid? AttributeId)
        {
            return Search(t => t.Character.CharacterId == CharacterId && t.Attribute.AttributeId == AttributeId)
                .FirstOrDefault();

        }

        public IEnumerable<CharacterAttributes> ListFromAttribute(Guid? AttributeId)
        {
            return Search(t => t.Attribute.AttributeId == AttributeId)
                .ToList();
        }

        public IEnumerable<CharacterAttributes> ListFromCharacter(Guid? CharacterId)
        {
            return Search(t => t.Character.CharacterId == CharacterId)
                .ToList();
        }
    }
}
