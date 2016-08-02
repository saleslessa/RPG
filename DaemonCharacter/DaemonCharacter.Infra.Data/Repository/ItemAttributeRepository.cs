using System;
using System.Linq;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class ItemAttributeRepository : Repository<ItemAttribute>, IItemAttributeRepository
    {
        public ItemAttributeRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public ItemAttribute Get(Guid? ItemId, Guid? AttributeId)
        {
            return Search(t => t.Item.ItemId == ItemId && t.Attribute.AttributeId == AttributeId)
                .FirstOrDefault();
        }

        public IEnumerable<ItemAttribute> ListFromAttribute(Guid? AttributeId)
        {
            return Search(t => t.Attribute.AttributeId == AttributeId)
                .ToList();
        }

        public IEnumerable<ItemAttribute> ListFromItem(Guid? ItemId)
        {
            return Search(t => t.Item.ItemId == ItemId)
                .ToList();
        }
    }
}
