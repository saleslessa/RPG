using DaemonCharacter.Domain.Validations.PlayerItem;
using DomainValidation.Validation;
using System;

namespace DaemonCharacter.Domain.Entities
{
    public class PlayerItem
    {

        public Guid PlayerItemId { get; set; }

        public Player Player { get; set; }

        public Item Item { get; set; }

        public int PlayerItemQtd { get; set; }

        public DateTime PlayerItemDateBought { get; set; }

        public double PlayerItemUnitPrice { get; set; }

        public bool PlayerItemApprovedByMaster { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public PlayerItem()
        {
            PlayerItemId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }

        public bool IsValid()
        {
            ValidationResult = new PlayerItemConsistentValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}