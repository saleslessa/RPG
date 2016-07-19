using DomainValidation.Interfaces.Specification;
using System;

namespace DaemonCharacter.Domain.Specifications.PlayerItem
{
    public class PlayerItemValidateDateBoughtSpecification : ISpecification<Entities.PlayerItem>
    {
        public bool IsSatisfiedBy(Entities.PlayerItem entity)
        {
            return entity.PlayerItemDateBought.Date >= DateTime.Now.Date;
        }
    }
}
