using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IItemAttributeService : IDisposable
    {
        ItemAttribute Add(ItemAttribute model);

        ItemAttribute Update(ItemAttribute model);

        IEnumerable<ItemAttribute> ListFromItem(Guid? itemId);

        IEnumerable<ItemAttribute> ListFromAttribute(Guid? attributeId);

        ItemAttribute Get(Guid ItemAttributeId);

        void Remove(Guid itemAttributeId);

    }
}
