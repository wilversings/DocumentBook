using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract {
    interface IPostingProvider {

        File CreateFile (string fileName, string mimeType, byte[] content);

        Post CreatePost (string authorId, string body, File attachment = null);

    }
}
