using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface ICampaignService : IDisposable
    {
        Campaign Add(Campaign c);

        Campaign Get(Guid? id);

        IEnumerable<Campaign> ListAll();

        Campaign Update(Campaign c);

        void Remove(Guid id);

    }
}
