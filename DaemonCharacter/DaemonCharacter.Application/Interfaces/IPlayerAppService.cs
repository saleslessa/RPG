using DaemonCharacter.Application.ViewModels.Player;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface IPlayerAppService : IDisposable
    {
        PlayerViewModel Add(PlayerViewModel model);

        PlayerViewModel Update(PlayerViewModel model);

        void Remove(Guid id);

        PlayerViewModel Get(Guid? id);

        IEnumerable<PlayerViewModel> ListAll();

    }
}
