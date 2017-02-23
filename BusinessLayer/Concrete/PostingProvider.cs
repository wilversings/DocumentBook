using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using DataAccessLayer.Abstract;
using System.Web;

namespace BusinessLayer.Concrete {
    public class PostingProvider : IPostingProvider {

        public IDbContext DbContext { get; protected set; }
        public PostingProvider(IDbContext dbContext) {
            this.DbContext = dbContext;
        }

        public File CreateFile (HttpPostedFileBase file, bool sync = false) {
            byte[] content = new byte[file.ContentLength];
            file.InputStream.Read (content, 0, file.ContentLength);
            return this.CreateFile (file.FileName, file.ContentType, content, sync);
        }

        public File CreateFile (string fileName, string mimeType, byte[] content, bool sync = false) {
            var newFile = new File {
                Content = content,
                FileName = fileName,
                MimeType = mimeType
            };

            DbContext.Files.Add (newFile);
            if (sync) {
                DbContext.SaveChanges ();
            }
            return newFile;
        }

        public Post CreatePost (string authorId, string body, File attachment = null, bool sync = false) {
            var newPost = new Post {
                CreateTimestamp = DateTime.Now,
                Author = DbContext.Users.FirstOrDefault (u => u.Id == authorId),
                Body = body,
                Attachment = attachment
            };

            DbContext.Posts.Add (newPost);
            if (sync) {
                DbContext.SaveChanges ();
            }
            return newPost;
        }
    }
}
