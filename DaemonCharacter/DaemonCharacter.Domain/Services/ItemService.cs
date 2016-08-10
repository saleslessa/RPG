using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;
using System.Linq;

namespace DaemonCharacter.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Item Add(Item model)
        {
            model.ValidationResult.Message = "Item created successfully";
            return _itemRepository.Add(model);
        }

        public void Dispose()
        {
            _itemRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Item Get(Guid? id)
        {
            return _itemRepository.Get(id);
        }

        public IEnumerable<Item> ListAll()
        {
            return _itemRepository.ListAll();
        }

        public void Remove(Guid id)
        {
            _itemRepository.Remove(id);
        }

        public Item SearchByName(string name)
        {
            return _itemRepository.Search(t => t.ItemName.ToUpper().Trim() == name.Trim().ToUpper()).FirstOrDefault();
        }

        public Item Update(Item model)
        {
            model.ValidationResult.Message = "Item updated successfully";
            return _itemRepository.Update(model);
        }
    }
}
