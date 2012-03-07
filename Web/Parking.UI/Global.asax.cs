using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Parking.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("Api/{*id}");

            routes.MapRoute("about", "about", new { controller = "Home", action="blank" });
            routes.MapRoute("help", "help", new { controller = "Home", action = "blank" });
            routes.MapRoute("privacy", "privacy", new { controller = "Home", action = "blank" });
            routes.MapRoute("terms", "terms", new { controller = "Home", action = "blank" });
            routes.MapRoute("status", "status", new { controller = "Home", action = "blank" });
            routes.MapRoute("login", "login", new { controller = "Account", action = "LogOn" });
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Blank", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}