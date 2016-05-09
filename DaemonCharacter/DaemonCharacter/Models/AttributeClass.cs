using System.Collections;
using System.Collections.Generic;
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

    public class AttributeBonusClass
    {
        [Key, Column(Order = 0)]
        public int idAttribute { get; set; }

        [Key, Column(Order = 1)]
        public int idAttributeBonusClass { get; set; }

        [ForeignKey("idAttribute")]
        public AttributeClass Attribute { get; set; }

        [ForeignKey("idAttributeBonusClass")]
        public virtual IEnumerable<AttributeClass> AttributeBonus { get; set; }

    }

    public class AttributeTypeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAttributeType { get; set; }

        [Required, MaxLength(50), Display(Name="Attribute Type Name")]
        public string name { get; set; }

        [Required(ErrorMessage ="Field Required"), DefaultValue(true), Display(Name ="Attribute can be bonified?")]
        public bool useBonus { get; set; }

        [Required(ErrorMessage = "Field Required"), DefaultValue(false), Display(Name = "Attribute bonify using modifier?")]
        public bool useModifier { get; set; }

        [DefaultValue(0), Display(Name = "Base modifier")]
        public int baseModifier { get; set; }
    }
}