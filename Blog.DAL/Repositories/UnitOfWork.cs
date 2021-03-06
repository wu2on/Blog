using System;
using System.Threading.Tasks;
using System.Diagnostics;

using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Identity;
using Blog.DAL.Interfaces;
using Blog.DAL.Interfaces.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BlogContext _context;

        private BlogUserManager _userManager;
        private BlogRoleManager _roleManager;
        private IUserProfileRepository _userProfileRepository;
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private ITagRepository _tagRepository;

        public UnitOfWork(string connectionString)
        {
            _context = new BlogContext(connectionString);
        }

        public BlogUserManager UserManager => _userManager ?? (_userManager = new BlogUserManager(new UserStore<User>(_context)));

        public BlogRoleManager RoleManager => _roleManager ?? (_roleManager = new BlogRoleManager(new RoleStore<Role>(_context)));

        public IUserProfileRepository UserProfileRepository => _userProfileRepository ?? (_userProfileRepository = new UserProfilesRepository(_context));

        public IPostRepository PostRepository => _postRepository ?? (_postRepository = new PostRepository(_context));
        public ICommentRepository CommentRepository => _commentRepository ?? ( _commentRepository = new CommentRepository(_context));
        public ITagRepository TagRepository => _tagRepository ?? ( _tagRepository = new TagRepository(_context));
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
                      
        }

       
        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
