using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using BusinessLayer;
using BusinessLayer.Configuration;
using Castle.Windsor;
using WebApi;
using WebApi.Config;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container = new WindsorContainer();
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            container.Install(new BusinessLayerInstaller());
            container.Install(new WebApiInstaller());

            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(container.Kernel);
        }

        public override void Dispose()
        {
            container.Dispose();
            base.Dispose();
        }
    }
}
