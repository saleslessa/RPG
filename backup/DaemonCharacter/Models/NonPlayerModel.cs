using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaemonCharacter.Models
{
    public class NonPlayerModel : CharacterModel
    {

        [Required, Display(Name = "NPC Type"), DefaultValue(NonPlayerTypes.NPC)]
        public NonPlayerTypes type { get; set; }

        [DefaultValue(0), Display(Name = "Chalenge Level"), Range(0, 999)]
        public int chalengeLevel { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Annotations that all players can see")]
        public string publicAnnotations { get; set; }
    }

    [Table("tb_nonplayer_session")]
    public class NonPlayerSessionModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int id { get; set; }

        [Key, Column(Order = 2)]
        public int idSession { set; get; }

        public int order { get; set; }

        [Required, Display(Name = "Remaining Life Points"), Range(0, int.MaxValue), DefaultValue(0)]
        public int remainingLife { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Annotations that only you (master) can see")]
        public string privateAnnotations { get; set; }

        public virtual List<NonPlayerModel> nonplayer { get; set; }

        [ForeignKey("idSession")]
        public virtual SessionModel session { get; set; }
    }

    public class NonPlayerGridSession
    {
        public NonPlayerModel nonPlayer { get; set; }

        public int idSession { get; set; }

        public int order { get; set; }

        public int remainingLife { get; set; }

        [DisplayName("Quantity")]
        public int qtd { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Annotations that only you (master) can see")]
        public string privateAnnotations { get; set; }
    }

    [Table("tb_NonPlayer_Campaign_Item")]
    public class NonPlayerSessionItemModel
    {
        [Key, Column(Order = 1)]
        public int idNonPlayerCampaign { get; set; }

        [Key, Column(Order = 2)]
        public int idItem { get; set; }

        [ForeignKey("idNonPlayerCampaign")]
        public virtual NonPlayerSessionModel nonPlayerCampaign { get; set; }

        [ForeignKey("idItem")]
        public virtual List<ItemModel> item { get; set; }

    }
}