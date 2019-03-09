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
    public class ResultadoTCController : ApiController
    {
        [HttpGet]
        public IEnumerable<ListItem> List()
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.ResultadoTC
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo +"-"+ x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> List(int marcoid)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.ResultadoTC
                    .Where(x => x.marcoid == marcoid)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo + "-" + x.nombre
                    })
                    .ToList();
            }
        }
    }
}
