using Autofac;
using Autofac.Integration.WebApi;
using MusicLib.Framework.Repos;
using MusicLib.Services.AudioService;
using MusicLib.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace MusicLib.WebUI
{
    public static class AutofacBootstraper
    {
        public static IContainer Build(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);


            builder.RegisterAssemblyTypes(typeof(HomeController).Assembly)
                .Where(t => t.Name.EndsWith("Impl"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IAudioService).Assembly)
                .Where(t => t.Name.EndsWith("Impl"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(IAudioFileRepository).Assembly)
                .Where(t => t.Name.EndsWith("Impl"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            var container = builder.Build();
            return container;
        }
    }
}