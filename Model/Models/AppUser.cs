﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models {
    public class AppUser : IdentityUser {

        public byte[] ProfilePicture { get; set; }

    }
}
