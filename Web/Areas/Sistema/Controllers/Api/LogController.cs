using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Web.Areas.Sistema.Models;

namespace Web.Areas.Sistema.Controllers.Api
{
    public class LogController : ApiController
    {
        public IEnumerable<object> Search(LogSearchModel data)
        {
            using (var db = new SMECEntities())
            {
                return db.Error
                    .Where(x
                        => (!data.fechainicio.HasValue || x.fecha >= data.fechainicio.Value)
                        && (!data.fechafin.HasValue || x.fecha <= data.fechafin.Value)
                        )
                    .Select(x => new
                    {
                        x.id,
                        x.fecha,
                        x.login,
                        x.mensaje
                    })
                    .OrderBy(y => y.fecha)
                    .ToList();
            }
        }
    }
}
