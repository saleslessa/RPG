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

        public AttributeViewModel Add(AttributeViewModel model)
        {
            var att = Mapper.Map<AttributeViewModel, Attributes>(model);
            att.AttributeBonus = new List<Attributes>();

            foreach (var attBonus in model.AttributeBonus.Where(attBonus => attBonus.Selected))
            {
                att.AttributeBonus.Add(_attributeService.Get(attBonus.AttributeId));
                _attributeService.AddParent(_attributeService.Get(attBonus.AttributeId), att);
            }

            att = _attributeService.Add(att);

            Commit();

            return Mapper.Map<Attributes, AttributeViewModel>(att);
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
                    foreach (var selected in att.AttributeBonus.Where(selected => item.AttributeId == selected.AttributeId))
                    {
                        item.Selected = true;
                        break;
                    }
                }
                else
                {
                    item.Selected = false;
                }
            }

            return result.ToList();
        }

        public void Remove(Guid attributeId)
        {
            _attributeService.RemoveChilds(attributeId);
            _attributeService.RemoveParent(attributeId);

            _attributeService.Remove(attributeId);

            Commit();
        }

        public AttributeViewModel Update(AttributeViewModel attributeViewModel)
        {
            var att = Mapper.Map<AttributeViewModel, Attributes>(attributeViewModel);

            att.AttributeBonus.Clear();

            _attributeService.Update(att);


            foreach (var item in attributeViewModel.AttributeBonus)
                _attributeService.RemoveParent(item.AttributeId, attributeViewModel.AttributeId);

            _attributeService.RemoveChilds(attributeViewModel.AttributeId);

            foreach (var bonus in from attBonus in attributeViewModel.AttributeBonus where attBonus.Selected select _attributeService.Get(attBonus.AttributeId))
            {
                _attributeService.AddChild(att, bonus);
                _attributeService.AddParent(bonus, att);
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

        public List<AttributeViewModel> SearchByName(string name)
        {
            return Mapper.Map<List<Attributes>, List<AttributeViewModel>>(_attributeService.SearchByName(name).ToList());
        }

        public List<AttributeViewModel> SearchByAttributeType(AttributeType? type)
        {
            return Mapper.Map<List<Attributes>, List<AttributeViewModel>>(type != null ? _attributeService.Search(t => t.AttributeType == type).ToList() : _attributeService.ListAll().ToList());
        }
    }
}
