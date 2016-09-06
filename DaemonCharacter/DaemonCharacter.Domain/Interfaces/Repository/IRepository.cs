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

        IEnumerable<TEntity> ListWithPagination(Expression<Func<TEntity, object>> OrderBy, int skip, int take);

        TEntity Update(TEntity obj);

        void Remove(Guid id);

        void Remove(IEnumerable<TEntity> entity);

        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> SearchWithPagination(Expression<Func<TEntity, object>> OrderBy, int skip, int take, Expression<Func<TEntity, bool>> predicate);
    }
}
