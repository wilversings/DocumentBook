using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentBook.Filters {
    public class FileTooBigExceptionAttribute : FilterAttribute, IExceptionFilter {
        public void OnException (ExceptionContext filterContext) {

            Console.WriteLine (filterContext.Exception.GetType ());

        }
    }
}