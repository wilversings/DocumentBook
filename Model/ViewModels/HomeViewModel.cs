using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels {
    public class HomeViewModel {

        public IEnumerable<Post> Posts { get; set; }
        public AppUser CurrentUser { get; set; }
        public Post Post { get; set; }

    }
}
