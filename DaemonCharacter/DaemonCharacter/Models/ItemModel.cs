using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaemonCharacter.Models
{

    [Table("tb_item")]
    public class ItemModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required"), StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Item Name"), DataType(DataType.Text)]
        public string name { get; set; }

        [Display(Name ="Other effects"), DataType(DataType.MultilineText), StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =5)]
        public string effect { get; set; }

        [DataType(DataType.Currency), Range(0,int.MaxValue, ErrorMessage ="Price cannot be lower than 0")]
        [DefaultValue(0), Required(ErrorMessage ="Preice of item is necessary")]
        [Display(Name ="Price")]
        public int price { get; set; }

    }

    [Table("tb_item_attribute")]
    public class ItemAttributeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public virtual ItemModel item { get; set; }

        public virtual AttributeModel attribute { get; set; }

        [Required(ErrorMessage = "You must select a value to associate to this attribute")]
        public int value { get; set; }

        public ItemAttributeModel() { }

        internal ItemAttributeModel(AttributeModel _att, int _value)
        {
            this.attribute = _att;
            this.value = _value;
        }
    }
}