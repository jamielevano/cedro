using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.BibliotecaVirtual.Controllers
{
    public class DocumentosController : Controller
    {
        //
        // GET: /BibliotecaVirtual/Documentos/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id, string descripcion, HttpPostedFileBase[] files)
        {
            using (var db = new SMECEntities())
            {
                foreach (var file in files)
                {
                    db.DocumentoArchivo.Add(new DocumentoArchivo
                    {
                        documentoid = id,
                        fecha = DateTime.Now,
                        nombre = file.FileName,
                        tipo = file.ContentType,
                        archivo = file.ToByteArray(),
                        //descripcion=item.descripcion ,
                        //descripcion = DocumentoArchivo.descripicion,
                        descripcion= descripcion,
                        aud_usuarioreg = User.Identity.Name
                });
                }

                db.SaveChanges();
            }

            return Redirect(Request.Url.ToString());
        }

        public FileResult Descargar(int id)
        {
            using (var db = new SMECEntities())
            {
                var file = db.DocumentoArchivo.Find(id);
                return File(file.archivo, file.tipo, file.nombre);
            }
        }

    }
}
