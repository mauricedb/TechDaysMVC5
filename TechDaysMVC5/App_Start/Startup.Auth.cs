﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;

namespace TechDaysMVC5
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
           
            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            var facebookAuthenticationOptions = new FacebookAuthenticationOptions
            {
                AppId = "1480656632147712",
                AppSecret = "971e8d74e63d9a6a5e676bebd5c134ff"
            };
            facebookAuthenticationOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookAuthenticationOptions);

            app.UseGoogleAuthentication();
        }
    }
}