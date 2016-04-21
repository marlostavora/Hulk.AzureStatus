using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Hulk.WebAPI.App_Start;

[assembly: OwinStartup(typeof(Hulk.WebAPI.Startup))]

namespace Hulk.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
