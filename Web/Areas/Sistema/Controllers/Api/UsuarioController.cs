using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Areas.Sistemas.Models;
using Web.Models;

namespace Web.Areas.Sistema.Controllers.Api
{
    public class UsuarioController : ApiController
    {
        public IEnumerable<object> List(UsuarioSearchModel data)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Usuario
                    .Where(x
                        => (!data.rol.HasValue || x.rolid == data.rol.Value)
                        && (!data.telecentro.HasValue || x.telecentro == data.telecentro)
                        && (string.IsNullOrEmpty(data.cargo) || x.cargo == data.cargo)
                        && (string.IsNullOrEmpty(data.login) || x.login == data.login)
                        )
                    .Select(x => new
                    {
                        x.id,
                        x.login,
                        x.clave,
                        x.nombre,
                        x.cargo,
                        rol=x.Rol.descripcion,
                        telecentro=db.ListaDetalle.FirstOrDefault(y=>y.listaid==51 && y.codigo==x.telecentro).nombre,
                        x.admin,
                        x.coordinador
                    })
                    .OrderBy(o=>o.login)
                    .ToList();
            }
        }

        [HttpGet]
        public Usuario Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Usuario.AsNoTracking()
                    .SingleOrDefault(x => x.id == id);
            }
        }

        public void Save(Usuario item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                item.aud_usuariomod = User.Identity.Name;
                item.aud_ipmod = Request.GetClientIp();
                item.aud_fechamod = DateTime.Now.GetAudFormat();


                if (item.id == 0)
                {
                    item.aud_usuarioreg = item.aud_usuariomod;
                    item.aud_ipreg = item.aud_ipmod;
                    item.aud_fechareg = item.aud_fechamod;

                    db.Usuario.Add(item);
                }
                else
                {
                    var _item = db.Usuario.SingleOrDefault(x => x.id == item.id);

                    _item.login = item.login;
                    _item.clave = item.clave;
                    _item.nombre = item.nombre;
                    _item.cargo = item.cargo;
                    _item.telecentro = item.telecentro;
                    _item.admin = item.admin;
                    _item.rolid = item.rolid;
                    _item.coordinador = item.coordinador;

                    _item.aud_usuariomod = item.aud_usuariomod;
                    _item.aud_ipmod = item.aud_ipmod;
                    _item.aud_fechamod = item.aud_fechamod;
                }

                db.SaveChanges();
            }
        }

        [HttpPost]
        public void Delete(DeleteModel item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                db.Usuario.Remove(db.Usuario.Find(item.id));
                db.SaveChanges();
            }
        }
    }
}
