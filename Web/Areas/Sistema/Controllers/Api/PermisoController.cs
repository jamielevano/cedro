using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Areas.Planificacion.Models;
using Web.Models;

namespace Web.Areas.Sistema.Controllers.Api
{
    public class PermisoController : ApiController
    {
        [HttpGet]
        public object Find(int id)
        {
            bool Admin = new Repositorio.Acceso().IsAdmin(HttpContext.Current.User.Identity.Name);

            if (Admin)
            { 
            
                return new { 
                        acceder=true,
                        agregar=true,
                        modificar=true,
                        eliminar=true
                    };
            }

            using (var db = new SMECEntities())
            {
                var res = new Repositorio.Acceso().Permisos(HttpContext.Current.User.Identity.Name, id);
                return res;
            }
        }
    }
}
