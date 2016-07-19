using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface INonPlayerService : IDisposable
    {

        NonPlayer Add(NonPlayer model);

        void Remove(Guid id);

        IEnumerable<NonPlayer> ListAll();

        NonPlayer GetByName(string name);

        NonPlayer Update(NonPlayer model);

    }
}
