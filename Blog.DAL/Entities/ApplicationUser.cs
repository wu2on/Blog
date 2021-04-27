using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}