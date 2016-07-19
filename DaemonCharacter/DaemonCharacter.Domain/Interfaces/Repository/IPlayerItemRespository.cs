using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IPlayerItemRespository : IRepository<PlayerItem>
    {
        IEnumerable<PlayerItem> ListFromPlayer(Guid id);



    }
}
