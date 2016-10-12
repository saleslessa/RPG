using System;
using DomainValidation.Validation;

namespace DaemonCharacter.Domain.Entities
{
    public class PlayerMagic
    {
        public Guid PlayerMagicId { get; set; }

        public virtual Player Player { get; set; }

        public virtual Magic Magic { get; set; }

        public bool Using { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public PlayerMagic()
        {
            PlayerMagicId = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }
    }
}
