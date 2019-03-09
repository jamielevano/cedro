using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Asistencia.Controllers
{
    public class ConfiguracionController : Controller
    {
        //
        // GET: /Asistencia/Configuracion/
        public ActionResult Edit() {

            using (var db = new SMECEntities())
            {
                //var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;
                //var eje = db.ListaDetalle.Where(x => x.listaid == 54 && x.codigo == telecentro).FirstOrDefault().relacionid;
                var usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().id;

                //ViewBag.telecentroid = telecentro;
                //ViewBag.ejeid = eje;
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

        public ActionResult Index() 
        {
            ViewBag.id = Session.GetDataFromSession("Configuracion_id");
            ViewBag.programaid = Session.GetDataFromSession("Configuracion_programaid");
            ViewBag.nivelid = Session.GetDataFromSession("Configuracion_nivelid");
            ViewBag.moduloid = Session.GetDataFromSession("Configuracion_moduloid");
            ViewBag.anioid = Session.GetDataFromSession("Configuracion_anioid");
            ViewBag.mesid = Session.GetDataFromSession("Configuracion_mesid");
            ViewBag.telecentroid = Session.GetDataFromSession("Configuracion_telecentroid");
            ViewBag.ejeid = Session.GetDataFromSession("Configuracion_ejeintervencionid");
            ViewBag.organizacion = Session.GetDataFromSession("Configuracion_organizacion");
            //solo filtra los telecentros asignado al usuario
            //if (ViewBag.telecentroid == null)
            //{
            //    using (var db = new SMECEntities())
            //    {
            //        ViewBag.telecentroid = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro.ToString();
            //    }
            //}

            return View();
        }

    }
}
