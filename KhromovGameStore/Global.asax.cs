using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KhromovGameStore.Models;

namespace KhromovGameStore
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DbInitializer());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
