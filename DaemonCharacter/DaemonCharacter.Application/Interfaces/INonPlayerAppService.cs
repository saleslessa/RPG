using DaemonCharacter.Application.ViewModels.NonPlayer;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface INonPlayerAppService : IDisposable
    {
        NonPlayerViewModel Add(NonPlayerViewModel model);

        NonPlayerViewModel Update(NonPlayerViewModel model);

        void Remove(Guid id);

        IEnumerable<NonPlayerViewModel> ListAll();

        NonPlayerViewModel GetByName(string name);
    }
}
