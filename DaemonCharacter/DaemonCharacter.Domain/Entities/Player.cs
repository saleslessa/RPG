namespace DaemonCharacter.Domain.Entities
{
    public class Player : Character
    {
        public int PlayerLevel { get; set; }

        public int PlayerAge { get; set; }

        public int PlayerExperience { get; set; }

        public string PlayerBackground { get; set; }

        public int PlayerPointsToDistribute { get; set; }

        public int PlayerMoney { get; set; }

        public Player() : base()
        {
        }

        public bool IsValid()
        {
            //TODO: MAKE VALIDATION OF CONSISTENCY
            return true;
        }
    }


}