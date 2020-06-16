using System.Web;
using System.Web.Mvc;

namespace T1809E_PROJECT_SEM3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
