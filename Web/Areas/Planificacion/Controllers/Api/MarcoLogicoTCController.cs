using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class MarcoLogicoTCController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List()
        {
            
            using (var db = new SMECEntities())
            {
                return db.MarcoLogicoTC
                    .AsNoTracking()                   
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        x.periodoinicio,
                        x.periodofin,
                        x.fin,
                        x.proposito,
                        x.supuesto,
                        x.nombre
                    })
                    .OrderByDescending(d => d.periodoinicio)
                    .ToList();
            }
        }

        [HttpGet]
        public object Find(int id)
        {
           
            using (var db = new SMECEntities())
            {
                return db.MarcoLogicoTC
                    .AsNoTracking()
                    .Include(x => x.PropositoMetasTC)
                    .Include(x => x.ResultadoTC.Select(y => y.ResultadoMetasTC))
                    .Where(x=>x.id==id)
                    .Select(x => new
                    {
                        x.id,
                        x.fin,
                        x.nombre,
                        x.periodoinicio,
                        x.periodofin,
                        x.codigo,
                        x.proposito,
                        x.supuesto,
                        PropositoMetasTC = x.PropositoMetasTC.Select(m => new
                        {
                            m.id,
                            m.codigo,
                            m.nombre,
                            m.cantidad,
                            m.unidadid,
                            m.indicador,
                            m.fuente,
                            archivos=m.PropositoMetaArchivoTC.Any()
                        }).OrderBy(o=>o.codigo),
                        ResultadoTC = x.ResultadoTC.Select(r => new
                        {
                            r.id,
                            r.codigo,
                            r.nombre,
                            r.supuesto,
                            ResultadoMetasTC = r.ResultadoMetasTC.Select(m => new
                            {
                                m.id,
                                m.codigo,
                                m.nombre,
                                m.cantidad,
                                m.unidadid,
                                m.indicador,
                                m.fuente,
                                archivos = m.ResultadoMetaArchivoTC.Any()
                            }).OrderBy(o=>o.codigo),
                            eliminable = true//!r.PlanOperativoTC.Any()
                        })
                    })
                    .FirstOrDefault();
            }
        }

        [HttpGet]
        public object GetFecha()
        {

            using (var db = new SMECEntities())
            {
                return db.MarcoLogicoTC
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.id,
                        x.periodoinicio,
                        x.periodofin,
                    })
                    .FirstOrDefault();
            }
        }

        public void Save(MarcoLogicoTC item)
        {
            using (var db = new SMECEntities())
            {
                item.aud_usuariomod = User.Identity.Name;
                item.aud_ipmod = Request.GetClientIp();
                item.aud_fechamod = DateTime.Now.GetAudFormat();

                if (item.id == 0)
                {
                    item.aud_usuarioreg = item.aud_usuariomod;
                    item.aud_ipreg = item.aud_ipmod;
                    item.aud_fechareg = item.aud_fechamod;

                    foreach (var propositoMetas in item.PropositoMetasTC)
                    {
                        propositoMetas.aud_usuarioreg = propositoMetas.aud_usuariomod = item.aud_usuariomod;
                        propositoMetas.aud_ipreg = propositoMetas.aud_ipmod = item.aud_ipmod;
                        propositoMetas.aud_fechareg = propositoMetas.aud_fechamod = item.aud_fechamod;
                    }

                    foreach (var resultadoTC in item.ResultadoTC)
                    {
                        resultadoTC.aud_usuarioreg = resultadoTC.aud_usuariomod = item.aud_usuariomod;
                        resultadoTC.aud_ipreg = resultadoTC.aud_ipmod = item.aud_ipmod;
                        resultadoTC.aud_fechareg = resultadoTC.aud_fechamod = item.aud_fechamod;

                        foreach (var resultadoMetasTC in resultadoTC.ResultadoMetasTC)
                        {
                            resultadoMetasTC.aud_usuarioreg = resultadoMetasTC.aud_usuariomod = item.aud_usuariomod;
                            resultadoMetasTC.aud_ipreg = resultadoMetasTC.aud_ipmod = item.aud_ipmod;
                            resultadoMetasTC.aud_fechareg = resultadoMetasTC.aud_fechamod = item.aud_fechamod;
                        }
                    }

                    db.MarcoLogicoTC.Add(item);
                }
                else
                {
                    var _item = db.MarcoLogicoTC
                        .Include(x => x.PropositoMetasTC)
                        .Include(x => x.ResultadoTC.Select(y => y.ResultadoMetasTC))
                        .SingleOrDefault(x => x.id == item.id);

                    // ----------------------

                    var deletedPropositoMetasTC = item.PropositoMetasTC.Where(x => x.id != 0).Select(x => x.id).ToList();
                    _item.PropositoMetasTC.Where(x => !deletedPropositoMetasTC.Contains(x.id)).ToList().ForEach(pm =>
                    {
                        db.PropositoMetasTC.Remove(pm);
                        _item.PropositoMetasTC.Remove(pm);
                    });

                    foreach (var pm in _item.PropositoMetasTC)
                    {
                        var propositoMetasTC = item.PropositoMetasTC.First(x => pm.id == x.id);
                        pm.codigo = propositoMetasTC.codigo;
                        pm.nombre = propositoMetasTC.nombre;
                        pm.cantidad = propositoMetasTC.cantidad;
                        pm.unidadid = propositoMetasTC.unidadid;
                        pm.indicador = propositoMetasTC.indicador;
                        pm.fuente = propositoMetasTC.fuente;
                        pm.aud_usuariomod = item.aud_usuariomod;
                        pm.aud_ipmod = item.aud_ipmod;
                        pm.aud_fechamod = item.aud_fechamod;
                    }

                    item.PropositoMetasTC.Where(x => x.id == 0).ToList().ForEach(pm =>
                    {
                        pm.aud_usuarioreg = pm.aud_usuariomod = item.aud_usuariomod;
                        pm.aud_ipreg = pm.aud_ipmod = item.aud_ipmod;
                        pm.aud_fechareg = pm.aud_fechamod = item.aud_fechamod;
                        _item.PropositoMetasTC.Add(pm);
                    });

                    // ----------------------

                    var deletedResultadoTC = item.ResultadoTC.Where(x => x.id != 0).Select(x => x.id).ToList();
                    _item.ResultadoTC.Where(x => !deletedResultadoTC.Contains(x.id)).ToList().ForEach(r =>
                    {
                        r.ResultadoMetasTC.ToList().ForEach(y => db.ResultadoMetasTC.Remove(y));
                        db.ResultadoTC.Remove(r);
                        _item.ResultadoTC.Remove(r);
                    });

                    foreach (var _r in _item.ResultadoTC)
                    {
                        var r = item.ResultadoTC.First(x => _r.id == x.id);
                        _r.codigo = r.codigo;
                        _r.nombre = r.nombre;
                        _r.supuesto = r.supuesto;
                        _r.aud_usuariomod = item.aud_usuariomod;
                        _r.aud_ipmod = item.aud_ipmod;
                        _r.aud_fechamod = item.aud_fechamod;

                        // ----------------------

                        var deletedResultadoMetasTC = r.ResultadoMetasTC.Where(x => x.id != 0).Select(x => x.id).ToList();
                        _r.ResultadoMetasTC.Where(x => !deletedResultadoMetasTC.Contains(x.id)).ToList().ForEach(rm =>
                        {
                            db.ResultadoMetasTC.Remove(rm);
                            _r.ResultadoMetasTC.Remove(rm);
                        });

                        foreach (var _rm in _r.ResultadoMetasTC)
                        {
                            var rm = r.ResultadoMetasTC.First(x => _rm.id == x.id);
                            _rm.codigo = rm.codigo;
                            _rm.nombre = rm.nombre;
                            _rm.cantidad = rm.cantidad;
                            _rm.unidadid = rm.unidadid;
                            _rm.indicador = rm.indicador;
                            _rm.fuente = rm.fuente;
                            _rm.aud_usuariomod = item.aud_usuariomod;
                            _rm.aud_ipmod = item.aud_ipmod;
                            _rm.aud_fechamod = item.aud_fechamod;
                        }

                        r.ResultadoMetasTC.Where(x => x.id == 0).ToList().ForEach(rm =>
                        {
                            rm.aud_usuarioreg = rm.aud_usuariomod = item.aud_usuariomod;
                            rm.aud_ipreg = rm.aud_ipmod = item.aud_ipmod;
                            rm.aud_fechareg = rm.aud_fechamod = item.aud_fechamod;
                            _r.ResultadoMetasTC.Add(rm);
                        });

                        // ----------------------
                    }

                    item.ResultadoTC.Where(x => x.id == 0).ToList().ForEach(r =>
                    {
                        r.aud_usuarioreg = r.aud_usuariomod = item.aud_usuariomod;
                        r.aud_ipreg = r.aud_ipmod = item.aud_ipmod;
                        r.aud_fechareg = r.aud_fechamod = item.aud_fechamod;

                        r.ResultadoMetasTC.ToList().ForEach(rm =>
                        {
                            rm.aud_usuarioreg = rm.aud_usuariomod = item.aud_usuariomod;
                            rm.aud_ipreg = rm.aud_ipmod = item.aud_ipmod;
                            rm.aud_fechareg = rm.aud_fechamod = item.aud_fechamod;
                        });

                        _item.ResultadoTC.Add(r);
                    });

                    // ----------------------

                    _item.fin = item.fin;
                    _item.periodoinicio = item.periodoinicio;
                    _item.periodofin = item.periodofin;
                    _item.codigo = item.codigo;
                    _item.proposito = item.proposito;
                    _item.supuesto = item.supuesto;

                    _item.aud_usuariomod = item.aud_usuariomod;
                    _item.aud_ipmod = item.aud_ipmod;
                    _item.aud_fechamod = item.aud_fechamod;
                }

                db.SaveChanges();
            }
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var db = new SMECEntities())
            {
                db.MarcoLogicoTC.Remove(db.MarcoLogicoTC.Find(id));
                db.SaveChanges();
            }
        }

        [HttpPost]
        public void ResultadoDeleteArchivo(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.ResultadoMetaArchivoTC.Delete(item.id);
                db.SaveChanges();
            }
        }
    }
}
