using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.BibliotecaVirtual.Controllers
{
    public class ModuloArchivoController : Controller
    {

        
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult ModuloArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModuloArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.ModuloArchivo.Add(new ModuloArchivo
                    {
                        documentoid = id,
                        fecha = DateTime.Now,
                        nombre = file.FileName,
                        tipo = file.ContentType,
                        archivo = file.ToByteArray()
                    });
                }

                db.SaveChanges();
            }

            return Redirect(Request.Url.ToString());
        }

        public FileResult ModuloDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.ModuloArchivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }




    }
}
