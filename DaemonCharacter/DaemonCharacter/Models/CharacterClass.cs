using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

public enum Gender
{
    Male,
    Female,
    Other
}

namespace DaemonCharacter.Models
{
    public class CharacterClass
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCharacter { get; set; }

        [Required(ErrorMessage="Character name is required"), MaxLength(50)]
        [Display(Name = "Character Name")]
        public string name { get; set; }

        [Display(Name="Gender"), DefaultValue(Gender.Other)]
        public Gender gender { get; set; }

        [Display(Name="Age"), DefaultValue(0), Range(0, int.MaxValue)]
        public int age { get; set; }

        [Required(ErrorMessage="Experience is necessary :)"), DefaultValue(0), Display(Name="Character Experience")]
        [Range(0, int.MaxValue)]
        public int experience { get; set; }

        [Required(ErrorMessage="You must have an initial points to distribute among attributes"), DefaultValue(1)]
        [Display(Name="Points to distribute among Attributes")]
        [Range(0, int.MaxValue)]
        public int pointsToDistribute { get; set; }

        [Display(Name="Attributes")]
        public virtual IEnumerable<CharacterAttributeClass> attributes { get; set; }
    }

    public class CharacterAttributeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCharacterAttributeClass { get; set; }

        [Required]
        public int idAttribute { get; set; }
        
        [Required, ForeignKey("idAttribute")]
        public AttributeClass attribute { get; set; }

        [Required, DefaultValue("0"), Range(0,100)]
        public int value { get; set; }

    }
}