using DaemonCharacter.Domain.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.PlayerItem
{
    public class SelectedPlayerItemViewModel
    {
        [Key]
        public Guid ItemId { get; set; }

        [ScaffoldColumn(false)]
        public Guid PlayerId { get; set; }

        [DisplayName("Item")]
        public string ItemName { get; set; }

        [DefaultValue(1)]
        [DisplayName("Quantity")]
        public int PlayerItemQtd { get; set; }

        [ScaffoldColumn(false)]
        public DateTime PlayerItemDateBought { get; set; }

        [DataType(DataType.Currency)]
        public double PlayerItemUnitPrice { get; set; }

        [ScaffoldColumn(false)]
        public bool PlayerItemApprovedByMaster { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public SelectedPlayerItemViewModel()
        {
            ValidationResult = new DomainValidation.Validation.ValidationResult();
            PlayerItemApprovedByMaster = false;
            PlayerItemDateBought = DateTime.Now.Date;
        }
    }
}
