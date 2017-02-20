using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models {
    public class Post {

        public int Id { get; set; }
        public AppUser Author { get; set; }
        public string Body { get; set; }
        public bool HasAttachment { get; set; }
        public byte[] Attachment { get; set; }

    }
}
