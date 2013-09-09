using async_web.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace async_web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Won't work for IIS :(
            //ThreadPool.SetMaxThreads(1000, 15000);
            //ThreadPool.SetMinThreads(100, 10000);
            
            AreaRegistration.RegisterAllAreas();
            
            // Initialize ComputerDb database
            Database.SetInitializer<ComputerDb>(new ComputerDbInitializer());
            using (var context = new ComputerDb())
            {
                context.Database.Initialize(true);
            }

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}