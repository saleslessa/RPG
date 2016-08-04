using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player SearchByName(string name);
    }
}
