using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces.Repositories;

namespace Blog.DAL.Repositories
{
    public class ClientProfilesRepository : Repository<ClientProfile>, IClientProfileRepository
    {
        public ClientProfilesRepository(BlogContext context) : base(context)
        {

        }
    }
}
