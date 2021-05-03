using Blog.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BlogUserManager UserManager { get; }
        BlogRoleManager RoleManager { get; }
        IClientManager ClientManager { get; }

        Task<bool> SaveAsync();
    }
}
