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

        [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int remainingLife { get; set; }

        public virtual UserProfileModel user { get; set; }

        [Display(Name = "Race")]
        public Races race { get; set; }

        [Display(Name ="Gender")]
        public Genders gender { get; set; }
 
    }

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
