using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public Campaign Add(Campaign c)
        {
            if (!c.IsValid())
                return c;

            c.ValidationResult.Message = "Campaign created successfully.";
            return _campaignRepository.Add(c);
        }

        public void Dispose()
        {
            _campaignRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Campaign Get(Guid? id)
        {
            return _campaignRepository.Get(id);
        }

        public IEnumerable<Campaign> ListAll()
        {
            return _campaignRepository.ListAll();
        }

        public IEnumerable<Campaign> ListAvailable()
        {
            return _campaignRepository.Search(c => c.CampaignStatus == CampaignStatus.Beginning && c.CampaignRemainingPlayers > 0);
        }

        public void Remove(Guid id)
        {
            _campaignRepository.Remove(id);
        }

        public Campaign Update(Campaign c)
        {
            if (!c.IsValid())
                return c;

            c.ValidationResult.Message = "Campaign updated successfully";
            return _campaignRepository.Update(c);
        }
    }
}
