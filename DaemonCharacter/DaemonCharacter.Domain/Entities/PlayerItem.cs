using DaemonCharacter.Domain.Validations.PlayerItem;
using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{
    public class PlayerItem
    {

        public Guid PlayerItemId { get; set; }

        public virtual Player Player { get; set; }

        public virtual Item Item { get; set; }

        public int PlayerItemQtd { get; set; }

        public DateTime PlayerItemDateBought { get; set; }

        public double PlayerItemUnitPrice { get; set; }

        public bool PlayerItemApprovedByMaster { get; set; }

        public bool PlayerItemUsingItem { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public bool PlayerItemActive { get; set; }

        public DateTime? Modified { get; set; }

        public PlayerItem()
        {
            PlayerItemId = Guid.NewGuid();
            PlayerItemUsingItem = false;
            ValidationResult = new ValidationResult();
        }

        public bool IsValid()
        {
            ValidationResult = new PlayerItemConsistentValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}