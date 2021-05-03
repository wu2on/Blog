using Blog.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
