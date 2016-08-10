using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class PlayerSessionRepository : Repository<PlayerSessions>, IPlayerSessionRepository
    {
        public PlayerSessionRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public PlayerSessions Get(Guid? CharacterId, Guid? SessionId)
        {
            return Search(t => t.Player.CharacterId == CharacterId && t.Session.SessionId == SessionId)
                .FirstOrDefault();
        }

        public IEnumerable<PlayerSessions> ListFromPlayer(Guid? CharacterId)
        {
            return Search(t => t.Player.CharacterId == CharacterId)
                .ToList();
        }

        public IEnumerable<PlayerSessions> ListFromSession(Guid? SessionId)
        {
            return Search(t => t.Session.SessionId == SessionId)
                .ToList();
        }
    }
}
