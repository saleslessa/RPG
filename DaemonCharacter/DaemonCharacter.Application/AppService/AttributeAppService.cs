using System;
using System.Collections.Generic;
using DaemonCharacter.Application.Interfaces;
using DaemonCharacter.Application.ViewModels.Attribute;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Domain.Interfaces.Service;
using AutoMapper;
using DaemonCharacter.Domain.Entities;
using System.Linq;

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
            att.AttributeBonus = new List<Attributes>();

            att = _attributeService.Add(att);

            if (!att.ValidationResult.IsValid)
                return Mapper.Map<Attributes, AttributeViewModel>(att);

            foreach (var AttBonus in AttributeViewModel.AttributeBonus)
            {
                if (AttBonus.Selected)
                {
                    _attributeService.AddChild(att.AttributeId, AttBonus.AttributeId);
                    _attributeService.AddParent(AttBonus.AttributeId, att.AttributeId);
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

        public List<AttributeBonusViewModel> ListAvailableForBonus(Guid? SelectedAttribute)
        {
            var result = Mapper.Map<IEnumerable<Attributes>, IEnumerable<AttributeBonusViewModel>>(_attributeService.ListAvailableForBonus(SelectedAttribute));

            var att = _attributeService.Get(SelectedAttribute);

            foreach (var item in result)
            {
                if (att != null)
                {
                    foreach (var selected in att.AttributeBonus)
                    {
                        if (item.AttributeId == selected.AttributeId)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

            return result.ToList();
        }

        public void Remove(Guid AttributeId)
        {
            _attributeService.RemoveChilds(AttributeId);
            _attributeService.RemoveParent(AttributeId);

            _attributeService.Remove(AttributeId);

            Commit();
        }

        public AttributeViewModel Update(AttributeViewModel _AttributeViewModel)
        {

            var att = Mapper.Map<AttributeViewModel, Attributes>(_AttributeViewModel);

            att.AttributeBonus.Clear();

            _attributeService.Update(att);


            foreach (var item in _AttributeViewModel.AttributeBonus)
                _attributeService.RemoveParent(item.AttributeId, _AttributeViewModel.AttributeId);

            _attributeService.RemoveChilds(_AttributeViewModel.AttributeId);

            foreach (var AttBonus in _AttributeViewModel.AttributeBonus)
            {
                if (AttBonus.Selected)
                {
                    _attributeService.AddChild(att.AttributeId, AttBonus.AttributeId);
                    _attributeService.AddParent(AttBonus.AttributeId, att.AttributeId);
                }
            }

            Commit();
            return Mapper.Map<Attributes, AttributeViewModel>(att);
        }

        public List<AttributeViewModel> ListWithPagination(int skip, int take)
        {
            return Mapper.Map<List<Attributes>, List<AttributeViewModel>>(_attributeService.ListWithPagination(skip, take).ToList());
        }

        public List<AttributeViewModel> SearchByNameWithPagination(int skip, int take, string name)
        {
            return Mapper.Map<List<Attributes>, List<AttributeViewModel>>(_attributeService.SearchByNameWithPagination(skip, take, name).ToList());
        }
    }
}
