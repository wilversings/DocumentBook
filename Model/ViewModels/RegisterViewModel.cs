using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;

namespace Model.ViewModels {
    public class RegisterViewModel {

        [EmailAddress, Required]
        public string Email { get; set; }
        [MinLength(8), Required]
        public string Password { get; set; }
        public object ProfilePicture { get; set; }

        
    }
}
