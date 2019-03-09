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
    public class ModuloArchivoController : ApiController
    {  
        

        [HttpGet]
        public object ModuloListArchivos(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.ModuloArchivo
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                            x.id,
                            x.nombre,
                            //y.tipo,
                            tamanio = SqlFunctions.DataLength(x.archivo),
                            x.fecha
                    })
                    .SingleOrDefault();
            }
        }
        
        [HttpPost]
        public void ModuloDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.ModuloArchivo.Delete(item.id);
                db.SaveChanges();
            }
        }

        
        
    }
}
