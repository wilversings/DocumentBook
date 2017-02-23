using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLayer.Abstract {
    public interface IPostingProvider {

        File CreateFile (HttpPostedFileBase file, bool sync = false);
        File CreateFile (string fileName, string mimeType, byte[] content, bool sync = false);
        Post CreatePost (string authorId, string body, File attachment = null, bool sync = false);

    }
}
