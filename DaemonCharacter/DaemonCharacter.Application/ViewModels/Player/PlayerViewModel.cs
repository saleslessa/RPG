using DaemonCharacter.Application.ViewModels.Campaign;
using DaemonCharacter.Application.ViewModels.CharacterAttribute;
using DaemonCharacter.Application.ViewModels.PlayerItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DaemonCharacter.Application.ViewModels.Player
{
    public class PlayerViewModel
    {
        public PlayerViewModel()
        {
            CharacterId = Guid.NewGuid();
            Campaigns = new List<PlayerCampaignViewModel>();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
        }

        [Key]
        public Guid CharacterId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "This field is required. Please select one.")]
        [MaxLength(50, ErrorMessage = "This field allows a maximum of {0} characters")]
        [MinLength(3, ErrorMessage = "Minimum required for this field is {0} characters")]
        public string CharacterName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Max Life must be between {0} and {1}")]
        [DisplayName("Max Life")]
        [Required(ErrorMessage = "This field required.")]
        public int CharacterMaxLife { get; set; }

        [ScaffoldColumn(false)]
        public int CharacterRemainingLife { get; set; }

        [DisplayName("Race")]
        [Required(ErrorMessage = "This field is required")]
        public Races CharacterRace { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "This field is required")]
        public Genders CharacterGender { get; set; }

        [DisplayName("Level")]
        [Required(ErrorMessage = "This field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be between {0} and {1}")]
        public int PlayerLevel { get; set; }

        [DisplayName("Age")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be between {0} and {1}")]
        public int PlayerAge { get; set; }

        [DisplayName("Experience")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be between {0} and {1}")]
        public int PlayerExperience { get; set; }

        [DisplayName("Background")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, HtmlEncode = true)]
        [MaxLength(5000, ErrorMessage ="Maximum characters allowed for this field is {0}")]
        [DataType(DataType.MultilineText)]
        public string PlayerBackground { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage ="You mus distribute between {0} and {1} points")]
        [DisplayName("Points to distribute")]
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
        public Guid SelectedCampaign { get; set; }

        [ScaffoldColumn(false)]
        public string CharacterUser { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<SelectedCharacterAttributeViewModel> SelectedAttributes { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<SelectedPlayerItemViewModel> SelectedItems { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

    }
}
