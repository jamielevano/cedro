using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Areas.BibliotecaVirtual.Controllers
{
    public class MarcoLogicoArchivoTCController : Controller
    {

        public ActionResult Search()
        {
            using (var db = new SMECEntities())
            {
                //ViewBag.ListTipoEncuesta = db.Encuesta.Select(x => new ListItem
                //{
                //    id = x.id,
                //    nombre = x.nombre
                //}).ToList();

                ViewBag.ListUsuario = db.Usuario.Select(x => x.login).OrderBy(x => x).ToList();
            }

            return View();
        }


        public ActionResult Edit()
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
                    db.PropositoMetaArchivoTC.Add(new PropositoMetaArchivoTC
                    {
                        propositometaid = id,
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
                var file = db.PropositoMetaArchivoTC.Find(id);
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
                    db.ResultadoMetaArchivoTC.Add(new ResultadoMetaArchivoTC
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
                var file = db.ResultadoMetaArchivoTC.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }



        public ActionResult EncuestaArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EncuestaArchivo(int id,HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.AvanceMLTC_Archivo.Add(new AvanceMLTC_Archivo
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

        public FileResult EncuestaDownload(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.AvanceMLTC_Archivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }

    }
}
