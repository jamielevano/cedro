using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Sistema_DefaultApi",
                routeTemplate: "api/Sistema/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Planificacion_DefaultApi",
                routeTemplate: "api/Planificacion/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Monitoreo_DefaultApi",
                routeTemplate: "api/Monitoreo/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Evaluacion_DefaultApi",
                routeTemplate: "api/Evaluacion/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "BibliotecaVirtual_DefaultApi",
                routeTemplate: "api/BibliotecaVirtual/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Reporte_DefaultApi",
                routeTemplate: "api/Reporte/{controller}/{action}"
            );

            config.Routes.MapHttpRoute(
                name: "Asistencia_DefaultApi",
                routeTemplate: "api/Asistencia/{controller}/{action}"
            );
            
        }
    }
}
