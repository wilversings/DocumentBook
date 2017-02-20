using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;
using Model.Models;

namespace DataAccessLayer.Concrete {
    class PostRepository : Abstract.AbstractPostRepository {

        IDbContext DbContext { get; set; }
        public PostRepository(IDbContext dbContext) {
            DbContext = dbContext;
        }

        public override IEnumerable<Post> All () {
            return DbContext.Posts.AsEnumerable ();
        }

        public override void Create (Post item) {
            DbContext.Posts.Add (item);
        }

        public override void Destroy (Post item) {
            DbContext.Posts.Remove (item);
        }

        public override void Update (Post from, Post to) {
            throw new NotImplementedException ();
        }

        public override int Sync () {
            return DbContext.SaveChanges ();
        }

        public override async Task<int> SyncAsync () {
            return await DbContext.SaveChangesAsync ();
        }
    }
}
