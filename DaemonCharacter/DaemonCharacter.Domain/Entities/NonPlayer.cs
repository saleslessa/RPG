namespace DaemonCharacter.Domain.Entities
{
    public class NonPlayers : Character
    {

        public NonPlayerTypes NonPlayerType { get; set; }

        public int NonPlayerChalengeLevel { get; set; }

        public string NonPlayerPublicAnnotations { get; set; }
    }
}