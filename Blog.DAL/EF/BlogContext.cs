using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Blog.DAL.Entities;
using System.Data.Entity.Infrastructure;

namespace Blog.DAL.EF
{
    public class BlogContext : IdentityDbContext<User>
    {
        public BlogContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }

    public class MigrationsContextFactory : IDbContextFactory<BlogContext>
    {
        public BlogContext Create()
        {
            return new BlogContext("DefaultConnection");
        }
    }
}