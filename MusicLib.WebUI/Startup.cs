using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using System.Configuration;

[assembly: OwinStartup(typeof(MusicLib.WebUI.Startup))]

namespace MusicLib.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var container = AutofacBootstraper.Build(config);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(config);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            app.UseAutofacMiddleware(container);

            app.Map("/api", inner =>
            {
                inner.UseAutofacWebApi(config);
                inner.UseWebApi(config);
            });
        }
    }
}
