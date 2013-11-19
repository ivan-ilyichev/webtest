using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using MyStore.Web;
using MyStore.Web.App_Start;
using Owin;

//[assembly: OwinStartup(typeof(Startup))]

namespace MyStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
