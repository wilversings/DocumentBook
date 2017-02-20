using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using DataAccessLayer.Abstract;
using Microsoft.AspNet.Identity.EntityFramework;
using Model.Models;

namespace DataAccessLayer.Concrete {
    public class AppDbContext : IdentityDbContext<AppUser>, IDbContext {

        public AppDbContext () : base("DefaultSchema") { }
        static AppDbContext() {
            Database.SetInitializer<AppDbContext> (new IdentityDbInit ());
        } 
        public static AppDbContext Create() {
            return new AppDbContext ();
        }

        public DbSet<Post> Posts { get; }
        public DbSet<Document> Documents { get; set; }


    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppDbContext> {
        protected override void Seed (AppDbContext context) {
            PerformInitialSetup (context);
            base.Seed (context);
        }

        public void PerformInitialSetup(AppDbContext context) {

        }
    }

}
