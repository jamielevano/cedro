using DatabaseContext;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class MarcoLogicoController : ApiController
    {
        [HttpGet]
        public object Find(int id)
        {
            using (var db = new SMECEntities())
            {

                return db.MarcoLogico
                    .AsNoTracking()
                    .Include(x => x.PropositoMetas)
                    .Include(x => x.Resultado.Select(y => y.ResultadoMetas))
                    .Where(x => x.proyectoid == id)
                    .Select(x => new
                    {
                        x.id,
                        x.fin,
                        x.codigo,
                        x.periodoinicio,
                        x.periodofin,
                        x.proposito,
                        x.supuesto,
                        habilitado = (x.Proyecto.habilitarmarcologico == true) ? false : true,
                        PropositoMetas = x.PropositoMetas.Select(m => new
                        {
                            m.id,
                            m.codigo,
                            m.marcoid,
                            m.nombre,
                            m.cantidad,
                            m.unidadid,
                            unidadmedida = db.ListaDetalle.FirstOrDefault(z => z.listaid == 1 && z.id == m.unidadid).nombre,
                            m.indicador,
                            m.fuente,
                            archivos = m.PropositoMetaArchivo.Any(),
                            ejecutado = (int?)m.AvancePropositoMetas.Sum(s => s.cantidad) ?? 0,
                            AvancePropositoMetas = m.AvancePropositoMetas.Select(a => new
                            {
                                a.id,
                                a.fechainicio,
                                a.fechafin,
                                a.cantidad
                            }),
                        }).OrderBy(o=>o.codigo),
                        Resultado = x.Resultado.Select(r => new
                        {
                            r.id,
                            r.codigo,
                            r.nombre,
                            r.supuesto,
                            ResultadoMetas = r.ResultadoMetas.Select(m => new
                            {
                                m.id,
                                m.codigo,
                                m.nombre,
                                m.cantidad,
                                m.unidadid,
                                m.indicador,
                                m.fuente,
                                archivos = m.ResultadoMetaArchivo.Any(),
                            }).OrderBy(o=>o.codigo),
                            eliminable = !r.PlanOperativo.Any()
                        }).OrderBy(o => o.codigo)
                    }).FirstOrDefault();
            }
        }

        [HttpGet]
        public object FindByProposito(int id)
        {

            using (SMECEntities db = new SMECEntities())
            {
                var resultado = db.Proyecto
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        x.nombre,
                        proposito=x.MarcoLogico.FirstOrDefault().proposito,
                        periodoinicio = x.MarcoLogico.FirstOrDefault().periodoinicio,
                        periodofin = x.MarcoLogico.FirstOrDefault().periodofin,
                        PropositoMetas = db.PropositoMetas.Where(y => y.MarcoLogico.proyectoid == x.id).Select(m => new
                        {  
                            m.id,
                            m.codigo,
                            m.unidadid,
                            unidadmedida = db.ListaDetalle.FirstOrDefault(z => z.listaid == 1 && z.id == m.unidadid).nombre,
                            m.marcoid,
                            m.nombre,
                            m.cantidad,
                            m.indicador,
                            m.fuente,
                            ejecutado = (int?)m.AvancePropositoMetas.Sum(s => s.cantidad) ?? 0,
                            AvancePropositoMetas = m.AvancePropositoMetas.Select(a => new
                            {
                                a.id,
                                a.fechainicio,
                                a.fechafin,
                                a.cantidad,
                                a.observacion,
                                archivos = a.AvancePropositoMetas_Archivo.Any()
                            }).OrderBy(z => z.fechainicio),
                        })
                    })
                    .FirstOrDefault(x => x.id == id);

                return resultado;
            }
        }

        public void Save(MarcoLogico item)
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

                    foreach (var propositoMetas in item.PropositoMetas)
                    {
                        propositoMetas.aud_usuarioreg = propositoMetas.aud_usuariomod = item.aud_usuariomod;
                        propositoMetas.aud_ipreg = propositoMetas.aud_ipmod = item.aud_ipmod;
                        propositoMetas.aud_fechareg = propositoMetas.aud_fechamod = item.aud_fechamod;
                    }

                    foreach (var resultado in item.Resultado)
                    {
                        resultado.aud_usuarioreg = resultado.aud_usuariomod = item.aud_usuariomod;
                        resultado.aud_ipreg = resultado.aud_ipmod = item.aud_ipmod;
                        resultado.aud_fechareg = resultado.aud_fechamod = item.aud_fechamod;

                        foreach (var resultadometas in resultado.ResultadoMetas)
                        {
                            resultadometas.aud_usuarioreg = resultadometas.aud_usuariomod = item.aud_usuariomod;
                            resultadometas.aud_ipreg = resultadometas.aud_ipmod = item.aud_ipmod;
                            resultadometas.aud_fechareg = resultadometas.aud_fechamod = item.aud_fechamod;
                        }
                    }

                    db.MarcoLogico.Add(item);
                }
                else
                {
                    var _item = db.MarcoLogico
                        .Include(x => x.PropositoMetas)
                        .Include(x => x.Resultado.Select(y => y.ResultadoMetas))
                        .SingleOrDefault(x => x.id == item.id);

                    db.MergeCollections(item.PropositoMetas, _item.PropositoMetas, x => x.id, (x, _x) =>
                    {
                        _x.codigo = x.codigo;
                        _x.nombre = x.nombre;
                        _x.cantidad = x.cantidad;
                        _x.unidadid = x.unidadid;
                        _x.indicador = x.indicador;
                        _x.fuente = x.fuente;
                        _x.aud_usuariomod = item.aud_usuariomod;
                        _x.aud_ipmod = item.aud_ipmod;
                        _x.aud_fechamod = item.aud_fechamod;
                    }, x =>
                    {
                        x.aud_usuarioreg = x.aud_usuariomod = item.aud_usuariomod;
                        x.aud_ipreg = x.aud_ipmod = item.aud_ipmod;
                        x.aud_fechareg = x.aud_fechamod = item.aud_fechamod;
                    });

                    db.MergeCollections(item.Resultado, _item.Resultado, x => x.id, (x, _x) =>
                    {
                        _x.codigo = x.codigo;
                        _x.nombre = x.nombre;
                        _x.supuesto = x.supuesto;
                        _x.aud_usuariomod = item.aud_usuariomod;
                        _x.aud_ipmod = item.aud_ipmod;
                        _x.aud_fechamod = item.aud_fechamod;
                        db.MergeCollections(x.ResultadoMetas, _x.ResultadoMetas, y => y.id, (y, _y) =>
                        {
                            _y.codigo = y.codigo;
                            _y.nombre = y.nombre;
                            _y.cantidad = y.cantidad;
                            _y.unidadid = y.unidadid;
                            _y.indicador = y.indicador;
                            _y.fuente = y.fuente;
                            _y.aud_usuariomod = item.aud_usuariomod;
                            _y.aud_ipmod = item.aud_ipmod;
                            _y.aud_fechamod = item.aud_fechamod;
                        }, y =>
                        {
                            y.aud_usuarioreg = y.aud_usuariomod = item.aud_usuariomod;
                            y.aud_ipreg = y.aud_ipmod = item.aud_ipmod;
                            y.aud_fechareg = y.aud_fechamod = item.aud_fechamod;
                        });
                    }, x =>
                    {
                        x.aud_usuarioreg = x.aud_usuariomod = item.aud_usuariomod;
                        x.aud_ipreg = x.aud_ipmod = item.aud_ipmod;
                        x.aud_fechareg = x.aud_fechamod = item.aud_fechamod;
                        x.ResultadoMetas.ToList().ForEach(y =>
                        {
                            y.aud_usuarioreg = y.aud_usuariomod = item.aud_usuariomod;
                            y.aud_ipreg = y.aud_ipmod = item.aud_ipmod;
                            y.aud_fechareg = y.aud_fechamod = item.aud_fechamod;
                        });
                    });

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
                db.MarcoLogico.Remove(db.MarcoLogico.Find(id));
                db.SaveChanges();
            }
        }
    }
}
