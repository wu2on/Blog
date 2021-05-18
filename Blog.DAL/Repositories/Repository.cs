using Blog.DAL.EF;
using Blog.DAL.Interfaces.Entities;
using Blog.DAL.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly BlogContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(BlogContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public void CreateMany(ICollection<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Delete(TKey id)
        {
            TEntity entity = DbSet.FirstOrDefault(e => e.Id.Equals(id));
        }

        public Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }
        public IEnumerable<TEntity> GetRange(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetRange(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate);
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
