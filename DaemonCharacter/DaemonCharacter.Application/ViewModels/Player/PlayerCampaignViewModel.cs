using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Player
{
    public class PlayerCampaignViewModel
    {
        public PlayerCampaignViewModel()
        {
            CampaignId = Guid.NewGuid();
        }

        [Key]
        public Guid CampaignId { get; set; }

        [DisplayName("Campaign")]
        public string CampaignName { get; set; }

        [DisplayName("Description")]
        public string CampaignShortDescription { get; set; }

        [DisplayName("Remaining players")]
        public int CampaignRemainingPlayers { get; set; }

        [DisplayName("Start year")]
        public int CampaignStartYear { get; set; }
    }
}
