using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Specifications.Player
{
    public class PlayerUpdateUniqueNameSpecification : ISpecification<Entities.Player>
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerUpdateUniqueNameSpecification(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return _playerRepository.Search(p=>p.CharacterName == entity.CharacterName && p.CharacterId != entity.CharacterId) == null;
        }
    }
}
