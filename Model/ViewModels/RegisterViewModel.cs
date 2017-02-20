using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Model.ViewModels {
    public class RegisterViewModel {

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        
    }
}
