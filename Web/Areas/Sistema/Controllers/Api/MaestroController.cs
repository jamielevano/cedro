using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Web.Areas.Sistema.Controllers.Api
{
    public class MaestroController : ApiController
    {
        [HttpGet]
        public List<Lista> List()
        {
            using (var db = new SMECEntities())
            {
                return db.Lista
                    .AsNoTracking()
                    .Where(x => x.aud_anulado == false)
                    .ToList();
            }
        }


        [HttpGet]
        public Lista Find(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.Lista
                    .AsNoTracking()
                    .Include(x => x.ListaDetalle)
                    .FirstOrDefault(x => x.id == id);
            }
        }


        public void Save(Lista item)
        {
            using (var db = new SMECEntities())
            {
                item.aud_usuariomod = User.Identity.Name;
                item.aud_ipmod = Request.GetClientIp();
                item.aud_fechamod = DateTime.Now.GetAudFormat();

                if (item.id == 0)
                {
                    throw new InvalidOperationException();
                    //item.aud_usuarioreg = item.aud_usuariomod;
                    //item.aud_ipreg = item.aud_ipmod;
                    //item.aud_fechareg = item.aud_fechamod;

                    //foreach (var listadetalle in item.ListaDetalle)
                    //{
                    //    listadetalle.aud_usuarioreg = listadetalle.aud_usuariomod = item.aud_usuariomod;
                    //    listadetalle.aud_ipreg = listadetalle.aud_ipmod = item.aud_ipmod;
                    //    listadetalle.aud_fechareg = listadetalle.aud_fechamod = item.aud_fechamod;
                    //}

                    //db.Lista.Add(item);
                }
                else
                {
                    var _item = db.Lista
                        .Include(x => x.ListaDetalle)
                        .SingleOrDefault(x => x.id == item.id);


                    var deletedListaDetalle  = item.ListaDetalle .Where(x => x.id != 0).Select(x => x.id).ToList();

                    _item.ListaDetalle .Where(x => !deletedListaDetalle .Contains(x.id)).ToList().ForEach(pm =>
                    {
                        db.ListaDetalle .Remove(pm);
                        _item.ListaDetalle .Remove(pm);
                    });

                    foreach (var pm in _item.ListaDetalle )
                    {
                        var listadetalle = item.ListaDetalle .First(x => pm.id == x.id);
                        pm.nombre = listadetalle.nombre;
                        pm.codigo = listadetalle.codigo;
                        pm.activo = listadetalle.activo;

                        pm.headid = listadetalle.headid;
                        pm.relacionid = listadetalle.relacionid;

                        pm.aud_usuariomod = item.aud_usuariomod;
                        pm.aud_ipmod = item.aud_ipmod;
                        pm.aud_fechamod = item.aud_fechamod;
                    }

                    item.ListaDetalle .Where(x => x.id == 0).ToList().ForEach(pm =>
                    {
                        pm.aud_usuarioreg = pm.aud_usuariomod = item.aud_usuariomod;
                        pm.aud_ipreg = pm.aud_ipmod = item.aud_ipmod;
                        pm.aud_fechareg = pm.aud_fechamod = item.aud_fechamod;
                        _item.ListaDetalle .Add(pm);
                    });



                    _item.nombre = item.nombre;
                    //_item.modificable = item.modificable;

                    _item.aud_usuariomod = item.aud_usuariomod;
                    _item.aud_ipmod = item.aud_ipmod;
                    _item.aud_fechamod = item.aud_fechamod;
                }

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                db.Lista.Remove(db.Lista.Find(id));
                db.SaveChanges();
            }
        }
    }
        
}
