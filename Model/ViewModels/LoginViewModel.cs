using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels {
    public class LoginViewModel {

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
