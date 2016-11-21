using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Player SearchByName(string name);

        void ChangePlayerAge(Player p);

        void ChangePlayerExperience(Player p);

        void ChangeCharacterMaxLife(Player p);

        void ChangePlayerMoney(Player p);

        void ChangePlayerLevel(Player p);

        void ChangeCharacterRemainingLife(Player p);

        void ChangePrivateAnnotations(Player p);
    }
}
