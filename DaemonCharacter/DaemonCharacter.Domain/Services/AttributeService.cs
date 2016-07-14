using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Validations.Attribute;
using System.Linq;

namespace DaemonCharacter.Domain.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;

        public AttributeService(IAttributeRepository AttributeRepository)
        {
            _attributeRepository = AttributeRepository;
        }

        public Attributes Add(Attributes att)
        {
            att.ValidationResult = new CreateAttributeValidator(_attributeRepository).Validate(att);
            if (!att.ValidationResult.IsValid)
            {
                return att;
            }

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
            return _attributeRepository.Search(t => t.ParentAttribute.Where(tt => tt.AttributeId == AttributeId).Count() > 0).ToList();
        }

        public IEnumerable<Attributes> ListAll()
        {
            return _attributeRepository.ListAll();
        }

        public IEnumerable<Attributes> ListAvailableForBonus(Guid? SelectedAttribute)
        {
            return _attributeRepository.Search(t => t.AttributeType != AttributeType.Characteristic
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

        public void AddParent(Guid _attribute, Guid parent)
        {
            var att = Get(_attribute);
            var attParent = Get(parent);

            att.ParentAttribute.Add(attParent);
            _attributeRepository.Update(att);
        }

        public void AddChild(Guid _attribute, Guid child)
        {
            var att = Get(_attribute);
            var attChild = Get(child);

            att.AttributeBonus.Add(attChild);
            _attributeRepository.Update(att);
        }

        public void RemoveParent(Guid att, Guid parent)
        {
            var child = Get(att);
            var par = Get(parent);

            if (child.ParentAttribute.Remove(par))
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
            att.ValidationResult = new UpdateAttributeValidator(_attributeRepository).Validate(att);

            if (!att.ValidationResult.IsValid)
            {
                return att;
            }

            att.ValidationResult.Message = "Attribute updated successfully";
            return _attributeRepository.Update(att);
        }
    }
}
