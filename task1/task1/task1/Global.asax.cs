using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

using task1.DataAccess;

namespace task1 {

  public class MvcApplication : System.Web.HttpApplication {
    protected void Application_Start() {
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      Database.SetInitializer<ExchangeRatesContext>(
        new ExchangeRatesInitializer());
    }
  }
}