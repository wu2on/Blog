using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.DAL.Entities
{
    public class User : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}