using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract {
    public interface IDbContext {

        DbSet<Document> Documents { get; set; }
        int SaveChanges ();
        Task<int> SaveChangesAsync ();

    }
}
