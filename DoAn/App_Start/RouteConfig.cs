using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAn
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new[]{ "DoAn.Controllers" }
			);
			routes.MapRoute(
	name: "OrderDetail",
	url: "Admin/Order/Detail/{id}",
	defaults: new { controller = "Order", action = "Detail", id = UrlParameter.Optional }
);
		}
	}
}
