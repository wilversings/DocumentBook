using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract {
    public interface IAuthProvider {

        Task<bool> AuthenticateAsync (string username, string password);
        IEnumerable<string> Register (string username, string password, byte[] profilePicture);
        Task<IEnumerable<string>> RegisterAsync (string username, string password, byte[] profilePicture);
        byte[] ProfilePicture (string username);
        void Logout ();

    }
}
