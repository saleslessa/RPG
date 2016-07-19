using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class NonPlayerRepository : Repository<NonPlayer>, INonPlayerRepository
    {
        public NonPlayerRepository(DaemonCharacterContext context) : base(context)
        {
        }
    }
}
