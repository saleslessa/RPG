using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class SessionRepository : Repository<Sessions>, ISessionRepository
    {
        public SessionRepository(DaemonCharacterContext context) : base(context)
        {
        }
    }
}
