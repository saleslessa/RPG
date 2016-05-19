using System.Collections.Generic;
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

        [Display(Name ="Other effects"), DataType(DataType.Text), StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =5)]
        public string effect { get; set; }

    }

    [Table("tb_item_attribute")]
    public class ItemAttributeModel
    {
        [Key, Column(Order = 1)]
        public int idItem { get; set; }

        [Key, Column(Order = 2)]
        public int idAttribute { get; set; }

        [Required(ErrorMessage ="You must select a value to associate to this attribute")]
        public int value { get; set; }

        [ForeignKey("idItem")]
        public virtual ItemModel item { get; set; }

        [ForeignKey("idAttribute")]
        public virtual AttributeModel attribute { get; set; }

    }
}