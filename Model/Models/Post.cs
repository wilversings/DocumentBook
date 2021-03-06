﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models {
    public class Post {

        [Key]
        public int Id { get; set; }
        public virtual AppUser Author { get; set; }
        public string Body { get; set; }
        public virtual File Attachment { get; set; }
        public DateTime CreateTimestamp { get; set; }

    }
}
