using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IPlayerService : IDisposable
    {
        Player Add(Player _player);

        Player Update(Player _player);

        IEnumerable<Player> ListAll();

        Player Get(Guid? id);

        void Remove(Guid id);
         
        IEnumerable<Player> SearchByName(string name);

    }
}
