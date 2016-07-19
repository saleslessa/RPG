using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.CharacterAttribute
{
    public class SelectedCharacterAttributeViewModel
    {

        [Key]
        [ScaffoldColumn(false)]
        public Guid AttributeId { get; set; }

        [DisplayName("Attribute")]
        public string AttributeName { get; set; }

        [DisplayName("Value")]
        [Required(ErrorMessage = "Character Value must be filled.")]
        [Range(1, short.MaxValue, ErrorMessage = "Character Value must be between {0} and {1}")]
        public int Value { get; set; }
    }
}
