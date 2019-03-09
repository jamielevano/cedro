using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Controllers.Api;
using Web.Models;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class ResultadoController : ApiController
    {   
        [HttpGet]
        public IEnumerable<ListItem> Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Resultado
                    .Where(x => x.MarcoLogico.proyectoid == id)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }
    }
}
