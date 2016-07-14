using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Specifications.Player
{
    public class PlayerUniqueNameSpecification : ISpecification<Entities.Player>
    {
        private IPlayerRepository _playerRepository;

        public PlayerUniqueNameSpecification(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return _playerRepository.Search(p => p.CharacterName == entity.CharacterName) != null;
        }
    }
}
