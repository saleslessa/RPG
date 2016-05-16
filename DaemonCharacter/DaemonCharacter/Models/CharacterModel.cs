using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum Gender
{
    Male = 0,
    Female = 1,
    Other = 2
}

namespace DaemonCharacter.Models
{
    [Table("tb_character")]
    public class CharacterModel
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCharacter { get; set; }

        [Required]
        public int idUser { get; set; }

        [Required]
        public int idCampaign { get; set; }

        [Required(ErrorMessage = "Character name is required"), MaxLength(50)]
        [Display(Name = "Character Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Character level is required")]
        [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
        public int level { get; set; }

        [Display(Name = "Gender"), DefaultValue(Gender.Other)]
        public Gender gender { get; set; }

        [Display(Name = "Age"), DefaultValue(0), Range(0, int.MaxValue)]
        public int age { get; set; }

        [Required, Display(Name ="Maximum Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int maxLife { get; set; }

        [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int remainingLife { get; set; }

        [Required(ErrorMessage = "Experience is necessary :)"), DefaultValue(0), Display(Name = "Character Experience")]
        [Range(0, int.MaxValue)]
        public int experience { get; set; }

        [Required(ErrorMessage = "You must have an initial points to distribute among attributes"), DefaultValue(1)]
        [Display(Name = "Points to distribute among Attributes")]
        [Range(0, int.MaxValue)]
        public int pointsToDistribute { get; set; }

        [DefaultValue(0), Range(0, int.MaxValue)]
        [Display(Name = "Remaining points to distribute among Attributes")]
        public int remainingPoints { get; set; }

        #region Virtual Attributes
        public virtual List<CharacterAttributeModel> attributes { get; set; }

        [ForeignKey("idUser")]
        public virtual UserProfileModel user { get; set; }

        [ForeignKey("idCampaign")]
        public virtual CampaignModel campaign { get; set; }
        #endregion

    }

    public class EditCharacterModel
    {

        [Required]
        public int idCharacter { get; set; }

        [Required(ErrorMessage = "Character name is required"), StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =5)]
        [Display(Name = "Character Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Character level is required")]
        [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
        public int level { get; set; }

        [Display(Name = "Gender"), DefaultValue(Gender.Other)]
        public Gender gender { get; set; }

        [Display(Name = "Age"), DefaultValue(0), Range(0, int.MaxValue)]
        public int age { get; set; }

        [Required, Display(Name = "Maximum Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int maxLife { get; set; }

        [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int remainingLife { get; set; }

        [Required(ErrorMessage = "Experience is necessary :)"), DefaultValue(0), Display(Name = "Character Experience")]
        [Range(0, int.MaxValue)]
        public int experience { get; set; }

        [Required(ErrorMessage = "You must have an initial points to distribute among attributes"), DefaultValue(1)]
        [Display(Name = "Points to distribute among Attributes")]
        [Range(0, int.MaxValue)]
        public int pointsToDistribute { get; set; }

        [DefaultValue(0), Range(0, int.MaxValue)]
        [Display(Name = "Remaining points to distribute among Attributes")]
        public int remainingPoints { get; set; }

        #region Virtual Attributes
        public virtual List<CharacterAttributeModel> attributes { get; set; }

        [ForeignKey("idCampaign")]
        public virtual CampaignModel campaign { get; set; }
        #endregion
    }

    [Table("tb_character_attribute")]
    public class CharacterAttributeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int idCharacterAttribute { get; set; }

        [Required, Key]
        [Column(Order = 1)]
        public int idCharacter { get; set; }

        [Required, Key]
        [Column(Order = 2)]
        public int idAttribute { get; set; }

        [Required, ForeignKey("idCharacter")]
        public virtual CharacterModel character { get; set; }

        [Required, ForeignKey("idAttribute")]
        public virtual AttributeModel attribute { get; set; }

        [Required, DefaultValue("0"), Range(0, int.MaxValue)]
        public int value { get; set; }

        public virtual ICollection<CharacterAttributeModel> bonusValues { get; set; }
    }
}
