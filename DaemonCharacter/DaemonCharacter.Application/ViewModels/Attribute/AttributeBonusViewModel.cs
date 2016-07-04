using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Attribute
{
    public class AttributeBonusViewModel
    {

        [Key]
        public Guid AttributeId { get; set; }

        [DisplayName("Name")]
        public string AttributeName { get; set; }

        [DisplayName("Selected")]
        [DefaultValue(false)]
        public bool Selected { get; set; }
    }
}
