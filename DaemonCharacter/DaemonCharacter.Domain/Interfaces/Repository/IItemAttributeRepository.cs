using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IItemAttributeRepository : IRepository<ItemAttributes>
    {
        IEnumerable<ItemAttributes> ListFromItem(Guid? AttributeId);

        IEnumerable<ItemAttributes> ListFromAttribute(Guid? AttributeId);

        ItemAttributes Get(Guid? ItemId, Guid? AttributeId);
    }
}
