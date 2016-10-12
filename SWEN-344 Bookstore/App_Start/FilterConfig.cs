using System.Web;
using System.Web.Mvc;

namespace SWEN_344_Bookstore {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
