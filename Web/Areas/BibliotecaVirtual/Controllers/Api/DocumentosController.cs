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
    public class DocumentosController : ApiController
    {
        [HttpGet]
        public object Listar(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.Documento
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.nombre,
                        archivos = x.DocumentoArchivo.Select(y => new
                        {
                            y.id,
                            y.nombre,
                            //y.tipo,
                            tamanio = SqlFunctions.DataLength(y.archivo),
                            y.fecha,
                            y.descripcion,
                            y.aud_usuarioreg

                        })
                    })
                    .SingleOrDefault();
            }
        }

        [HttpPost]
        public void Eliminar(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.DocumentoArchivo.Delete(item.id);
                db.SaveChanges();
            }
        }

    }
}
