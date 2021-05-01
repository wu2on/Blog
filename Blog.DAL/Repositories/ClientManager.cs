using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System;

namespace Blog.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public BlogContext Database { get; set; }
        public ClientManager(BlogContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}