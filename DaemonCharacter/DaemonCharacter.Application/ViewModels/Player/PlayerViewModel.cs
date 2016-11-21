using DaemonCharacter.Application.ViewModels.Campaign;
using DaemonCharacter.Application.ViewModels.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DaemonCharacter.Application.ViewModels.Player
{
    public class PlayerViewModel
    {
        [Key]
        public Guid CharacterId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "This field is required. Please select one.")]
        [MaxLength(50, ErrorMessage = "This field allows a maximum of 50 characters")]
        [MinLength(3, ErrorMessage = "Minimum required for this field is 3 characters")]
        public string CharacterName { get; set; }

        [Range(0, 999, ErrorMessage = "Max Life must be between 0 and 999")]
        [DisplayName("Max Life")]
        [Required(ErrorMessage = "This field required.")]
        public int CharacterMaxLife { get; set; }

        [DisplayName("Life")]
        public int CharacterRemainingLife { get; set; }

        [DisplayName("Race")]
        [Required(ErrorMessage = "This field is required")]
        public Races CharacterRace { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "This field is required")]
        public Genders CharacterGender { get; set; }

        [DisplayName("Level")]
        [Required(ErrorMessage = "This field is required")]
        [Range(1, 999, ErrorMessage = "This field must be between 1 and 999")]
        public int PlayerLevel { get; set; }

        [DisplayName("Age")]
        [Range(1, 9999, ErrorMessage = "This field must be between 1 and 9999")]
        public int PlayerAge { get; set; }

        [DisplayName("XP")]
        [Range(0, int.MaxValue, ErrorMessage = "This field must be between 0 and 99999")]
        public int PlayerExperience { get; set; }

        [DisplayName("Background")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, HtmlEncode = true)]
        [MaxLength(5000, ErrorMessage ="Maximum characters allowed for this field is 5000")]
        [DataType(DataType.MultilineText)]
        public string PlayerBackground { get; set; }

        [Range(0, 9999, ErrorMessage ="You mus distribute between 0 and 9999 points")]
        [DisplayName("Points to distribute")]
        [ScaffoldColumn(false)]
        public int PlayerPointsToDistribute { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Remaining points")]
        public int PlayerRemainingPoints { get; set; }

        [DisplayName("Money")]
        [DataType(DataType.Currency)]
        public int PlayerMoney { get; set; }

        [DisplayName("Image")]
        [DataType(DataType.ImageUrl)]
        public byte[] CharacterImage { get; set; }

        [DisplayName("Campaign")]
        public IEnumerable<PlayerCampaignViewModel> Campaigns { get; set; }

        [ScaffoldColumn(false)]
        public Guid SelectedCampaignId { get; set; }

        [DisplayName("Campaign")]
        public Domain.Entities.Campaign SelectedCampaign { get; set; }

        [ScaffoldColumn(false)]
        public string CharacterUser { get; set; }

        [DisplayName("Pvt Annotations")]
        public string PrivateAnnotations { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<SelectedCharacterAttributeViewModel> SelectedAttributes { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<SelectedPlayerItemViewModel> SelectedItems { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public PlayerViewModel()
        {
            CharacterId = Guid.NewGuid();
            Campaigns = new List<PlayerCampaignViewModel>();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
            SelectedAttributes = new List<SelectedCharacterAttributeViewModel>();
            SelectedItems = new List<SelectedPlayerItemViewModel>();
        }

    }
}
