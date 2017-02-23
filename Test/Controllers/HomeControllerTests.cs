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

namespace Mvc.Controllers.Tests {
    [TestClass ()]
    public class HomeControllerTests {
        [TestMethod ()]
        public void IndexTest () {
            
        }

        [TestMethod ()]
        public void ProfilePictureTest () {

            /*var mock = new Mock<IDbContext> ();
            mock.SetupGet (x => x.Users).Returns (new FakeDbSet<AppUser> {
                new AppUser {
                    UserName = "sampleuser@sampledomain.com",
                    ProfilePicture = Encoding.ASCII.GetBytes("match_text")
                },
                new AppUser {
                    UserName = "123@345.com"
                }
            });

            var ctrl = new HomeController (mock.Object);
            var result = ctrl.ProfilePicture ("sampleuser@sampledomain.com");

            Assert.AreEqual (result.GetType (), typeof (FileContentResult));
            Assert.AreEqual (Encoding.ASCII.GetString((result as FileContentResult).FileContents), "match_text");
            Assert.AreEqual ((result as FileContentResult).ContentType, "image");

            result = ctrl.ProfilePicture ("123@345.com");
            Assert.AreEqual(result.GetType(), typeof(FileContentResult));

            result = ctrl.ProfilePicture ("nonexistent@user.com");

            Assert.AreEqual (result.GetType (), typeof (HttpStatusCodeResult));
            Assert.AreEqual ((result as HttpStatusCodeResult).StatusCode, HttpStatusCode.NotFound);
            */
        }

        [TestMethod ()]
        public void PostTest () {

        }
    }
}