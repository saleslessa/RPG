using System;
using System.Collections.Generic;
using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using AutoMapper;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AppService
{
    public class AttributeAppService : ApplicationService, IAttributeAppService
    {
        private readonly IAttributeService _attributeService;

        public AttributeAppService(IAttributeService attributeService, IUnitOfWork uow)
            : base(uow)
        {
            _attributeService = attributeService;
        }

        public AttributeViewModel Add(AttributeViewModel AttributeViewModel)
        {
            var att = Mapper.Map<AttributeViewModel, Attributes>(AttributeViewModel);

            BeginTransaction();

            _attributeService.Add(att);

            foreach (var AttBonus in AttributeViewModel.AttributeBonus)
            {
                if (AttBonus.Selected)
                {
                    var AttributeChild = _attributeService.Get(AttBonus.AttributeId);

                    att.AttributeBonus.Add(AttributeChild);
                    AttributeChild.ParentAttribute.Add(att);

                    _attributeService.Update(att);
                    _attributeService.Update(AttributeChild);
                }
            }

            Commit();

            return AttributeViewModel;
        }

        public void Dispose()
        {
            _attributeService.Dispose();
            GC.SuppressFinalize(this);
        }

        public AttributeViewModel Get(Guid? AttributeId)
        {
            return Mapper.Map<Attributes, AttributeViewModel>(_attributeService.Get(AttributeId));
        }

        public IEnumerable<AttributeViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<Attributes>, IEnumerable<AttributeViewModel>>
                (_attributeService.ListAll());
        }

        public IEnumerable<AttributeBonusViewModel> ListAvailableForBonus(Guid? SelectedAttribute)
        {
            var result = Mapper.Map<IEnumerable<Attributes>, IEnumerable<AttributeBonusViewModel>>(_attributeService.ListAvailableForBonus(SelectedAttribute));

            var att = _attributeService.Get(SelectedAttribute);

            foreach (var item in result)
            {
                if (att != null)
                {
                    foreach (var selected in att.AttributeBonus)
                    {
                        item.Selected = item.AttributeId == selected.AttributeId;
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

            return result;
        }

        public void Remove(Guid AttributeId)
        {
            _attributeService.Remove(AttributeId);
        }

        public AttributeViewModel Update(AttributeViewModel _AttributeViewModel)
        {
            return Mapper.Map<Attributes, AttributeViewModel>(
                _attributeService.Update(
                    Mapper.Map<AttributeViewModel, Attributes>(_AttributeViewModel)
                    )
                );
        }
    }
}
