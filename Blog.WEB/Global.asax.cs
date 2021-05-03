using Blog.BLL.Infrastructure;
using Blog.WEB.Util;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Blog.WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            NinjectModule userModule = new UserModule();
            NinjectModule serviceModule = new ServiceModule("Blog");
            var kernel = new StandardKernel(userModule, serviceModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
