using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using Blog.DAL.Identity;

namespace Blog.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private BlogContext db;

        private BlogUserManager userManager;
        private BlogRoleManager roleManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new BlogContext(connectionString);
            userManager = new BlogUserManager(new UserStore<User>(db));
            roleManager = new BlogRoleManager(new RoleStore<Role>(db));
            clientManager = new ClientManager(db);
        }

        public BlogUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public BlogRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
