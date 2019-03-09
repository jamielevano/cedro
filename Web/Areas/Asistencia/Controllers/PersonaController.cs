using DatabaseContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class PersonaController : Controller
    {
        //
        // GET: /Asistencia/Persona/


        public ActionResult Edit()
        {   
            using (var db = new SMECEntities())
            {
                //var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;
                //var eje = db.ListaDetalle.Where(x => x.listaid == 54 && x.codigo == telecentro).FirstOrDefault().relacionid;

                //ViewBag.telecentroid = telecentro;
                //ViewBag.ejeid = eje;

                var usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().login;
                ViewBag.usuarioid = usuario;

                ViewBag.ListUsuario = db.Usuario
                                        .Where(x => x.nombre != null)
                                        .Select(x => new
                                        {
                                            x.login ,
                                            x.nombre
                                        }).OrderBy(x => x.nombre).ToList();
            }

            return View();
        }

        public ActionResult Index()
        {
            
            //var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;

            ViewBag.dni = Session.GetDataFromSession("Persona_dni");
            ViewBag.apellidopaterno = Session.GetDataFromSession("Persona_apellidopaterno");
            ViewBag.apellidomaterno = Session.GetDataFromSession("Persona_apellidomaterno");
            ViewBag.nombre = Session.GetDataFromSession("Persona_nombre");
            ViewBag.codigo = Session.GetDataFromSession("Persona_codigo");
            ViewBag.telecentroid = Session.GetDataFromSession("Persona_telecentroid");

            if (ViewBag.telecentroid == null)
            {
                using (var db = new SMECEntities())
                {
                    ViewBag.telecentroid = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;
                }
            }

            return View();
        }

        

    }
}
