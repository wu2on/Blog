using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using Blog.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;



namespace Blog.DAL.EF
{
    public class BlogContext : IdentityDbContext<User>
    {
        public BlogContext(string conectionString) : base(conectionString) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }

    public class MigrationsContextFactory : IDbContextFactory<BlogContext>
    {
        public BlogContext Create()
        {
            return new BlogContext("Blog");
        }
    }
}