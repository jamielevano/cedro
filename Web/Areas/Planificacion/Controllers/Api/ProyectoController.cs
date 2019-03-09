using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class ProyectoController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List()
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Proyecto
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        x.nombre,
                        periodoinicio = (DateTime?)x.MarcoLogico.FirstOrDefault().periodoinicio,
                        periodofin = (DateTime?)x.MarcoLogico.FirstOrDefault().periodofin,
                        planoperativo = x.MarcoLogico.FirstOrDefault().Resultado.Any(),
                        eliminable = !(x.habilitarmarcologico ?? false) && !(x.habilitarplanoperativo ?? false)
                    })
                    .OrderBy(x => x.periodoinicio ?? DateTime.MaxValue)
                    .ThenBy(x => x.codigo)
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<object> Search(string codigo = null, string nombre = null, DateTime? desde = null, DateTime? hasta = null)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Proyecto
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        x.nombre,
                        periodoinicio = (DateTime?)x.MarcoLogico.FirstOrDefault().periodoinicio,
                        periodofin = (DateTime?)x.MarcoLogico.FirstOrDefault().periodofin,
                        planoperativo = x.MarcoLogico.FirstOrDefault().Resultado.Any(),
                        eliminable = !(x.habilitarmarcologico ?? false) && !(x.habilitarplanoperativo ?? false)
                    })
                    .Where(x
                        => (string.IsNullOrEmpty(codigo) || x.codigo == codigo)
                        && (string.IsNullOrEmpty(nombre) || x.nombre.Contains(nombre))
                        && (!desde.HasValue || !x.periodoinicio.HasValue || x.periodoinicio >= desde.Value)
                        && (!hasta.HasValue || !x.periodoinicio.HasValue || x.periodoinicio <= hasta.Value)
                        )
                    .OrderBy(x => x.periodoinicio ?? DateTime.MaxValue)
                    .ThenBy(x => x.codigo)
                    .ToList();
            }
        }



        [HttpGet]
        public Proyecto Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                var proyecto = db.Proyecto.AsNoTracking()
                    .Include("MarcoLogico")
                    .SingleOrDefault(x => x.id == id);

                return proyecto;
            }
        }

        public void Save(Proyecto item)
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

                    db.Proyecto.Add(item);
                }
                else
                {
                    var _item = db.Proyecto.SingleOrDefault(x => x.id == item.id);

                    _item.codigo = item.codigo;
                    _item.nombre = item.nombre;
                    _item.aud_usuariomod = item.aud_usuariomod;
                    _item.aud_ipmod = item.aud_ipmod;
                    _item.aud_fechamod = item.aud_fechamod;
                }

                db.SaveChanges();
            }
        }

        public void Habilitar(Proyecto item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                item.aud_usuariomod = User.Identity.Name;
                item.aud_ipmod = Request.GetClientIp();
                item.aud_fechamod = DateTime.Now.GetAudFormat();


                var _item = db.Proyecto.SingleOrDefault(x => x.id == item.id);
                _item.habilitarmarcologico = item.habilitarmarcologico;
                _item.habilitarplanoperativo = item.habilitarplanoperativo;
                _item.aud_usuariomod = item.aud_usuariomod;
                _item.aud_ipmod = item.aud_ipmod;
                _item.aud_fechamod = item.aud_fechamod;

                db.SaveChanges();
            }
        }

        [HttpPost]
        public void Delete(DeleteModel model)
        {            
            using (SMECEntities db = new SMECEntities())
            {
                db.Proyecto.Delete(model.id);
                db.SaveChanges();
            }
           
        }
    }
}
