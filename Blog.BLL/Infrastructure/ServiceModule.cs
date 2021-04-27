using Ninject.Modules;
using Blog.DAL.Interfaces;
using Blog.DAL.Repositories;

namespace Blog.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
