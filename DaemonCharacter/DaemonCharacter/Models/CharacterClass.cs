using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public enum Gender
{
    Male = 0,
    Female = 1,
    Other = 2
}

namespace DaemonCharacter.Models
{
    public class CharacterClass
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCharacter { get; set; }

        public IEnumerable<CharacterAttributeClass> characterAttributes { get; set; }

        [Required(ErrorMessage = "Character name is required"), MaxLength(50)]
        [Display(Name = "Character Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Character name is required")]
        [Display(Name = "Character Level"), DefaultValue(1), Range(1, int.MaxValue)]
        public int level { get; set; }

        [Display(Name = "Gender"), DefaultValue(Gender.Other)]
        public Gender gender { get; set; }

        [Display(Name = "Age"), DefaultValue(0), Range(0, int.MaxValue)]
        public int age { get; set; }

        [Required(ErrorMessage = "Experience is necessary :)"), DefaultValue(0), Display(Name = "Character Experience")]
        [Range(0, int.MaxValue)]
        public int experience { get; set; }

        [Required(ErrorMessage = "You must have an initial points to distribute among attributes"), DefaultValue(1)]
        [Display(Name = "Points to distribute among Attributes")]
        [Range(0, int.MaxValue)]
        public int pointsToDistribute { get; set; }

        [DefaultValue(0), Range(0, int.MaxValue)]
        [Display(Name = "Remaining points to distribute among Attributes")]
        public int remainingPoints { get; set; }

        [Display(Name = "Attributes")]
        [ForeignKey("characterAttributes")]
        public virtual IEnumerable<CharacterAttributeClass> attributes { get; set; }
    }

    public class CharacterAttributeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order =0)]
        public int idCharacterAttributeClass { get; set; }

        [Required, Key]
        [Column(Order = 1)]
        public int idCharacter { get; set; }

        [Required, Key]
        [Column(Order = 2)]
        public int idAttribute { get; set; }

        [Required, ForeignKey("idCharacter")]
        public CharacterClass character { get; set; }

        [Required, ForeignKey("idAttribute")]
        public AttributeClass attribute { get; set; }

        [Required, DefaultValue("0"), Range(0, 100)]
        public int value { get; set; }

    }
}