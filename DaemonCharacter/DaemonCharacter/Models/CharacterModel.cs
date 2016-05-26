using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaemonCharacter.Models
{


    [Table("tb_character")]
    public class CharacterModel
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int idUser { get; set; }

        [Required(ErrorMessage = "Name is required"), MaxLength(50)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Race is required. If you don't have one choose default")]
        public int idRace { get; set; }

        [Display(Name = "Gender"), Required(ErrorMessage = "Gender is required")]
        public int idGender { get; set; }

        [Required, Display(Name = "Maximum Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int maxLife { get; set; }

        #region Virtual Attributes
        public virtual List<CharacterAttributeModel> attributes { get; set; }

        [ForeignKey("idUser")]
        public virtual UserProfileModel user { get; set; }

        [ForeignKey("idRace"), Display(Name = "Race")]
        public virtual RaceModel race { get; set; }

        [ForeignKey("idGender"), Display(Name ="Gender")]
        public virtual GenderModel gender { get; set; }
        #endregion

    }

    public class PlayerModel : CharacterModel
    {

        [Required]
        public int idCampaign { get; set; }

        [Required(ErrorMessage = "Player level is required")]
        [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
        public int level { get; set; }

        [Display(Name = "Age"), DefaultValue(0), Range(0, int.MaxValue)]
        public int age { get; set; }

        [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int remainingLife { get; set; }

        [Required(ErrorMessage = "Experience is necessary :)"), DefaultValue(0), Display(Name = "Player Experience")]
        [Range(0, int.MaxValue)]
        public int experience { get; set; }

        [Display(Name = "Background"), DataType(DataType.MultilineText)]
        public string background { get; set; }

        [Required(ErrorMessage = "You must have an initial points to distribute among attributes"), DefaultValue(1)]
        [Display(Name = "Points to distribute among Attributes")]
        [Range(0, int.MaxValue)]
        public int pointsToDistribute { get; set; }

        [DefaultValue(0), Range(0, int.MaxValue)]
        [Display(Name = "Remaining points to distribute among Attributes")]
        public int remainingPoints { get; set; }

        [ForeignKey("idCampaign"), Display(Name ="Campaign")]
        public virtual CampaignModel campaign { get; set; }
    }

    public class EditPlayerModel
    {

        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = "Character name is required"), StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Character Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Character level is required")]
        [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
        public int level { get; set; }

        [Display(Name = "Gender")]
        public int idGender { get; set; }

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

        public virtual GenderModel gender { get; set; }
        #endregion
    }

    [Table("tb_character_attribute")]
    public class CharacterAttributeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int id { get; set; }

        [Required, Key]
        [Column(Order = 2)]
        public int idCharacter { get; set; }

        [Required, Key]
        [Column(Order = 3)]
        public int idAttribute { get; set; }

        [Required, ForeignKey("idCharacter")]
        public virtual CharacterModel character { get; set; }

        [Required, ForeignKey("idAttribute")]
        public virtual AttributeModel attribute { get; set; }

        [Required, DefaultValue("0"), Range(0, int.MaxValue)]
        public int value { get; set; }

        public virtual List<CharacterAttributeModel> bonusValues { get; set; }

        public CharacterAttributeModel() { }

        public CharacterAttributeModel(int _idCharacter, int _idAttribute, int _value) {
            idCharacter = _idCharacter;
            idAttribute = _idAttribute;
            value = _value;

        }
    }

    public class NonPlayerModel : CharacterModel
    {
        [Required]
        public int idType { get; set; }

        [ForeignKey("idType"), Display(Name ="NPC Type")]
        public NonPlayerTypeModel type { get; set; }

        [DefaultValue(0), Display(Name = "Chalenge Level"), Range(0, 999)]
        public int chalengeLevel { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Annotations that all players can see")]
        public string publicAnnotations { get; set; }

        public virtual ICollection<NonPlayerCampaignModel> nonPlayerCampaigns { get; set; }
    }

    [Table("tb_nonplayer_campaign")]
    public class NonPlayerCampaignModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key, Column(Order = 1)]
        public int idNonPlayer { get; set; }

        [Key, Column(Order = 2)]
        public int idCampaign { get; set; }

        [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int remainingLife { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Annotations that only you (master) can see")]
        public string privateAnnotations { get; set; }


        [ForeignKey("idNonPlayer")]
        public virtual NonPlayerModel nonplayer { get; set; }

        [ForeignKey("idCampaign")]
        public virtual CampaignModel campaign { get; set; }
    }


    [Table("tb_race")]
    public class RaceModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, Display(Name = "Name")]
        public string name { get; set; }

    }

    [Table("tb_gender")]
    public class GenderModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, Display(Name = "Name")]
        public string name { get; set; }
    }

    [Table("tb_nonplayer_type")]
    public class NonPlayerTypeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, Display(Name = "Name")]
        public string name { get; set; }

    }
}
