using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaemonCharacter.Models
{
    public class CharacterClass
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, MaxLength(50)]
        public string name { get; set; }


    }

    public class CharacterAttributeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [Key]
        public CharacterClass character { get; set; }

        [Key]
        public AttributeClass attributte { get; set; }

        [Required, DefaultValue("0")]
        public int value { get; set; }

        public AttributeClass bonus { get; set; }

    }
}