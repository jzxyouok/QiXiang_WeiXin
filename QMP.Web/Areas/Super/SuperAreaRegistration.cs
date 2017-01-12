using System.Web.Mvc;

namespace QMP.Web.Areas.Super
{
    public class SuperAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Super";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Super_default",
                "Super/{controller}/{action}/{id}",
              new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "QMP.Web.Areas.Super.Controllers" }

            );
        }
    }
}