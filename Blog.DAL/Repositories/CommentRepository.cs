using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces.Repositories;

namespace Blog.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context)
        {

        }
    }
}
