using System.Web;
using System.Web.Mvc;

namespace DocumentBook {
    public class FilterConfig {
        public static void RegisterGlobalFilters (GlobalFilterCollection filters) {
            filters.Add (new HandleErrorAttribute ());
        }
    }
}
