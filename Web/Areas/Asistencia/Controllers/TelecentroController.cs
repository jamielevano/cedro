using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class TelecentroController : Controller
    {
        //
        // GET: /Asistencia/Telecentro/

        public ActionResult Edit()
        {
            using (var db = new SMECEntities())
            {
                var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;
                var eje = db.ListaDetalle.Where(x => x.listaid == 54 && x.codigo == telecentro).FirstOrDefault().relacionid;
                var usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().id;

                ViewBag.telecentroid = telecentro;
                ViewBag.ejeid = eje;
                ViewBag.usuarioid = usuario;

                ViewBag.ListUsuario = db.Usuario
                                        .Where(x => x.nombre != null)
                                        .Select(x => new
                                        {
                                            x.id,
                                            x.nombre
                                        }).OrderBy(x => x.nombre).ToList();
            }
            return View();
        }
        public ActionResult Index() {

            ViewBag.telecentroid = Session.GetDataFromSession("Acceso_telecentroid");
            ViewBag.anioid = Session.GetDataFromSession("Acceso_anioid");
            ViewBag.mesid = Session.GetDataFromSession("Acceso_mesid");

            if (ViewBag.telecentroid == null)
            {
                using (var db = new SMECEntities())
                {
                    ViewBag.telecentroid = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro.ToString();
                }
            }

            return View(); 
        }

    }
}
