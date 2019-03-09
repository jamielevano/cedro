using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Monitoreo.Controllers
{
    public class TareaTCController : Controller
    {
        public ActionResult Mover() { return View(); }

        public ActionResult Index() { return View(); }

        public ActionResult Edit() {


            using (var db = new SMECEntities())
            {
                //var telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro;//cesar
                //var eje = db.ListaDetalle.Where(x => x.listaid == 54 && x.codigo == telecentro).FirstOrDefault().relacionid;//cesar
                var usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().id;

                //ViewBag.telecentroid = telecentro; ///cesar
                //ViewBag.telecentroinvitadoid = telecentro;//cesar
                //ViewBag.ejeid = eje;//ceasr
                ViewBag.usuarioid = usuario;

                ViewBag.ListUsuario = db.Usuario
                                        .Where(x => x.nombre != null)
                                        .Select(x => new
                                        {
                                            x.id,
                                            x.nombre
                                        }).OrderBy(x => x.nombre).ToList();
            }
            
            ViewData["archivoid"] = new Random().Next(1, 2147483);
            return View(); 
        }

        public ActionResult Search()
        {
            ViewBag.id = Session.GetDataFromSession("Tarea_id");
            ViewBag.fechainicio = Session.GetDataFromSession("Tarea_fechainicio");
            ViewBag.fechafin = Session.GetDataFromSession("Tarea_fechafin");
            ViewBag.tarea = Session.GetDataFromSession("Tarea_tarea");
            ViewBag.usuario = Session.GetDataFromSession("Tarea_usuario");
            ViewBag.telecentroid = Session.GetDataFromSession("Tarea_telecentroid");
            ViewBag.ejeintervencionid = Session.GetDataFromSession("Tarea_ejeintervencionid");

            //if (ViewBag.telecentroid == null)
            //{
            //    using (var db = new SMECEntities())
            //    {
            //        ViewBag.telecentroid = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro.ToString();
            //    }
            //}

            using (var db = new SMECEntities())
            {
                ViewBag.ListUsuario = db.Usuario.Select(x => x.login).OrderBy(x => x).ToList();
            }

            return View();
        }
    }
}
