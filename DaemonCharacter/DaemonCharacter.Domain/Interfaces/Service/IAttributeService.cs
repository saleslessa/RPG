using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IAttributeService : IDisposable
    {

        Attributes Add(Attributes _attribute);

        Attributes Get(Guid? AttributeId);

        IEnumerable<Attributes> ListAll();

        IEnumerable<Attributes> ListWithPagination(int take, int skip);

        Attributes Update(Attributes _attribute);

        void Remove(Guid AttributeId);

        IEnumerable<Attributes> ListAvailableForBonus(Guid? SelectedAttribute);

        Attributes RemoveChilds(Guid _Attribute);

        void RemoveParent(Guid AttributeId);

        void RemoveParent(Guid att, Guid parent);

        List<Attributes> ListBonusAttribute(Guid AttributeId);

        IEnumerable<Guid> ListBonusAttributeIds(Guid AttributeId);

        void AddChild(Attributes attribute, Attributes child);

        void AddParent(Attributes attribute, Attributes parent);

        IEnumerable<Attributes> SearchByNameWithPagination(int skip, int take, string name);

        IEnumerable<Attributes> SearchByName(string name);

        IEnumerable<Attributes> Search(Expression<Func<Attributes, bool>> predicate);
    }
}
