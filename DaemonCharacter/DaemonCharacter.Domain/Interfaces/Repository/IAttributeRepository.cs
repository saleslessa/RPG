using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IAttributeRepository : IRepository<Attributes>
    {
        IEnumerable<Attributes> ListWithBonus(Guid? id);

        IEnumerable<Attributes> GetBonusAttribute(Guid? id);

        void RemoveParent(Guid? id);

        void RemoveChilds(Guid? id);

        Attributes GetByName(string name);

        Attributes GetUpdateable(Guid id, string name);

        IEnumerable<Attributes> SearchByName(string name);

    }
}
