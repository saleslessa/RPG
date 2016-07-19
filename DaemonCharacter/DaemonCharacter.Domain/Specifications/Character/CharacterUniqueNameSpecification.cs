using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Specifications.Character
{
    public class CharacterUniqueNameSpecification : ISpecification<Entities.Player>
    {
        private ICharacterRepository _playerRepository;

        public CharacterUniqueNameSpecification(ICharacterRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return _playerRepository.Search(p => p.CharacterName == entity.CharacterName) != null;
        }
    }
}
