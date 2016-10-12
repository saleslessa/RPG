using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IMagicService
    {
        Magic Add(Magic model);

        Magic Update(Magic model);

        Magic Get(Guid id);

        IEnumerable<Magic> ListAll();

        IEnumerable<Magic> SearchByName(string name);

        IEnumerable<Magic> Search(Expression<Func<Magic, bool>> predicate);

        void Remove(Guid id);
    }
}
