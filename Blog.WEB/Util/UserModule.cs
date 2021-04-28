using Ninject.Modules;
using Blog.BLL.Service;
using Blog.BLL.Interfaces;

namespace Blog.WEB.Util
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
        }
    }
}