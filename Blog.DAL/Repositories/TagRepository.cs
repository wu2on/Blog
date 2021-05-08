using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces.Repositories;

namespace Blog.DAL.Repositories
{
    public class TagRepository : Repository<Tag, int>, ITagRepository
    {
        public TagRepository(BlogContext context) : base(context)
        {

        }
    }
}