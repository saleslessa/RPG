using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Campaign
{
    public class CampaignViewModel
    {
        public CampaignViewModel()
        {
            CampaignId = Guid.NewGuid();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
        }

        [Key]
        public Guid CampaignId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage ="The name of campaign is obrigatory. Plase chose one")]
        [StringLength(50, ErrorMessage ="The length of campaign's name must be between 5 and 50", MinimumLength =5)]
        [DataType(DataType.Text)]
        public string CampaignName { get; set; }

        [DisplayName("Short Description")]
        [Required(ErrorMessage = "The description of campaign is obrigatory. Plase chose one")]
        [StringLength(500, ErrorMessage = "The length of campaign's description must be between 5 and 500", MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        public string CampaignShortDescription { get; set; }

        [DisplayName("Briefing")]
        [StringLength(500, ErrorMessage = "The maximum length of campaign's briefing is 500")]
        [DataType(DataType.MultilineText)]
        public string CampaignBriefing { get; set; }

        [DisplayName("Start Year")]
        [Range(short.MinValue, short.MaxValue, ErrorMessage ="Start year must be between {0} and {1}")]
        public short CampaignStartYear { get; set; }

        [DisplayName("Max Players")]
        [Required(ErrorMessage = "This field is obrigatory. Please select a value")]
        [Range(1, 10, ErrorMessage ="Maximum players must be between {0} and {1}")]
        public short CampaignMaxPlayers { get; set; }

        [DisplayName("Image")]
        [DataType(DataType.ImageUrl)]
        public byte[] CampaignImg { get; set; }

        [DisplayName("Status")]
        [DefaultValue(CampaignStatus.Beginning)]
        [Required(ErrorMessage = "This field is obrigatory. Please select a value")]
        public CampaignStatus CampaignStatus { get; set; }

        [ScaffoldColumn(false)]
        public string CampaignUserMaster { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }
    }
}
