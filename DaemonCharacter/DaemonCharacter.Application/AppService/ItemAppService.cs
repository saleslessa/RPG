using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Application.ViewModels.Item;
using AutoMapper;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AppService
{
    public class ItemAppService : ApplicationService, IItemAppService
    {
        private readonly IItemService _itemService;

        public ItemAppService(IItemService itemService, IUnitOfWork uow) : base(uow)
        {
            _itemService = itemService;
        }

        public ItemViewModel Add(ItemViewModel model)
        {
            var item = Mapper.Map<ItemViewModel, Item>(model);
            //TODO: VALIDATIONS
            item = _itemService.Add(item);
            Commit();

            item.ValidationResult.Message = "Item created successfully";
            return Mapper.Map<Item, ItemViewModel>(item);
        }

        public void Dispose()
        {
            _itemService.Dispose();
            GC.SuppressFinalize(this);
        }

        public ItemViewModel Get(Guid? id)
        {
            return Mapper.Map<Item, ItemViewModel>(_itemService.Get(id));
        }

        public IEnumerable<ItemViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(_itemService.ListAll());
        }

        public void Remove(Guid id)
        {
            _itemService.Remove(id);
        }

        public ItemViewModel Update(ItemViewModel model)
        {
            var item = Mapper.Map<ItemViewModel, Item>(model);
            //TODO: VALIDATIONS
            item = _itemService.Update(item);

            return Mapper.Map<Item, ItemViewModel>(item);
        }
    }
}
