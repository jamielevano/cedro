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
    public class MarcoLogicoArchivoController : ApiController
    {
        [HttpGet]
        public object PropositoListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PropositoMetas
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.nombre,
                        x.indicador,
                        archivos = x.PropositoMetaArchivo.Select(y => new
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
                db.PropositoMetaArchivo.Delete(item.id);
                db.SaveChanges();
            }
        }


        [HttpGet]
        public object ResultadoListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.ResultadoMetas
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.indicador,
                        x.nombre,
                        archivos = x.ResultadoMetaArchivo.Select(y => new
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
                db.ResultadoMetaArchivo.Delete(item.id);
                db.SaveChanges();
            }
        }


        [HttpGet]
        public object AvancePropositoMetaListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.AvancePropositoMetas
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        archivos = x.AvancePropositoMetas_Archivo.Select(y => new
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
        public void AvancePropositoMetaDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.AvancePropositoMetas_Archivo.Delete(item.id);
                db.SaveChanges();
            }
        }


        [HttpGet]
        public object AvanceResultadoMetaListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.AvanceResultadoMetas
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        archivos = x.AvanceResultadoMetas_Archivo.Select(y => new
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
        public void AvanceResultadoMetaDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.AvanceResultadoMetas_Archivo.Delete(item.id);
                db.SaveChanges();
            }
        }
    }
}
