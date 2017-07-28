using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TRAVELPLUS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Level1",
                url: "duyet-po-cap-do-1/{id}",
                defaults: new { controller = "POApprovalManage", action = "Level1", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Level2",
                url: "duyet-po-cap-do-2/{id}",
                defaults: new { controller = "POApprovalManage", action = "Level2", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Level3",
                url: "duyet-po-cap-do-3/{id}",
                defaults: new { controller = "POApprovalManage", action = "Level3", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Level4",
                url: "duyet-po-cap-do-4/{id}",
                defaults: new { controller = "POApprovalManage", action = "Level4", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Level5",
                url: "duyet-po-cap-do-5/{id}",
                defaults: new { controller = "POApprovalManage", action = "Level5", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Level6",
                url: "duyet-po-cap-do-6/{id}",
                defaults: new { controller = "POApprovalManage", action = "Level6", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
