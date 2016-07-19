using DaemonCharacter.Domain.Entities;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        IEnumerable<Campaign> ListAvailableCampaigns();

    }
}
