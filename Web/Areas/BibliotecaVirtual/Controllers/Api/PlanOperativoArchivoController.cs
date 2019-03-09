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
    public class PlanOperativoArchivoController : ApiController
    {
        [HttpGet]
        public object MetaListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoMeta
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        archivos = x.PlanOperativoMetaArchivo.Select(y => new
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
        public void MetaDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.PlanOperativoMetaArchivo.Delete(item.id);
                db.SaveChanges();
            }
        }



        [HttpGet]
        public object AvanceActividadListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.AvancePO
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        archivos = x.AvancePO_Archivo.Select(y => new
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
        public void AvanceActividadDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.AvancePO_Archivo.Delete(item.id);
                db.SaveChanges();
            }
        }
    }
}
