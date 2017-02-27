using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Mocks {
    class FakeAuthProvider : IAuthProvider {
        public async Task<bool> AuthenticateAsync (string username, string password) {

            if (username == "sample_user" && password == "sample_password") {
                return true;
            }
            if (username == "abcd" && password == "1234") {
                return true;
            }
            return false;

        }

        public void Logout () {
            
        }

        public byte[] ProfilePicture (string username) {
            throw new NotImplementedException ();
        }

        public IEnumerable<string> Register (string username, string password, System.Web.HttpPostedFileBase profilePicture) {
            throw new NotImplementedException ();
        }

        public IEnumerable<string> Register (string username, string password, byte[] profilePicture) {
            throw new NotImplementedException ();
        }

        public Task<IEnumerable<string>> RegisterAsync (string username, string password, byte[] profilePicture) {
            throw new NotImplementedException ();
        }
    }
}
