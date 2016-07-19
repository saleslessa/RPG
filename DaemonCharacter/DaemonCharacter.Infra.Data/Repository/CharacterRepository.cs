using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {
        public CharacterRepository(DaemonCharacterContext context) : base(context)
        {
        }
    }
}
