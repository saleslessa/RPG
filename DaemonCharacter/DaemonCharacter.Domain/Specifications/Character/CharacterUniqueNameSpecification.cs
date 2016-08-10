using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Specifications.Character
{
    public class CharacterUniqueNameSpecification : ISpecification<Entities.Player>
    {
        private IPlayerRepository _playerRepository;

        public CharacterUniqueNameSpecification(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool IsSatisfiedBy(Entities.Player entity)
        {
            var obj = _playerRepository.SearchByName(entity.CharacterName);
            return obj == null || obj.CharacterId == entity.CharacterId;
        }
    }
}
