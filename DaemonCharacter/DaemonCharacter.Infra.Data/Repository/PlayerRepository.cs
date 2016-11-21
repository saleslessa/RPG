using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;
using System.Linq;
using System;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private readonly DaemonCharacterContext _context;

        public PlayerRepository(DaemonCharacterContext context) : base(context)
        {
            _context = context;
        }

        public Player SearchByName(string name)
        {
            return Search(t => t.CharacterName.ToUpper().Trim() == name.ToUpper().Trim()).FirstOrDefault();
        }

        public void ChangePlayerAge(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.PlayerAge).IsModified = true;
        }

        public void ChangePlayerExperience(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.PlayerExperience).IsModified = true;
        }

        public void ChangeCharacterMaxLife(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.CharacterMaxLife).IsModified = true;
        }

        private void Attach(Player p) => _context.Players.Attach(p);

        public void ChangePlayerMoney(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.PlayerMoney).IsModified = true;
        }

        public void ChangePlayerLevel(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.PlayerLevel).IsModified = true;
        }

        public void ChangeCharacterRemainingLife(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.CharacterRemainingLife).IsModified = true;
        }

        public void ChangePrivateAnnotations(Player p)
        {
            Attach(p);
            _context.Entry(p).Property(t => t.PrivateAnnotations).IsModified = true;
        }
    }
}
