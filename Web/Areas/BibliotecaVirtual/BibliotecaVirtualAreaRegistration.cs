using System.Web.Mvc;

namespace Web.Areas.BibliotecaVirtual
{
    public class BibliotecaVirtualAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BibliotecaVirtual";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BibliotecaVirtual_default",
                "BibliotecaVirtual/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
