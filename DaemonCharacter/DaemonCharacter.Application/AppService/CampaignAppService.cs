﻿using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Application.ViewModels.Campaign;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Infra.Data.Interfaces;
using AutoMapper;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AppService
{
    public class CampaignAppService : ApplicationService, ICampaignAppService
    {

        private readonly ICampaignService _campaignService;

        public CampaignAppService(ICampaignService campaignService, IUnitOfWork uow)
            : base(uow)
        {
            _campaignService = campaignService;
        }

        public CampaignViewModel Add(CampaignViewModel c)
        {
            var campaign = Mapper.Map<CampaignViewModel, Campaign>(c);
            campaign.CampaignRemainingPlayers = campaign.CampaignMaxPlayers;

            return Mapper.Map<Campaign, CampaignViewModel>(_campaignService.Add(campaign));
        }

        public void Dispose()
        {
            _campaignService.Dispose();
            GC.SuppressFinalize(this);
        }

        public CampaignViewModel Get(Guid? id)
        {
            return Mapper.Map<Campaign, CampaignViewModel>(_campaignService.Get(id));
        }

        public IEnumerable<CampaignViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<Campaign>, IEnumerable<CampaignViewModel>>(_campaignService.ListAll());
        }

        public void Remove(Guid id)
        {
            _campaignService.Remove(id);
        }

        public CampaignViewModel Update(CampaignViewModel c)
        {
            return Mapper.Map<Campaign, CampaignViewModel>(
                _campaignService.Update(
                    Mapper.Map<CampaignViewModel, Campaign>(c)
                    )
                 );
        }
    }
}