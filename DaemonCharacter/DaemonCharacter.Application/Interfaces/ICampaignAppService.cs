using DaemonCharacter.Application.ViewModels.Campaign;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Application.Interfaces
{
    public interface ICampaignAppService : IDisposable
    {
        CampaignViewModel Add(CampaignViewModel c);

        CampaignViewModel Get(Guid? id);

        IEnumerable<CampaignViewModel> ListAll();

        CampaignViewModel Update(CampaignViewModel c);

        void Remove(Guid id);

        IEnumerable<PlayerCampaignViewModel> ListAvailableCampaigns();

    }
}
