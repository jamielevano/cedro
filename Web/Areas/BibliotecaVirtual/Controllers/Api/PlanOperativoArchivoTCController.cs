using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.BibliotecaVirtual.Controllers.Api
{
    public class PlanOperativoArchivoTCController : ApiController
    {
        [HttpGet]
        public object TareaListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTareaTC
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.indicador,
                        tarea = x.codigo + "-" + x.tarea,
                        actividad = x.PlanOperativoTC.codigo + "-" + x.PlanOperativoTC.actividad,
                        resultado = x.PlanOperativoTC.ResultadoTC.codigo + "-" + x.PlanOperativoTC.ResultadoTC.nombre,
                        archivos = x.PlanOperativoTareaArchivoTC.Select(y => new
                        {
                            y.id,
                            y.nombre,
                            //y.tipo,
                            tamanio = SqlFunctions.DataLength(y.archivo),
                            y.fecha
                        })
                    })
                    .SingleOrDefault();
            }
        }

        [HttpPost]
        public void TareaDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.PlanOperativoTareaArchivoTC.Delete(item.id);
                db.SaveChanges();
            }
        }





        [HttpGet]
        public IEnumerable<object> FichaListArchivos(int id, int ficha)
        {
            using (var db = new SMECEntities())
            {
                return db.AvancePOTC_Archivo
                    .Where(x => x.avanceid == id && x.ficha == ficha)
                    .Select(y => new
                    {

                        y.id,
                        y.ficha,
                        y.nombre,
                        //y.tipo,
                        tamanio = SqlFunctions.DataLength(y.archivo),
                        y.fecha
                    })
                    .ToList();
            }
        }

        [HttpPost]
        public void FichaDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.AvancePOTC_Archivo.Delete(item.id);
                db.SaveChanges();
            }
        }
    }
}
