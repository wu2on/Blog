using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces.Repositories;

namespace Blog.DAL.Repositories
{
    public class UserProfilesRepository : Repository<UserProfile, string>, IUserProfileRepository
    {
        public UserProfilesRepository(BlogContext context) : base(context)
        {

        }
    }
}
