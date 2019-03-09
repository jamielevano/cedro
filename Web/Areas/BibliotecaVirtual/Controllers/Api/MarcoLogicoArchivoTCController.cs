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
    public class MarcoLogicoArchivoTCController : ApiController
    {  
        [HttpGet]
        public object PropositoListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PropositoMetasTC
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.nombre,
                        x.indicador,
                        archivos = x.PropositoMetaArchivoTC.Select(y => new
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
        public void PropositoDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.PropositoMetaArchivoTC.Delete(item.id);
                db.SaveChanges();
            }
        }

        [HttpGet]
        public object ResultadoListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.ResultadoMetasTC
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.indicador,
                        x.nombre,
                        resultado = x.ResultadoTC.codigo + "-" + x.ResultadoTC.nombre,
                        archivos = x.ResultadoMetaArchivoTC.Select(y => new
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
        public void ResultadoDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.ResultadoMetaArchivoTC.Delete(item.id);
                db.SaveChanges();
            }
        }

        
        [HttpGet]
        public IEnumerable<object> EncuestaListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.AvanceMLTC_Archivo
                    .Where(x => x.avanceid == id)
                    .Select(y => new
                    {

                        y.id,
                        y.nombre,
                        //y.tipo,
                        tamanio = SqlFunctions.DataLength(y.archivo),
                        y.fecha
                    })
                    .ToList();
            }
        }

        [HttpPost]
        public void EncuestaDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.AvanceMLTC_Archivo.Delete(item.id);
                db.SaveChanges();
            }
        }
    }
}
