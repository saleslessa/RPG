using DaemonCharacter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using System.Linq;

namespace DaemonCharacter.Domain.Services
{
    public class ItemAttributeService : IItemAttributeService
    {
        private readonly IItemAttributeRepository _itemAttributeRepository;

        public ItemAttributeService(IItemAttributeRepository itemAttributeRepository)
        {
            _itemAttributeRepository = itemAttributeRepository;
        }

        public ItemAttribute Add(ItemAttribute model)
        {
            if (!model.IsValid())
                return model;

            return _itemAttributeRepository.Add(model);
        }

        public void Dispose()
        {
            _itemAttributeRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public ItemAttribute Get(Guid itemAttributeId)
        {
            return _itemAttributeRepository.Get(itemAttributeId);
        }

        public ItemAttribute Get(Guid itemId, Guid attributeId)
        {
            return _itemAttributeRepository.Search(t => t.Attribute.AttributeId == attributeId && t.Item.ItemId == itemId).FirstOrDefault();
        }

        public IEnumerable<ItemAttribute> ListFromAttribute(Guid? attributeId)
        {
            return _itemAttributeRepository.Search(t => t.Attribute.AttributeId == attributeId).ToList();
        }

        public IEnumerable<ItemAttribute> ListFromItem(Guid? itemId)
        {
            return _itemAttributeRepository.Search(t => t.Item.ItemId == itemId).ToList();
        }

        public void Remove(Guid itemAttributeId)
        {
            _itemAttributeRepository.Remove(itemAttributeId);
        }

        public void RemoveFromItem(Guid itemId)
        {
            _itemAttributeRepository.Remove(_itemAttributeRepository.ListFromItem(itemId));
        }

        public ItemAttribute Update(ItemAttribute model)
        {
            if (!model.IsValid())
                return model;

            return _itemAttributeRepository.Update(model);
        }
    }
}
