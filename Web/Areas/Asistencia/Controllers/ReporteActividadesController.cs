using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class ReporteActividadesController : Controller
    {
        // GET: Asistencia/ReporteActividades
        public ActionResult Index()
        {
            var db = new SMECEntities();
            ViewBag.ListUsuario = db.Usuario
                                        .Where(x => x.nombre != null)
                                        .Select(x => new
                                        {
                                            x.id,
                                            x.nombre

                                        }).OrderBy(x => x.nombre).ToList();

            var usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().id;

            ViewBag.usuarioid = usuario;

            return View();
        }
    }
}