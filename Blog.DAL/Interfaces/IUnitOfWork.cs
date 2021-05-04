using Blog.DAL.Identity;
using Blog.DAL.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BlogUserManager UserManager { get; }
        BlogRoleManager RoleManager { get; }
        IClientProfileRepository ClientManager { get; }

        Task<bool> SaveAsync();
    }
}
