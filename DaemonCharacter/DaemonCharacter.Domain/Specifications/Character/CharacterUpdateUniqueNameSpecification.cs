using DomainValidation.Interfaces.Specification;
using DaemonCharacter.Domain.Interfaces.Repository;

namespace DaemonCharacter.Domain.Specifications.Character
{
    public class CharacterUpdateUniqueNameSpecification : ISpecification<Entities.Character>
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterUpdateUniqueNameSpecification(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public bool IsSatisfiedBy(Entities.Character entity)
        {
            return _characterRepository.Search(p=>p.CharacterName == entity.CharacterName && p.CharacterId != entity.CharacterId) != null;
        }
    }
}
