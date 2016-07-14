using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public IEnumerable<Campaign> ListAvailableCampaigns()
        {
            return Search(t => t.CampaignRemainingPlayers > 0);
        }

        public void Remove(Guid? id)
        {
            Remove(id);
        }
    }
}
