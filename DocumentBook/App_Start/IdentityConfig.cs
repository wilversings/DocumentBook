using Microsoft.AspNet.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete {
    class IdentityConfig {
        public void Configuration(IAppBuilder app) {

            app.CreatePerOwinContext<AppDbContext> (AppDbContext.Create);
            app.CreatePerOwinContext<AppUserManager> (AppUserManager.Create);

            app.UseCookieAuthentication (new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString ("/Account/Login")
            });

        }

    }
}
