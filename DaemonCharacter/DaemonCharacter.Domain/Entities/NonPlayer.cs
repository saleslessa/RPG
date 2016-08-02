using DaemonCharacter.Domain.Validations.NonPlayer;

namespace DaemonCharacter.Domain.Entities
{
    public class NonPlayer : Character
    {

        public NonPlayerTypes NonPlayerType { get; set; }

        public int NonPlayerChalengeLevel { get; set; }

        public string NonPlayerPublicAnnotations { get; set; }

        public NonPlayer() : base()
        {
        }

        public bool IsValid()
        {
            ValidationResult = new NonPlayerConsistentValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}