using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DaemonCharacter.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);

        TEntity Get(Guid? id);

        IEnumerable<TEntity> ListAll();

        TEntity Update(TEntity obj);

        void Remove(Guid id);

        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);

        void SaveChanges();
    }
}
