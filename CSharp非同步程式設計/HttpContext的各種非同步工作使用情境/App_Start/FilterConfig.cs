using System.Web;
using System.Web.Mvc;

namespace HttpContext的各種非同步工作使用情境
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
