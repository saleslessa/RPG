using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Repository;
using System.Linq;

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
            return _playerRepository.SearchByName(entity.CharacterName) == null;
        }
    }
}
