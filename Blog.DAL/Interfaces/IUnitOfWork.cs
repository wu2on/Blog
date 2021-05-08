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
        IUserProfileRepository UserProfileRepository { get; }
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        ITagRepository TagRepository { get; }

        Task<bool> SaveAsync();
    }
}
