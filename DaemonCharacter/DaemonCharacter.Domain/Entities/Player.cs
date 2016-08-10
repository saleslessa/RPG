using DaemonCharacter.Domain.Validations.Player;

namespace DaemonCharacter.Domain.Entities
{
    public class Player : Character
    {
        public int PlayerLevel { get; set; }

        public int PlayerAge { get; set; }

        public int PlayerExperience { get; set; }

        public string PlayerBackground { get; set; }

        public int PlayerPointsToDistribute { get; set; }

        public double PlayerMoney { get; set; }

        public string PrivateAnnotations { get; set; }

        public Player() : base()
        {
        }

        public bool IsValid()
        {
            ValidationResult = new PlayerConsistentValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }


}