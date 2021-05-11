using Blog.DAL.EF;
using Blog.DAL.Interfaces.Entities;
using Blog.DAL.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

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


        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = DbSet;

            return query.Where(predicate);
        }

        public TEntity Get(TKey Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
