using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models {
    public class Document {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
        public string FileExtension { get; set; }
        public string MimeType { get; set; }
        public bool Private { get; set; }
    }
}
