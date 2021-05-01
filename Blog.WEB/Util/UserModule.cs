using Blog.BLL.Interfaces;
using Blog.BLL.Service;
using Ninject.Modules;

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