using DaemonCharacter.Application.ViewModels.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.CharacterAttribute
{
    public class CharacterAttributeViewModel
    {
        public Guid CharacterId { get; set; }

        [DisplayName("Available Attributes")]
        public List<AttributeViewModel> ListOfAvailableAttributes { get; set; }

        [DisplayName("Selected Attributes")]
        public List<SelectedCharacterAttributeViewModel> SelectedAttributes { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public CharacterAttributeViewModel()
        {
            ListOfAvailableAttributes = new List<AttributeViewModel>();
            SelectedAttributes = new List<SelectedCharacterAttributeViewModel>();
            ValidationResult = new DomainValidation.Validation.ValidationResult();
        }

    }
}