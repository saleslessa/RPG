using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DaemonCharacter.Models
{
    public class AttributeClass
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required, MaxLength(50), Display(Name = "Attribute Name")]
        public string name { get; set; }

        [DataType(DataType.MultilineText), MaxLength(250), Display(Name = "Attribute Description")]
        public string description { get; set; }

        [Required, Display(Name = "Type")]
        public AttributeType type { get; set; }

        public int minimum { get; set; }
    }
}