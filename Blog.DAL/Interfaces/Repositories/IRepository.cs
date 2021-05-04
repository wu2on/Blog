using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(string id);
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(string id);
    }
}
