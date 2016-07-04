namespace DaemonCharacter.Domain.Entities
{
    public class Player : Character
    {
        public virtual Campaign Campaign { get; set; }

        public int PlayerLevel { get; set; }

        public int PlayerAge { get; set; }

        public int PlayerExperience { get; set; }

        public string PlayerBackground { get; set; }

        public int PlayerPointsToDistribute { get; set; }

        public int PlayerRemainingPoints { get; set; }

        public int PlayerMoney { get; set; }
    }

    
}