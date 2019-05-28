using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class ReporteIFMController : Controller
    {
        //
        // GET: /Asistencia/ReporteGeneral/

        public ActionResult Index()
        {
            using (var db = new SMECEntities())
            {
                ViewBag.ListUsuario = db.Usuario
                                            .Where(x => x.nombre != null)
                                            .Select(x => new
                                            {
                                                x.id,
                                                x.nombre

                                            }).OrderBy(x => x.nombre).ToList();

                var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;
                var eje = db.ListaDetalle.Where(x => x.listaid == 54 && x.codigo == telecentro).FirstOrDefault().relacionid;
                var usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().id;

                ViewBag.usuarioid = usuario;
                ViewBag.telecentroid = telecentro;
                ViewBag.ejeid = eje;
            }

            return View();
        }

    }
}
