using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IPlayerSessionRepository : IRepository<PlayerSessions>
    {
        PlayerSessions Get(Guid? PlayerId, Guid? SessionId);

        IEnumerable<PlayerSessions> ListFromPlayer(Guid? PlayerId);

        IEnumerable<PlayerSessions> ListFromSession(Guid? SessionId);
    }
}
