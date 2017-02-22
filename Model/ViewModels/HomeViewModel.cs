using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.ViewModels {
    public class HomeViewModel {

        public IEnumerable<Post> Posts { get; set; }
        public AppUser CurrentUser { get; set; }
        public virtual Post Post { get; set; }
        public object Attachment { get; set; }

    }
}
