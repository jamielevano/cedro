using DatabaseContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class FotoController : Controller
    {
        //
        // GET: /Asistencia/Foto/

        public ActionResult Index(int id)
        {  
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Preview(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                var _item = db.Persona.SingleOrDefault(x => x.id == id);

                if (_item.foto != null)
                {

                    return File(_item.foto, "image/jpeg");
                }
            }

            return new HttpNotFoundResult();
        }


        [HttpPost]
        public ActionResult SendImage(int id, HttpPostedFileBase img)
        {
            double size = ConvertBytesToMegabytes(img.ContentLength);

            if (size <= 1.0)
            {

                var data = new byte[img.ContentLength];
                img.InputStream.Read(data, 0, img.ContentLength);



                using (SMECEntities db = new SMECEntities())
                {

                    var _item = db.Persona.SingleOrDefault(x => x.id == id);
                    _item.foto = data;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", new { id = id });
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
