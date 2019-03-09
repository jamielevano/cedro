using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.BibliotecaVirtual.Controllers
{
    public class PlanOperativoArchivoController : Controller
    {
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Avance()
        {
            return View();
        }

        public ActionResult AvanceActividad()
        {
            return View();
        }

        
        public ActionResult MetaArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MetaArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.PlanOperativoMetaArchivo.Add(new PlanOperativoMetaArchivo
                    {
                        pometaid = id,
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

        public FileResult MetaDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.PlanOperativoMetaArchivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }


        public ActionResult AvanceActividadArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AvanceActividadArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.AvancePO_Archivo.Add(new AvancePO_Archivo
                    {
                        avanceid = id,
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

        public FileResult AvanceActividadDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.AvancePO_Archivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }

    }
}
