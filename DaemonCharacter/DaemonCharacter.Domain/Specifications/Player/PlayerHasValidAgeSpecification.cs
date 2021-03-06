﻿using DomainValidation.Interfaces.Specification;

namespace DaemonCharacter.Domain.Specifications.Player
{
    public class PlayerHasValidAgeSpecification : ISpecification<Entities.Player>
    {
        public bool IsSatisfiedBy(Entities.Player entity)
        {
            return entity.PlayerAge > 0;
        }
    }
}
