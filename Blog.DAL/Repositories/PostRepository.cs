using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces.Repositories;

namespace Blog.DAL.Repositories
{
    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(BlogContext context) : base(context)
        {

        }
    }
}
