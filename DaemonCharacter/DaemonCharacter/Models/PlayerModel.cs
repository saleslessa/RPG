using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaemonCharacter.Models
{
    public class PlayerModel : CharacterModel
    {

        [Display(Name = "Campaign")]
        public virtual CampaignModel campaign { get; set; }

        [Required(ErrorMessage = "Player level is required")]
        [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
        public int level { get; set; }

        [Display(Name = "Age"), DefaultValue(0), Range(0, int.MaxValue)]
        public int age { get; set; }

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

        [Display(Name = "Money"), DataType(DataType.Currency)]
        public int money { get; set; }
    }

    [Table("tb_player_item")]
    public class PlayerItemModel
    {
        [Key, Column(Order = 1)]
        public int idPlayer { get; set; }

        [Key, Column(Order = 2)]
        public int idItem { get; set; }

        [ForeignKey("idPlayer")]
        public PlayerModel player { get; set; }

        [ForeignKey("idItem")]
        public ItemModel item { get; set; }

        [Display(Name ="Quantity"), Range(1, 999, ErrorMessage ="Minimum to buy is 1")]
        public int qtd { get; set; }

        [Display(Name = "Unit price"), Range(1,int.MaxValue, ErrorMessage ="Minimum price is 1")]
        [DataType(DataType.Currency)]
        public int unitPrice { get; set; }

        [DefaultValue(false)]
        public bool approvedByMaster { get; set; }
    }
}