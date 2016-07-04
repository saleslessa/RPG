using System;

namespace DaemonCharacter.Domain.Entities
{
    public class Sessions
    {
        public Guid SessionId { get; set; }

        public DateTime SessionDateScheduled { get; set; }

        public virtual Campaign Campaign { get; set; }

        public string SessionBriefing { get; set; }

        public string SessionPrivateBeforeAnnotations { get; set; }

        public string SessionDuringAnnotations { get; set; }

        public SessionStatus SessionStatus { get; set; }

        public Sessions()
        {
            SessionId = new Guid();
        }

    }
}