using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaemonCharacter.Models
{
    [Table("tb_Attribute")]
    public class AttributeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Please put a correct name"), MaxLength(50), Display(Name = "Attribute Name")]
        public string name { get; set; }

        [DataType(DataType.MultilineText), MaxLength(250), Display(Name = "Attribute Description")]
        public string description { get; set; }

        [Required, Display(Name = "Type")]
        public AttributeType type { get; set; }

        [Required, Display(Name = "Minimum Required to use")]
        [DefaultValue(0), Range(0, 100)]
        public int minimum { get; set; }

        public virtual List<AttributeModel> ParentAttribute { get; set; }

        public virtual List<AttributeModel> AttributeBonus { get; set; }

    }

   
}