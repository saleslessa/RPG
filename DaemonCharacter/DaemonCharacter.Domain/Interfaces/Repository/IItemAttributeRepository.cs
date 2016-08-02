using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IItemAttributeRepository : IRepository<ItemAttribute>
    {
        IEnumerable<ItemAttribute> ListFromItem(Guid? AttributeId);

        IEnumerable<ItemAttribute> ListFromAttribute(Guid? AttributeId);

        ItemAttribute Get(Guid? ItemId, Guid? AttributeId);

    }
}
