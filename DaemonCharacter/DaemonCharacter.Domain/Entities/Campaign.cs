using System;

namespace DaemonCharacter.Domain.Entities
{
    public class Campaign
    {
        public Guid CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string CampaignShortDescription { get; set; }

        public string CampaignBriefing { get; set; }

        public int CampaignStartYear { get; set; }

        public int CampaignMaxPlayers { get; set; }

        public int CampaignRemainingPlayers { get; set; }

        public byte[] CampaignImg { get; set; }

        public CampaignStatus CampaignStatus { get; set; }

        public virtual Users CampaignUserMaster { get; set; }

        public Campaign()
        {
            CampaignId = new Guid();
        }

    }

}