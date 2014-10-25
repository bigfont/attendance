using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Attendance.DataAccess.DAL;
using Attendance.WebApi.Providers;


namespace Attendance.WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {            
            app.CreatePerOwinContext<AttendanceContext>(Attendance.DataAccess.DAL.AttendanceContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // [Do we really want to use a cookie? Maybe change this later.]
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            System.Diagnostics.Debugger.Break();

            // Configure the application for OAuth based flow
            PublicClientId = "self"; 
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                ///  $body = @{ grant_type = "password"; username = "bigfont@outlook.com"; password = ""  }
                /// Invoke-RestMethod http://localhost/Attendance.WebApi/token -Method POST -ContentType "application/x-www-form-urlencoded" -Body $body
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true // TODO Change this to HTTPS
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}