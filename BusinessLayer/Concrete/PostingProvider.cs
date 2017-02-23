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
    class PostingProvider : IPostingProvider {

        public IDbContext DbContext { get; protected set; }
        public PostingProvider(IDbContext dbContext) {
            this.DbContext = dbContext;
        }

        public File CreateFile (HttpPostedFileBase file) {
            byte[] content = new byte[file.ContentLength];
            file.InputStream.Read (content, 0, file.ContentLength);
            return this.CreateFile (file.FileName, file.ContentType, content);
        }

        public File CreateFile (string fileName, string mimeType, byte[] content) {
            var newFile = new File {
                Content = content,
                FileName = fileName,
                MimeType = mimeType
            };

            DbContext.Files.Add (newFile);
            return newFile;
        }

        public Post CreatePost (string authorId, string body, File attachment = null) {
            var newPost = new Post {
                CreateTimestamp = DateTime.Now,
                Author = DbContext.Users.FirstOrDefault (u => u.Id == authorId),
                Body = body,
                Attachment = attachment
            };

            DbContext.Posts.Add (newPost);
            return newPost;
        }
    }
}
