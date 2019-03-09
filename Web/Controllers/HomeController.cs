using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using DatabaseContext;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var menu = new Repositorio.Acceso().GetAcceso(User.Identity.Name);

#if DEBUG            
            if (Request.QueryString.ToString() == "m")
                return View("_menu", menu);
#endif

            return View(menu);
        }

        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string password)
        {
            bool admin;
            bool coordinador;
            int ejeid;
            int telecentroid;

            if (new Repositorio.Acceso().Login(user, password, out admin, out coordinador, out ejeid, out telecentroid))
            {
                Auth.SetAuthCookie(user, new AuthTicketData
                {
                    Admin = admin,
                    Coordinador= coordinador,
                    EjeId= ejeid,
                    TelecentroId= telecentroid
                });
                return RedirectToAction("Index");
            }

            return View(model: "El login y/o clave ingresado no es válido");
        }

        public ActionResult Password()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Password(string old, string @new)
        {
            using (var db = new SMECEntities())
            {
                var item = db.Usuario.Single(x => x.login == User.Identity.Name);
                if (item.clave == old)
                {
                    item.clave = @new;
                    //item.aud_fechamod = DateTime.Now.GetAudFormat();
                    //item.aud_ipmod = Request.UserHostAddress;
                    db.SaveChanges();

                    return View("Password2");
                }
                else
                {
                    ViewBag.Error = "La contraseña antigua es incorrecta.";
                    return View();
                }
            }
        }
    }
}
