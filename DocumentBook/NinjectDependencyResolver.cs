using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;

namespace DocumentBook {
    public class NinjectDependencyResolver : IDependencyResolver {

        public IKernel Kernel { get; set; }
        public NinjectDependencyResolver(IKernel kernel) {
            this.Kernel = kernel;
            this.SetBindings ();
        }

        public void SetBindings() {

            // Business layer bindings
            this.Kernel.Bind<IAuthProvider> ().To<AppAuthProvider> ();
            this.Kernel.Bind<IPostingProvider> ().To<PostingProvider> ();

            // Data access layer bindings
            this.Kernel.Bind<IDbContext> ().To<AppDbContext> ();
        }

        public object GetService (Type serviceType) {
            throw new NotImplementedException ();
        }

        public IEnumerable<object> GetServices (Type serviceType) {
            throw new NotImplementedException ();
        }
    }

}