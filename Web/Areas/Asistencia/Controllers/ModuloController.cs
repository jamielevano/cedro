using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class ModuloController : Controller
    {
        //
        // GET: /Asistencia/Modulo/

        //
        // GET: /Asistencia/Configuracion/
        public ActionResult Edit(){ return View();}
        public ActionResult Index() { return View(); }

    }
}
