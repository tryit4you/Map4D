﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Map4D
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PostionDetail",
                url: "{dia-diem}",
                defaults: new { controller = "PolygonDetail", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DrawPolygon", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}