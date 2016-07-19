using DaemonCharacter.Application.ViewModels.Player;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IPlayerAppService : IDisposable
    {
        PlayerViewModel Add(PlayerViewModel player);

        PlayerViewModel Update(PlayerViewModel player);

        void Remove(Guid id);

        PlayerViewModel Get(Guid? id);

        IEnumerable<PlayerViewModel> ListAll();

        
    }
}
