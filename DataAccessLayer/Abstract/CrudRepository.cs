using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract {
    public abstract class CrudRepository<T> {

        public delegate bool Predicate (T current);
        public abstract void Create (T item);
        public abstract void Destroy (T item);
        public abstract void Update (T from, T to);
        public abstract IEnumerable<T> All ();

        public abstract int Sync ();
        public abstract Task<int> SyncAsync ();
    }
}
