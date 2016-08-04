using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;
using System.Linq;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {


        public PlayerRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public Player SearchByName(string name)
        {
            return Search(t => t.CharacterName.ToUpper().Trim() == name.ToUpper().Trim()).FirstOrDefault();
        }
    }
}
