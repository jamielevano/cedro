using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.BibliotecaVirtual.Controllers
{
    public class PlanOperativoArchivoTCController : Controller
    {
        public ActionResult Search()
        {
            using (var db = new SMECEntities())
            {
                ViewBag.ListUsuario = db.Usuario.Select(x => x.login).OrderBy(x => x).ToList();
            }
            return View();
        }
      
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TareaArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TareaArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.PlanOperativoTareaArchivoTC.Add(new PlanOperativoTareaArchivoTC
                    {
                        potareaid = id,
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

        public FileResult TareaDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.PlanOperativoTareaArchivoTC.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }




        public ActionResult FichaArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FichaArchivo(int id,int ficha, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.AvancePOTC_Archivo.Add(new AvancePOTC_Archivo
                    {
                        avanceid = id,
                        ficha = ficha,
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

        public FileResult FichaDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.AvancePOTC_Archivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }

       
    }
}
