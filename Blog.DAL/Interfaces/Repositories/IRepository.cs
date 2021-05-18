using Blog.DAL.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Create(TEntity entity);
        void CreateMany(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TKey id);

        IEnumerable<TEntity> GetRange(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetRange(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
