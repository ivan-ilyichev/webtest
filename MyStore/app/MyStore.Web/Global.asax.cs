using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyStore.Domain;
using MyStore.Init;
using MyStore.Web.App_Start;
using MyStore.Web.Binders;
using SharpLite.Web.Mvc.ModelBinder;

namespace MyStore.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start() {
            log4net.Config.XmlConfigurator.Configure();
            DependencyResolverInitializer.Initialize();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            //GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());
            
            //RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            ModelBinders.Binders.Add(typeof(Money), new MoneyBinder());

        }
    }
}