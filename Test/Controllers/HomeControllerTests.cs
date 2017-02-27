using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using DataAccessLayer.Abstract;
using Model.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;
using BusinessLayer.Abstract;
using System.Web;

namespace Mvc.Controllers.Tests {
    [TestClass ()]
    public class HomeControllerTests {
        [TestMethod ()]
        public void IndexTest () {
            
        }

        public static Mock<IPostingProvider> MockPostingProvider() {
            var mock = new Mock<IPostingProvider> ();
            return mock;
        }

        public static Mock<IAuthProvider> MockAuthProvider() {
            var mock = new Mock<IAuthProvider> ();

            mock.Setup (x => x.ProfilePicture (It.IsAny<string> ())).Returns<string> (uname => {
                if (uname == "sampleuser@sampledomain.com") {
                    return Encoding.ASCII.GetBytes ("match_text");
                }
                if (uname == "123@345.com") {
                    return Encoding.ASCII.GetBytes ("not_important");
                }
                return null;
            });

            return mock;
        }



        public static Mock<IDbContext> MockDbContext () {
            var mock = new Mock<IDbContext> ();
            mock.SetupGet (x => x.Users).Returns (new FakeDbSet<AppUser> {
                new AppUser {
                    UserName = "sampleuser@sampledomain.com",
                    ProfilePicture = Encoding.ASCII.GetBytes("match_text")
                },
                new AppUser {
                    UserName = "123@345.com"
                }
            });
            return mock;
        }

        [TestMethod ()]
        public void ProfilePictureTest () {

            var ctrl = new HomeController (
                MockPostingProvider ().Object,
                MockDbContext ().Object,
                MockAuthProvider ().Object
            );
            var result = ctrl.ProfilePicture ("sampleuser@sampledomain.com");

            Assert.AreEqual (result.GetType (), typeof (FileContentResult));
            Assert.AreEqual (Encoding.ASCII.GetString((result as FileContentResult).FileContents), "match_text");
            Assert.AreEqual ((result as FileContentResult).ContentType, "image");

            result = ctrl.ProfilePicture ("123@345.com");
            Assert.AreEqual(result.GetType(), typeof(FileContentResult));

            result = ctrl.ProfilePicture ("nonexistent@user.com");

            Assert.AreEqual (result.GetType (), typeof (HttpStatusCodeResult));
            Assert.AreEqual ((result as HttpStatusCodeResult).StatusCode, 404);

        }



    }
}