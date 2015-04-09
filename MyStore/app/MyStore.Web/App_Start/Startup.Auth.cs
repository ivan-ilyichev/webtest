using System;
using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using MyStore.Domain;
using MyStore.Domain.Security;
using MyStore.Web.Providers;
using Owin;

namespace MyStore.Web
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () =>
            {
                return new UserManager<NhIdentityUser>(new NhUserStore<NhIdentityUser>());
            };

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),

                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<NhIdentityUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication();

            /* Ivan's account test
            app.UseFacebookAuthentication(
                appId: "842300665832308",
                appSecret: "0510d35e25faeeba3b9531cb0cc78fd4");
            */
            
            //  Wozawow Ivan test app
            app.UseFacebookAuthentication(
                appId: "721133778006415",
                appSecret: "5ee329e65cf2225f51cd3bdfb4a7cec3");
            
            // Wozawow
            
            /*
            app.UseFacebookAuthentication(
                appId: "720737971379329",
                appSecret: "2bb2e66905f9f909fd7f974c273fd10f");
            
            */
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "372895005306-r6slka4gc31fq6vf446qs4q9t591t32g.apps.googleusercontent.com",
                ClientSecret = "PtRJgRLGiDCAmodJlUGNmf7a",
                CallbackPath = new PathString("/signin-google")
                //CallbackPath = new PathString("/Account/ExternalLoginCallback")
            });
        }
    }
}
