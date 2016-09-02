using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IPlayerItemService : IDisposable
    {
        PlayerItem Add(PlayerItem model);

        PlayerItem Update(PlayerItem model);

        IEnumerable<PlayerItem> ListAll();

        PlayerItem Get(Guid id);

        void Remove(Guid id);

        IEnumerable<PlayerItem> ListFromPlayer(Guid CharacterId);
    }
}
