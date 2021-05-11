using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey id);
        IQueryable<TEntity> Find(Expression<Func<TEntity, Boolean>> predicate);
        TEntity Create(TEntity entity);
        void CreateMany(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TKey id);

        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
