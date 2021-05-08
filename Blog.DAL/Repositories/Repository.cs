using Blog.DAL.EF;
using Blog.DAL.Interfaces.Entities;
using Blog.DAL.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public void Delete(TKey id)
        {
            TEntity entity = DbSet.FirstOrDefault(e => e.Id.Equals(id));
        }


        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TKey Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
