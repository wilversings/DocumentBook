using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete {
    public class AppUserManager : UserManager<AppUser> {

        public AppUserManager(IUserStore<AppUser> store) : base(store) { }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context) {
            var db = context.Get<AppDbContext> ();
            var manager = new AppUserManager (new UserStore<AppUser> (db));
            return manager;
        }

    }
}
