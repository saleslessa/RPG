using System;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Item
{
    public class SelectedItemViewModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public Guid ItemId { get; set; }

        public double ItemPrice { get; set; }

        public int ItemQtd { get; set; }

        [ScaffoldColumn(false)]
        public Guid CharacterId { get; set; }

        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public SelectedItemViewModel()
        {
            ValidationResult = new DomainValidation.Validation.ValidationResult();
        }
    }
}
