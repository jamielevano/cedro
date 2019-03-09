using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.BibliotecaVirtual.Controllers
{
    public class MarcoLogicoArchivoController : Controller
    {
        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Avance()
        {
            return View();
        }

        public ActionResult AvancePropositoMeta()
        {
            return View();
        }


        public ActionResult AvanceResultadoMeta()
        {
            return View();
        }

        public ActionResult PropositoArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PropositoArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.PropositoMetaArchivo.Add(new PropositoMetaArchivo
                    {
                        metaid = id,
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

        public FileResult PropositoDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.PropositoMetaArchivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }


        public ActionResult ResultadoArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResultadoArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.ResultadoMetaArchivo.Add(new ResultadoMetaArchivo
                    {
                        resultadometaid = id,
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

        public FileResult ResultadoDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.ResultadoMetaArchivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }


        public ActionResult AvancePropositoMetaArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AvancePropositoMetaArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.AvancePropositoMetas_Archivo.Add(new AvancePropositoMetas_Archivo
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

        public FileResult AvancePropositoMetaDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.AvancePropositoMetas_Archivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }



        public ActionResult AvanceResultadoMetaArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AvanceResultadoMetaArchivo(int id, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.AvanceResultadoMetas_Archivo.Add(new AvanceResultadoMetas_Archivo
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

        public FileResult AvanceResultadoMetaDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.AvanceResultadoMetas_Archivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }
    }
}
