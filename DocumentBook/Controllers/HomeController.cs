using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using Microsoft.AspNet.Identity;
using Model.ViewModels;
using System.Data.Entity;
using Model.Models;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;

namespace Mvc.Controllers {
    public class HomeController : Controller {

        readonly IPostingProvider PostingProvider;
        readonly IDbContext DbContext;
        readonly IAuthProvider AuthProvider;

        public HomeController (IPostingProvider postingProvider, IDbContext dbContext, IAuthProvider authProvider) {
            PostingProvider = postingProvider;
            DbContext = dbContext;
            AuthProvider = authProvider;
        }

        public ActionResult Index () {
            string uid = User.Identity.GetUserId ();
            return View (new HomeViewModel {
                CurrentUser = DbContext.Users.FirstOrDefault (u => u.Id == uid),
                Posts = DbContext.Posts
            });
        }

        public ActionResult ProfilePicture(string username) {
            var pic = AuthProvider.ProfilePicture (username);
            if (pic == null) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.NotFound);
            }
            return File (pic, "image");
        }

        [HttpDelete, Authorize]
        public ActionResult DeletePost(int id) {

            var post = DbContext.Posts.FirstOrDefault (p => p.Id == id);
            if (post == null) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.NotFound);
            }
            if (User.Identity.GetUserId() != post.Author.Id) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.Forbidden);
            }
            if (post.Attachment != null) {
                DbContext.Files.Remove (post.Attachment);
            }
            var deletedPost = DbContext.Posts.Remove (post);
            DbContext.SaveChanges ();
            
            return Redirect ("/");

        }

        [HttpPost, Authorize, DocumentBook.Filters.FileTooBigException]
        public ActionResult Post(HomeViewModel model) {
            
            if (!ModelState.IsValid) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.BadRequest);
            }
            PostingProvider.CreatePost (
                authorId:   User.Identity.GetUserId (),
                body:       model.Post.Body,
                attachment: PostingProvider.CreateFile ((model.Attachment as HttpPostedFileBase[])[0]),
                sync:       true
            );
            return Redirect ("/");

        }

        public ActionResult Download(int id) {
            var att = DbContext.Posts.FirstOrDefault (p => p.Id == id).Attachment;
            return File (att.Content, att.MimeType, att.FileName);
        }
        public ActionResult Preview(int id) {
            var att = DbContext.Posts.FirstOrDefault (p => p.Id == id).Attachment;
            return new FileContentResult(att.Content, att.MimeType);
        }

    }
}