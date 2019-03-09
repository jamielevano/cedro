using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class EquipoController : Controller
    {
        //
        // GET: /Asistencia/Equipo/

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Add()
        {
            using (var db = new SMECEntities())
            {
                var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;
                
                ViewBag.telecentroid = telecentro;
                
            }
            return View();
        }

        public ActionResult Index()
        {
            
            return View();
        }

    }
}
