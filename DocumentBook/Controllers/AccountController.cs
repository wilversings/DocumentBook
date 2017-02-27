using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using Model.Models;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Reflection;

namespace DocumentBook.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthProvider AuthProvider;

        public AccountController (IAuthProvider authProvider) {
            this.AuthProvider = authProvider;
        }


        public ActionResult Index() {
            return RedirectToAction ("Register");
        }

        public ActionResult Register()
        {
            if(Request.IsAuthenticated) {
                return Redirect ("/");
            }
            return View ();
        }

        [HttpPost]
        public ActionResult Register (RegisterViewModel model) {
            if (!ModelState.IsValid) {
                return View (model);
            }
            var errors = AuthProvider.Register (model.Email, model.Password, (model.ProfilePicture as HttpPostedFileBase[])[0]);
            foreach(string err in errors) {
                ModelState.AddModelError ("", err);
            }
            
            return View ();
        }

        public ActionResult Login(string redirectUrl) {
            if (Request.IsAuthenticated) {
                return Redirect ("/");
            }
            ViewBag.RedirectUrl = redirectUrl;
            return View ();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel, string redirectUrl) {
            bool success = await AuthProvider.AuthenticateAsync (viewModel.Email, viewModel.Password);
            if (success) {
                return RedirectToRoute (redirectUrl);
            }
            return View (viewModel);
        }

        [HttpPost, Authorize]
        public ActionResult Logout () {
            AuthProvider.Logout ();
            return Redirect ("/");
        }

    }
}