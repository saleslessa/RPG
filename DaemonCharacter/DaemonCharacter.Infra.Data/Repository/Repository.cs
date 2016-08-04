using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DaemonCharacterContext db;
        protected DbSet<TEntity> DbSet;

        public Repository(DaemonCharacterContext context)
        {
            db = context;
            DbSet = db.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            var result = DbSet.Add(obj);
            //db.SaveChanges();
            return result;
        }

        public void Dispose()
        {
            db.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> ListAll()
        {
            return DbSet.ToList();
        }

        public void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            //db.SaveChanges();
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public TEntity Get(Guid? id)
        {
            return DbSet.Find(id);
        }

        public TEntity Update(TEntity obj)
        {
            var entry = db.Entry(obj);
            
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
            //db.SaveChanges();
            return obj;
        }

        public IEnumerable<TEntity> ListWithPagination(Expression<Func<TEntity, object>> OrderBy, int skip, int take)
        {
            return DbSet.ToList();
            //TODO: AJUST PAGINATION
            //return DbSet.OrderBy(OrderBy).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<TEntity> SearchWithPagination(Expression<Func<TEntity, object>> OrderBy, int skip, int take, Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
            //TODO: AJUST PAGINATION
            //return DbSet.Where(predicate).OrderBy(OrderBy).Skip(skip).Take(take).ToList();
        }

    }
}
