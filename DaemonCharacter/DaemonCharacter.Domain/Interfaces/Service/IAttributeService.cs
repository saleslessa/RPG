using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IAttributeService : IDisposable
    {

        Attributes Add(Attributes _attribute);

        Attributes Get(Guid? AttributeId);

        IEnumerable<Attributes> ListAll();

        Attributes Update(Attributes _attribute);

        void Remove(Guid AttributeId);

        IEnumerable<Attributes> ListAvailableForBonus(Guid? SelectedAttribute);

        Attributes RemoveChilds(Guid _Attribute);

        void RemoveParent(Guid AttributeId);

        void RemoveParent(Guid att, Guid parent);

        List<Attributes> ListBonusAttribute(Guid AttributeId);

        void AddChild(Guid _attribute, Guid child);

        void AddParent(Guid _attribute, Guid parent);
    }
}
