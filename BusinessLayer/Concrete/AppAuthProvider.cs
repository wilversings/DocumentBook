using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using DataAccessLayer.Concrete;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using DataAccessLayer.Abstract;

namespace BusinessLayer.Concrete {
    public class AppAuthProvider : IAuthProvider {

        readonly IDbContext DbContext;
        public AppAuthProvider (IDbContext dbContext) {
            DbContext = dbContext;
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext ().GetUserManager<AppUserManager> ();
            }
        }
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext ().Authentication;
            }
        }

        public async Task<bool> AuthenticateAsync (string username, string password) {
            var user = await UserManager.FindAsync (username, password);
            if (user == null) {
                return false;
            }
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync (user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthManager.SignOut ();
            AuthManager.SignIn (new AuthenticationProperties {
                IsPersistent = false
            }, ident);
            return true;
        }

        public IEnumerable<string> Register (string username, string password, byte[] profilePicture) {
            var identityResult = UserManager.CreateAsync (new Model.Models.AppUser {
                UserName = username,
                ProfilePicture = profilePicture,
                CreateTimestamp = DateTime.Now
            }, password).Result;
            return identityResult.Errors;
        }

        public async Task<IEnumerable<string>> RegisterAsync (string username, string password, byte[] profilePicture) {
            return (await UserManager.CreateAsync (new Model.Models.AppUser {
                UserName = username,
                ProfilePicture = profilePicture,
                CreateTimestamp = DateTime.Now
            }, password)).Errors;
        }

        public byte[] ProfilePicture (string username) {
            var user = DbContext.Users.FirstOrDefault (u => u.UserName == username);
            if (user == null)
                return null;
            if (user.ProfilePicture != null) {
                return user.ProfilePicture;
            }
            // TODO return default profile picture
            return null;
        }

        public void Logout () {
            AuthManager.SignOut ();
        }
    }
}
