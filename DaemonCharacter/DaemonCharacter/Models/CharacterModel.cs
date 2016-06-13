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

        [Required(ErrorMessage = "Name is required"), MaxLength(50)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required, Display(Name = "Maximum Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int maxLife { get; set; }

        public virtual UserProfileModel user { get; set; }

        [Display(Name = "Race")]
        public Races race { get; set; }

        [Display(Name ="Gender")]
        public Genders gender { get; set; }
 

    }

    public class PlayerModel : CharacterModel
    {

        [Display(Name = "Campaign")]
        public virtual CampaignModel campaign { get; set; }

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
    }

    //public class EditPlayerModel
    //{

    //    [Required]
    //    public int id { get; set; }

    //    [Required(ErrorMessage = "Character name is required"), StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
    //    [Display(Name = "Character Name")]
    //    public string name { get; set; }

    //    [Required(ErrorMessage = "Character level is required")]
    //    [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
    //    public int level { get; set; }

    //    [Display(Name = "Gender")]
    //    public int idGender { get; set; }

    //    [Display(Name = "Age"), DefaultValue(0), Range(0, int.MaxValue)]
    //    public int age { get; set; }

    //    [Required, Display(Name = "Maximum Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
    //    public int maxLife { get; set; }

    //    [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
    //    public int remainingLife { get; set; }

    //    [Required(ErrorMessage = "Experience is necessary :)"), DefaultValue(0), Display(Name = "Character Experience")]
    //    [Range(0, int.MaxValue)]
    //    public int experience { get; set; }

    //    [Required(ErrorMessage = "You must have an initial points to distribute among attributes"), DefaultValue(1)]
    //    [Display(Name = "Points to distribute among Attributes")]
    //    [Range(0, int.MaxValue)]
    //    public int pointsToDistribute { get; set; }

    //    [DefaultValue(0), Range(0, int.MaxValue)]
    //    [Display(Name = "Remaining points to distribute among Attributes")]
    //    public int remainingPoints { get; set; }

    //    #region Virtual Attributes
    //    public List<CharacterAttributeModel> attributes { get; set; }

    //    [ForeignKey("idCampaign")]
    //    public CampaignModel campaign { get; set; }

    //    public Genders gender { get; set; }
    //    #endregion
    //}

    [Table("tb_character_attribute")]
    public class CharacterAttributeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public virtual CharacterModel character { get; set; }

        [Required]
        public virtual AttributeModel attribute { get; set; }

        [Required, DefaultValue("0"), Range(0, int.MaxValue)]
        public int value { get; set; }

        //public virtual List<CharacterAttributeModel> bonusValues { get; set; }

        public CharacterAttributeModel() { }

       
    }

    public class NonPlayerModel : CharacterModel
    {
        
        [Required, Display(Name ="NPC Type"), DefaultValue(NonPlayerTypes.NPC)]
        public NonPlayerTypes type { get; set; }

        [DefaultValue(0), Display(Name = "Chalenge Level"), Range(0, 999)]
        public int chalengeLevel { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Annotations that all players can see")]
        public string publicAnnotations { get; set; }
    }

    //[Table("tb_nonplayer_session")]
    //public class NonPlayerCampaignModel
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    [Column(Order = 0)]
    //    public int id { get; set; }

    //    [Key, Column(Order = 1)]
    //    public int idSession { set; get; }

    //    [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
    //    public int remainingLife { get; set; }

    //    [DataType(DataType.MultilineText)]
    //    [Display(Name = "Annotations that only you (master) can see")]
    //    public string privateAnnotations { get; set; }

    //    public virtual NonPlayerModel nonplayer { get; set; }

    //    [ForeignKey("idSession")]
    //    public virtual CampaignSessionModel session { get; set; }
    //}

}
