using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using Microsoft.AspNet.Identity;
using DataAccessLayer.Abstract;
using Model.ViewModels;
using System.Data.Entity;
using Model.Models;

namespace Mvc.Controllers {
    public class HomeController : Controller {

        IDbContext DbContext { get; set; }
        public HomeController (IDbContext dbContext) {
            DbContext = dbContext;
        }

        public ActionResult Index () {
            string uid = User.Identity.GetUserId ();
            return View (new HomeViewModel {
                CurrentUser = DbContext.Users.FirstOrDefault (u => u.Id == uid),
                Posts = (DbContext.Posts as DbSet<Post>).Include("Author")
            });
        }

        public ActionResult ProfilePicture(string username) {
            var user = DbContext.Users.FirstOrDefault (u => u.UserName == username);
            if (user == null) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.NotFound);
            }
            if (user.ProfilePicture.Length != 0)
                return File (user.ProfilePicture, "image");
            return null;
        }

        [HttpDelete]
        public ActionResult DeletePost(int id) {
            return View ();
        }

        [HttpPost, Authorize]
        public ActionResult Post(HomeViewModel model) {
            
            if (!ModelState.IsValid) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.BadRequest);
            }
            var username = User.Identity.Name;
            model.Post.Author = DbContext.Users.FirstOrDefault (u => u.UserName == username);
            if (model.Post.Author == null) {
                return new HttpStatusCodeResult (System.Net.HttpStatusCode.BadRequest);
            }
            model.Post.CreateTimestamp = DateTime.Now;

            var att = (model.Attachment as HttpPostedFileBase[])[0];
            if (att != null) {
                var contentBuffer = new byte[att.ContentLength];
                att.InputStream.Read (contentBuffer, 0, att.ContentLength);
                var newFile = new File {
                    MimeType = att.ContentType,
                    FileName = att.FileName,
                    Content = contentBuffer
                };
                model.Post.Attachment = newFile;
                DbContext.Files.Add (newFile);
            }
            else {
                model.Post.Attachment = null;
            }

            DbContext.Posts.Add (model.Post);
            DbContext.SaveChanges ();
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