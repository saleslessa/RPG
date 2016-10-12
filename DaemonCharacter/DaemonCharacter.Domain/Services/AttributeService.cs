using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Validations.Attribute;
using System.Linq;
using System.Linq.Expressions;

namespace DaemonCharacter.Domain.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;

        public AttributeService(IAttributeRepository attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public Attributes Add(Attributes att)
        {
            if (!att.IsValid())
                return att;

            att.ValidationResult = new CreateAttributeValidation(_attributeRepository).Validate(att);
            if (!att.ValidationResult.IsValid)
                return att;


            att.ValidationResult.Message = "Attribute created successfully";
            return _attributeRepository.Add(att);
        }

        public void Dispose()
        {
            _attributeRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public Attributes Get(Guid? AttributeId)
        {
            return _attributeRepository.Get(AttributeId);
        }

        public List<Attributes> ListBonusAttribute(Guid AttributeId)
        {
            return _attributeRepository.Search(t => t.AttributeBonus.Where(tt => tt.AttributeId == AttributeId).Count() > 0).ToList();
        }

        public IEnumerable<Attributes> ListAll()
        {
            return _attributeRepository.ListAll();
        }

        public IEnumerable<Attributes> ListAvailableForBonus(Guid? SelectedAttribute)
        {
            return _attributeRepository.Search(t => t.AttributeType != AttributeType.Talent
            && (t.AttributeId != SelectedAttribute || SelectedAttribute == null));
        }


        public void Remove(Guid AttributeId)
        {
            _attributeRepository.Remove(AttributeId);
        }

        public Attributes RemoveChilds(Guid _Attribute)
        {
            var att = Get(_Attribute);

            att.AttributeBonus.Clear();
            return _attributeRepository.Update(att);
        }

        public void AddParent(Attributes attribute, Attributes parent)
        {
            attribute.ParentAttribute.Add(parent);
            _attributeRepository.Update(attribute);
        }

        public void AddChild(Attributes attribute, Attributes child)
        {
            attribute.AttributeBonus.Add(child);
            _attributeRepository.Update(attribute);
        }

        public void RemoveParent(Guid att, Guid parent)
        {
            var child = Get(att);
            var par = Get(parent);

            if (child != null && child.ParentAttribute != null && child.ParentAttribute.Remove(par))
                _attributeRepository.Update(child);
        }

        public void RemoveParent(Guid AttributeId)
        {
            var att = Get(AttributeId);

            att.ParentAttribute.Clear();
            _attributeRepository.Update(att);
        }

        public Attributes Update(Attributes att)
        {
            att.ValidationResult = new UpdateAttributeValidation(_attributeRepository).Validate(att);

            if (!att.ValidationResult.IsValid)
            {
                return att;
            }

            att.ValidationResult.Message = "Attribute updated successfully";
            return _attributeRepository.Update(att);
        }

        public IEnumerable<Attributes> ListWithPagination(int skip, int take)
        {
            return _attributeRepository.ListWithPagination(o => o.AttributeType, skip, take);
        }

        public IEnumerable<Attributes> SearchByNameWithPagination(int skip, int take, string name)
        {
            return _attributeRepository.SearchWithPagination(o => o.AttributeType, skip, take, s => s.AttributeName.ToUpper().Trim() == name.ToUpper().Trim());
        }

        public IEnumerable<Attributes> SearchByName(string name)
        {
            return _attributeRepository.SearchByName(name);
        }

        public IEnumerable<Attributes> Search(Expression<Func<Attributes, bool>> predicate)
        {
            return _attributeRepository.Search(predicate);
        }

        public IEnumerable<Guid> ListBonusAttributeIds(Guid AttributeId)
        {
            return _attributeRepository.Search(t => t.AttributeBonus.Where(tt => tt.AttributeId == AttributeId).Count() > 0).Select(s => s.AttributeId).ToList();
        }
    }
}
