using System;

namespace DaemonCharacter.Domain.Entities
{
    public class PlayerSessions
    {
        public Guid PlayerSessionId { get; set; }

        public Sessions Session { get; set; }

        public Player Player { get; set; }

        public string PlayerSessionPublicAnnotations { get; set; }

        public string PlayerSessionPrivateAnnotations { get; set; }

        public PlayerSessions()
        {
            PlayerSessionId = Guid.NewGuid();
        }
    }

}