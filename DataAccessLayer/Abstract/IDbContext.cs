﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract {
    public interface IDbContext {

        int SaveChanges ();
        Task<int> SaveChangesAsync ();

        IDbSet<Post> Posts { get; }
        IDbSet<AppUser> Users { get; }
        IDbSet<File> Files { get; }

    }
}
