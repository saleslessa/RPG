using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DaemonCharacter.Application.ViewModels.Player
{
    public class SelectedPlayerItemViewModel
    {
        [Key]
        public Guid ItemId { get; set; }

        [ScaffoldColumn(false)]
        public Guid CharacterId { get; set; }

        [DisplayName("Item")]
        public string ItemName { get; set; }

        public bool ItemUniqueUse { get; set; }

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
        public bool PlayerItemActive { get; set; }

        [ScaffoldColumn(false)]
        public bool PlayerItemUsingItem { get; set; }

        [ScaffoldColumn(false)]
        public DomainValidation.Validation.ValidationResult ValidationResult { get; set; }

        public SelectedPlayerItemViewModel()
        {
            ValidationResult = new DomainValidation.Validation.ValidationResult();
            PlayerItemApprovedByMaster = false;
            PlayerItemDateBought = DateTime.Now.Date;
            PlayerItemActive = true;
            PlayerItemUsingItem = false;
        }
    }
}
