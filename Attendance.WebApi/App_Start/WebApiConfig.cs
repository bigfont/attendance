using Attendance.WebApi.Resolver;
using Attendance.WebApi.Services;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Attendance.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            SetErrorPolicy();
            EnableCrossSiteRequests(config);
            ConfigureRoutes(config);
            ConfigureDependencyResolver(config);

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

#if !DEBUG
            config.Filters.Add(new AuthorizeAttribute());
#endif
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void EnableCrossSiteRequests(HttpConfiguration config)
        {
#if DEBUG
            var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
#else
            var cors = new EnableCorsAttribute("http://attendance1.azurewebsites.net", headers: "*", methods: "*");
#endif
            config.EnableCors(cors);
        }

        private static void SetErrorPolicy()
        {

            var config = (CustomErrorsSection)
            ConfigurationManager.GetSection("system.web/customErrors");

            IncludeErrorDetailPolicy errorDetailPolicy;

            switch (config.Mode)
            {
                case CustomErrorsMode.RemoteOnly:
                    errorDetailPolicy
                    = IncludeErrorDetailPolicy.LocalOnly;
                    break;
                case CustomErrorsMode.On:
                    errorDetailPolicy
                    = IncludeErrorDetailPolicy.Never;
                    break;
                case CustomErrorsMode.Off:
                    errorDetailPolicy
                    = IncludeErrorDetailPolicy.Always;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = errorDetailPolicy;
        }

        private static void ConfigureDependencyResolver(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IEmailService, EmailService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);            
        }
    }
}
