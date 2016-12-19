using System.Web.Mvc;

namespace QMP.Web.Areas.WeiXin
{
    public class WeiXinAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WeiXin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WeiXin_default",
                "WeiXin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                  new string[] { "QMP.Web.Areas.WeiXin.Controllers" }
            );
        }
    }
}