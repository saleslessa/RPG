using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Application.ViewModels.Item;
using AutoMapper;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Application.ViewModels.Attribute;
using System.Linq;

namespace DaemonCharacter.Application.AppService
{
    public class ItemAppService : ApplicationService, IItemAppService
    {
        private readonly IItemService _itemService;
        private readonly IAttributeService _attributeService;
        private readonly IItemAttributeService _itemAttributeService;

        public ItemAppService(IItemService itemService, IAttributeService attributeService, IItemAttributeService itemAttributeService, IUnitOfWork uow) : base(uow)
        {
            _itemService = itemService;
            _attributeService = attributeService;
            _itemAttributeService = itemAttributeService;
        }

        public ItemViewModel Add(ItemViewModel model)
        {
            var item = Mapper.Map<ItemViewModel, Item>(model);

            item = _itemService.Add(item);

            foreach (var obj in model.ItemAttribute)
            {
                if (obj.ItemAttributeValue != 0)
                    _itemAttributeService.Add(new ItemAttribute()
                    {
                        Attribute = _attributeService.Get(obj.Attribute.AttributeId),
                        Item = item,
                        ItemAttributeValue = obj.ItemAttributeValue
                    });
            }

            Commit();

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

        public IEnumerable<ItemAttributeViewModel> ListAvailableForBonus(Guid? itemId)
        {
            var result = new List<ItemAttributeViewModel>();
            var list = _attributeService.ListAvailableForBonus(null).ToList();
            var selected = itemId != null ? _itemAttributeService.ListFromItem(itemId.Value) : null;

            foreach (var att in list)
            {
                var viewModel = selected == null ? null : Mapper.Map<ItemAttribute, ItemAttributeViewModel>(selected
                    .Where(t => t.Attribute.AttributeId == att.AttributeId)
                    .FirstOrDefault());

                result.Add(viewModel == null ? new ItemAttributeViewModel() { Attribute = att } : viewModel);
            }
            return result;
        }

        public void Remove(Guid id)
        {
            _itemService.Remove(id);
            Commit();
        }

        public ItemViewModel Update(ItemViewModel model)
        {
            var item = Mapper.Map<ItemViewModel, Item>(model);

            if (!item.IsValid())
                return Mapper.Map<Item, ItemViewModel>(item);

            item = _itemService.Update(item);
            _itemAttributeService.RemoveFromItem(item.ItemId);

            foreach (var obj in model.ItemAttribute)
            {
                if (obj.ItemAttributeValue != 0)
                    _itemAttributeService.Add(new ItemAttribute()
                    {
                        Attribute = _attributeService.Get(obj.Attribute.AttributeId),
                        Item = item,
                        ItemAttributeValue = obj.ItemAttributeValue
                    });
            }

            Commit();
            
            return Mapper.Map<Item, ItemViewModel>(item);
        }
    }
}
