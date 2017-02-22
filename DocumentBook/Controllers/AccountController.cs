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

namespace DocumentBook.Controllers
{
    public class AccountController : Controller
    {

        private IAuthProvider AuthProvider { get; set; }

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
        public async Task<ActionResult> Register (RegisterViewModel model) {
            if (!ModelState.IsValid) {
                return View (model);
            }

            byte[] profilePictureBuffer;
            if (model.ProfilePicture != null) {
                var File = (model.ProfilePicture as HttpPostedFileBase[])[0];
                profilePictureBuffer = new byte[File.ContentLength];
                File.InputStream.Read (profilePictureBuffer, 0, File.ContentLength);
            } else {
                profilePictureBuffer = new byte[0];
            }

            // TODO validate File.ContentType

            var errors = await AuthProvider.RegisterAsync (model.Email, model.Password, profilePictureBuffer);
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

        [HttpPost]
        public ActionResult Logout () {
            AuthProvider.Logout ();
            return Redirect ("/");
        }

    }
}