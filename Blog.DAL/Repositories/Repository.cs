using Blog.DAL.EF;
using Blog.DAL.Interfaces.Repositories;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
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

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(string id)
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
