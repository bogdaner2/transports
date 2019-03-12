using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Transports.Web.Forms
{
    public class Global : HttpApplication
    {
        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                "Drivers", "drivers", "~/DriversPage.aspx");
            routes.MapPageRoute(
                "default", "", "~/DriversPage.aspx");
            routes.MapPageRoute(
                "driverCreate", "driverCreate", "~/DriverCreatePage.aspx");

        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}