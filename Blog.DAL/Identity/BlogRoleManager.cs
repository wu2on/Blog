using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Blog.DAL.Entities;

namespace Blog.DAL.Identity
{
    public class BlogRoleManager : RoleManager<Role>
    {
        public BlogRoleManager(RoleStore<Role> store)
                    : base(store)
        { 
        }
    }
}
