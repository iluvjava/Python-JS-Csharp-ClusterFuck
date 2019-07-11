using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


/// <summary>
/// This is where the routing is. 
/// navigate between controller and action. 
/// </summary>
namespace MyWebAppAttempt
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // 
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // this is a route handler, where the below codes 
            // represents the template for url and where it matches things. 
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", // home/about
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
                //^
                // Specifying the controller and action above. 
                // Specifying the default webapge it's going into. 
                // You shouldn't change this, leave this alone until you know how to 
                // add new settings for routing. 
            );
        }
    }
}
