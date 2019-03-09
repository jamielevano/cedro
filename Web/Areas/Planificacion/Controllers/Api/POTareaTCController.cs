using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using Web.Models;


namespace Web.Areas.Planificacion.Controllers.Api
{
    public class POTareaTCController : ApiController
    {

        [HttpGet]
        public IEnumerable<ListItem> FindByActividad(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTareaTC
                    .Where(x => x.poid == id)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo + "-" + x.tarea
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public object Find(int id)
        {
            using (var db = new SMECEntities())
            {

                return db.PlanOperativoTareaTC.Where(x => x.id == id)
                    .Select(x => new
                    {
                        id = x.id,
                        x.ficha1,
                        x.ficha2,
                        x.ficha3,
                        x.ficha4,
                        x.ficha6,
                        periodoinicio = x.PlanOperativoTC.periodoinicio,
                        periodofin = x.PlanOperativoTC.periodofin,
                        tarea = x.codigo + "-" + x.tarea,
                        actividad = x.PlanOperativoTC.codigo + "-" + x.PlanOperativoTC.actividad,
                        resultado = x.PlanOperativoTC.ResultadoTC.codigo + "-" + x.PlanOperativoTC.ResultadoTC.nombre
                    })
                    .FirstOrDefault();

            }
        }
    }
}
