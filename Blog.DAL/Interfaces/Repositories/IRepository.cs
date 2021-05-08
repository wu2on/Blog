using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace Blog.DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        TEntity Create(TEntity entity);
        void CreateMany(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TKey id);
    }
}
