using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Service;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Domain.Validations.Attribute;

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
            if(!att.ValidationResult.IsValid)
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
            return _attributeRepository.SearchById(AttributeId);
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
