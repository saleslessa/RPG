using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaemonCharacter.Models
{
    public class AttributeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAttribute { get; set; }

        public int idAttributeType { get; set; }

        [Required(ErrorMessage="Please put a correct name"), MaxLength(50), Display(Name = "Attribute Name")]
        public string name { get; set; }

        [DataType(DataType.MultilineText), MaxLength(250), Display(Name = "Attribute Description")]
        public string description { get; set; }

        [Display(Name = "Type")]
        [ForeignKey("idAttributeType")]
        public virtual AttributeTypeClass type { get; set; }

        [Required, Display(Name = "Minimum Required to use")]
        [DefaultValue(0), Range(0, 100)]
        public int minimum { get; set; }

    }

    public class AttributeTypeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAttributeType { get; set; }

        [Required, MaxLength(50), Display(Name="Attribute Type Name")]
        public string name { get; set; }

    }
}