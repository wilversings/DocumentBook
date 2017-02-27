using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLayer.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentBook.Controllers;
using Model.ViewModels;
using System.Web.Mvc;
using System.Web;

namespace Test {
    [TestClass]
    public class AccountControllerTests {

        [TestMethod]
        public void LoginTest () {

            var mock = new Mock<IAuthProvider> ();
            mock.Setup (m => m.AuthenticateAsync (It.IsAny<string> (), It.IsAny<string> ())).Returns<string, string> ((x, y) => {

                if (x == "sample_username" && y == "sample_password") {
                    return Task.FromResult (true);
                }
                if (x == "abcd" && y == "1234") {
                    return Task.FromResult (true);
                }
                return Task.FromResult(false);

            });
            var ctrl = new AccountController (mock.Object);

            var vm = new LoginViewModel {
                Email = "sample_username",
                Password = "sample_password"
            };
            var actionResult = ctrl.Login (vm, "sample").Result;

            Assert.AreEqual (actionResult.GetType (), typeof(RedirectToRouteResult));
            Assert.AreEqual ((actionResult as RedirectToRouteResult).RouteName, "sample");

            vm = new LoginViewModel {
                Email = "abcd",
                Password = "1234"
            };
            actionResult = ctrl.Login (vm, "sample15").Result;

            Assert.AreEqual (actionResult.GetType (), typeof (RedirectToRouteResult));
            Assert.AreEqual ((actionResult as RedirectToRouteResult).RouteName, "sample15");

            vm = new LoginViewModel {
                Email = "abcde",
                Password = "1234"
            };
            actionResult = ctrl.Login (vm, "2sample2").Result;

            Assert.AreEqual (actionResult.GetType (), typeof (ViewResult));
            Assert.AreEqual ((actionResult as ViewResult).Model, vm);

        }

        [TestMethod]
        public void RegisterTest() {
            var mock = new Mock<IAuthProvider> ();
            mock.Setup (m => m.RegisterAsync (It.IsAny<string> (), It.IsAny<string> (), null)).Returns<string, string> ((x, y) => {

                if (x == "sampleuser@sampledomain.com") 
                    return Task.FromResult(new string[] { "Duplicate User" } as IEnumerable<string>);
                if (x == "wrong_email")
                    return Task.FromResult (new string[] { "Email not valid" } as IEnumerable<string>);
                if (y == "wkpass") 
                    return Task.FromResult (new string[] { "Password too weak" } as IEnumerable<string>);
                return Task.FromResult(new string[] { } as IEnumerable<string>);

            });
            var ctrl = new AccountController (mock.Object);
            ctrl.ViewData.ModelState.Clear ();
            var vm = new RegisterViewModel {
                Email = "myusername",
                Password = "mypassword",
                ProfilePicture = new HttpPostedFileWrapper[1] { null }
            };
            ctrl.ModelState.AddModelError ("email", "Invalid email!");
            var result = ctrl.Register (vm);
            Assert.AreEqual (result.GetType(), typeof (ViewResult));

            // ModelState is valid. This is not correct because `myusername` is not a email address
            // So model validation is not done
            Assert.AreEqual ((result as ViewResult).Model, vm);

            ctrl = new AccountController (mock.Object);
            vm = new RegisterViewModel {
                Email = "sampleuser@sampledomain.com",
                Password = "secret",
                ProfilePicture = new HttpPostedFileWrapper[1] { null }
            };
            result = ctrl.Register (vm);

            Assert.AreEqual (result.GetType (), typeof (ViewResult));
            Assert.AreEqual ((result as ViewResult).Model, null);
            // Can't set Controller.ModelState.IsValid because the setter is private
        }

    }
}
