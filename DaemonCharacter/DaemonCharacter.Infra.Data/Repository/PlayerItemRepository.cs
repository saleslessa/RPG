using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Context;
using System.Linq;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class PlayerItemRepository : Repository<PlayerItem>, IPlayerItemRespository
    {
        public PlayerItemRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public IEnumerable<PlayerItem> ListFromPlayer(Guid id)
        {
            return Search(p => p.Player.CharacterId == id).ToList();
        }
    }
}
