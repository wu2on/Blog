using Blog.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace Blog.DAL.Identity
{
    public class BlogUserManager : UserManager<User>
    {
        public BlogUserManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}