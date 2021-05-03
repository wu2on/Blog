using Blog.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BlogUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        BlogRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
