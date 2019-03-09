using System.Web.Mvc;

namespace Web.Areas.Evaluacion
{
    public class EvaluacionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get 
            {
                return "Evaluacion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Evaluacion_default",
                "Evaluacion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
