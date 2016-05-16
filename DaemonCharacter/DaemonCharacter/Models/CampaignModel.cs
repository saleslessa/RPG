
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaemonCharacter.Models
{
    [Table("tb_campaign")]
    public class CampaignModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage ="Campaign name is required. Please chose one")]
        [Display(Name ="Campaign Name"), DataType(DataType.Text), MaxLength(50)]
        public string name { get; set; }

        [Display(Name = "Short Description"), DataType(DataType.MultilineText), MaxLength(255)]
        public string shortDescription { get; set; }

        [Required(ErrorMessage = "Every campaign must have a briefing. Please use your imagination to create one :)")]
        [Display(Name = "Campaign Briefing"), DataType(DataType.MultilineText), MaxLength(5000)]
        public string briefing { get; set; }

        [Display(Name = "Start Year"), DefaultValue(0)]
        public int startYear { get; set; }

        [Required, Range(1, 7), DefaultValue(2)]
        [Display(Name="Maximum participants")]
        public int maxPlayers { get; set; }

        [Required, DefaultValue(0)]
        [Display(Name = "Remaining participants")]
        public int remainingPlayers { get; set; }

        [Display(Name = "Campaign image"), DataType(DataType.ImageUrl)]
        public byte[] img { get; set; }

        [Required]
        public int idMaster { get; set; }

        [ForeignKey("idMaster")]
        public UserProfileModel userMaster { get; set; }

        public virtual List<CharacterModel> userCharacters { get; set; }
    }

    public class AvailableCampaignsModel
    {
        [Required]
        public int id { get; set; }

        [Display(Name = "Campaign Name"), DataType(DataType.Text), MaxLength(50)]
        public string name { get; set; }

        [Display(Name = "Short Description"), DataType(DataType.MultilineText), MaxLength(255)]
        public string shortDescription { get; set; }

        [Required]
        [Display(Name = "Remaining participants")]
        public int remainingPlayers { get; set; }

        [Required]
        public int idMaster { get; set; }

        [ForeignKey("idMaster")]
        public virtual UserProfileModel userMaster { get; set; }

    }
}