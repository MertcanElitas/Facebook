using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Facebook.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);



           


            routes.MapRoute("Profile", "Profile/{action}/{id}", new { controller = "Profile", action = "Index",id=UrlParameter.Optional });


         


            routes.MapRoute("Account", "{username}",new { controller = "Account", action = "Username" });

            routes.MapRoute("CreateAccount", "Account/CreateAccount", new { controller = "Account", action = "CreateAccount" });
            routes.MapRoute("Login", "Account/Login", new { controller = "Account", action = "Login" });
            routes.MapRoute("Deafult", "", new { controller = "Account", action = "Login" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
