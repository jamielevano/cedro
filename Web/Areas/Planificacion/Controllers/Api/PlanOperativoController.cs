using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class PlanOperativoController : ApiController
    {

        [HttpGet]
        public IEnumerable<object> List(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.Resultado
                    .Where(x => x.MarcoLogico.proyectoid == id)
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        x.nombre,
                        PlanOperativo = x.PlanOperativo.Select(y => new
                        {
                            y.id,
                            y.codigo,
                            y.actividad,
                            y.presupuesto,
                            y.periodoinicio,
                            y.periodofin,
                            eliminable = !y.PlanOperativoMeta.Where(z => z.AvancePO.Any()).Any()
                        }).OrderBy(o => o.codigo)
                    }).OrderBy(o => o.codigo)
                    .ToList();
            }
        }

        [HttpGet]
        public object FindByProyecto(int id)
        {

            using (SMECEntities db = new SMECEntities())
            {
                var resultado = db.Proyecto
                    .AsNoTracking()
                    .Select(x => new
                    {
                        id = x.id,
                        codigo = x.codigo,
                        nombre = x.nombre,
                        periodoinicio = x.MarcoLogico.FirstOrDefault().periodoinicio,
                        periodofin = x.MarcoLogico.FirstOrDefault().periodofin,
                        Resultado = db.Resultado.Where(y => y.MarcoLogico.proyectoid == x.id).Select(m => new
                        {
                            m.id,
                            m.codigo,
                            m.nombre,
                            ResultadoMetas = m.ResultadoMetas.Select(r => new
                            {
                                r.id,
                                r.codigo,
                                r.nombre,
                                r.cantidad,
                                r.unidadid,
                                unidadmedida = db.ListaDetalle.FirstOrDefault(z => z.listaid == 1 && z.id == r.unidadid).nombre,
                                ejecutado = (int?)r.AvanceResultadoMetas.Sum(s => s.cantidad) ?? 0,
                                AvanceResultadoMetas = r.AvanceResultadoMetas.Select(a => new
                                {
                                    a.id,
                                    a.fechainicio,
                                    a.fechafin,
                                    a.cantidad,
                                    a.observacion,
                                    archivos = a.AvanceResultadoMetas_Archivo.Any()
                                }).OrderBy(z => z.fechainicio),
                            }).OrderBy(o => o.codigo)
                        }).OrderBy(o => o.codigo),
                        PlanOperativo = db.PlanOperativo.Where(y => y.Resultado.MarcoLogico.proyectoid == x.id).Select(m => new
                        {
                            m.id,
                            m.codigo,
                            m.resultadoid,
                            resultado = m.Resultado.nombre,
                            actividaddescripcion = m.codigo + " " + m.actividad,
                            m.actividad,
                            m.presupuesto,
                            anioinicio = ((DateTime?)m.periodoinicio).Value.Year,
                            aniofin = ((DateTime?)m.periodofin).Value.Year,
                            periodoinicio = m.periodoinicio,
                            periodofin = m.periodofin,
                            PlanOperativoMeta = m.PlanOperativoMeta.Select(a => new
                            {
                                a.id,
                                a.codigo,
                                a.meta,
                                metadescripcion = a.codigo + " " + a.meta,
                                a.cantidad,
                                a.unidadid,
                                unidadmedida = db.ListaDetalle.FirstOrDefault(z => z.listaid == 1 && z.id == a.unidadid).nombre,
                                a.indicador,
                                a.fuente,
                                periodoinicio = a.PlanOperativo.periodoinicio,
                                periodofin = a.PlanOperativo.periodofin,
                                montoejecutado = a.AvancePO.Sum(b => (decimal?)b.monto) ?? 0,
                                metaejecutada = a.AvancePO.Sum(b => (decimal?)b.cantidad) ?? 0,
                                AvancePO = a.AvancePO.Select(y => new
                                {
                                    y.id,
                                    y.fechainicio,
                                    y.fechafin,
                                    y.monto,
                                    y.cantidad,
                                    y.observacion,
                                    archivos = y.AvancePO_Archivo.Any()
                                }).OrderBy(z => z.fechainicio)
                            }),
                        }).OrderBy(o => o.codigo)
                    })
                    .FirstOrDefault(x => x.id == id);

                return resultado;
            }
        }


        [HttpGet]
        public object Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.PlanOperativo
                    .AsNoTracking()
                    .Select(p => new
                    {
                        p.id,
                        p.resultadoid,
                        p.codigo,
                        p.actividad,
                        p.periodoinicio,
                        p.periodofin,
                        p.supuesto,
                        p.presupuesto,
                        PlanOperativoMeta = p.PlanOperativoMeta.Select(x => new
                        {
                            x.id,
                            x.codigo,
                            x.meta,
                            x.indicador,
                            x.unidadid,
                            x.cantidad,
                            x.fuente,
                            archivos = x.PlanOperativoMetaArchivo.Any(),
                            PlanOperativoMetaAnual = x.PlanOperativoMetaAnual.Select(m => new
                            {
                                m.pometaid,
                                m.anio,
                                m.cantidad
                            })
                        }),
                    })
                    .FirstOrDefault(p => p.id == id);
            }
        }

        [HttpGet]
        public object GetMetaByActividad(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.PlanOperativo
                    .AsNoTracking()
                    .Select(p => new
                    {
                        p.id,
                        p.codigo,
                        p.actividad,
                        p.periodoinicio,
                        p.periodofin,
                        PlanOperativoMeta = p.PlanOperativoMeta.Select(x => new
                        {
                            x.id,
                            x.codigo,
                            x.meta,
                            x.indicador,
                            unidadmedida = db.ListaDetalle.FirstOrDefault(z => z.listaid == 1 && z.id == x.unidadid).nombre,
                            PlanOperativoMetaAnual = x.PlanOperativoMetaAnual.Select(m => new
                            {
                                anio = m.anio,
                                cantidad = m.cantidad,
                                ejecutado = 0//x.PlanOperativo.AvancePO.Where(z => z.poid == m.pometaid && z.fecha.Year == m.anio).Sum(b => (decimal?)b.cantidad) ?? 0,
                            }),
                            //AvancePOMeta = x.AvancePOMeta.Select(a => new
                            //{
                            //    a.id,
                            //    a.fecha,
                            //    a.cantidad
                            //})
                        }),
                    })
                    .FirstOrDefault(x => x.id == id);
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> FindByResultado(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.PlanOperativo
                    .Where(x => x.resultadoid == id)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo + ' ' + x.actividad
                    })
                    .ToList();
            }
        }


        public void Save(PlanOperativo item)
        {
            using (var db = new SMECEntities())
            {
                item.aud_usuariomod = User.Identity.Name;
                item.aud_ipmod = Request.GetClientIp();
                item.aud_fechamod = DateTime.Now.GetAudFormat();


                foreach (var meta in item.PlanOperativoMeta)
                {
                    var year = ((DateTime?)item.periodoinicio).Value.Year;
                    meta.cantidad = 0;
                    foreach (var avance in meta.PlanOperativoMetaAnual)
                    {
                        avance.anio = year++;
                        meta.cantidad += avance.cantidad;
                    }
                }


                if (item.id == 0)
                {
                    item.aud_usuarioreg = item.aud_usuariomod;
                    item.aud_ipreg = item.aud_ipmod;
                    item.aud_fechareg = item.aud_fechamod;

                    foreach (var planoperativotareaTC in item.PlanOperativoMeta)
                    {
                        planoperativotareaTC.aud_usuarioreg = planoperativotareaTC.aud_usuariomod = item.aud_usuariomod;
                        planoperativotareaTC.aud_ipreg = planoperativotareaTC.aud_ipmod = item.aud_ipmod;
                        planoperativotareaTC.aud_fechareg = planoperativotareaTC.aud_fechamod = item.aud_fechamod;
                    }

                    db.PlanOperativo.Add(item);
                }
                else
                {
                    var _item = db.PlanOperativo
                        .Include(x => x.PlanOperativoMeta.Select(y => y.PlanOperativoMetaAnual))
                        .SingleOrDefault(x => x.id == item.id);

                    db.MergeCollections(item.PlanOperativoMeta, _item.PlanOperativoMeta, x => x.id,
                        (x, _x) =>
                        {
                            _x.codigo = x.codigo;
                            _x.meta = x.meta;
                            _x.cantidad = x.cantidad;
                            _x.unidadid = x.unidadid;
                            _x.indicador = x.indicador;
                            _x.fuente = x.fuente;
                            _x.aud_usuariomod = item.aud_usuariomod;
                            _x.aud_ipmod = item.aud_ipmod;
                            _x.aud_fechamod = item.aud_fechamod;

                            db.ReplaceCollection(x.PlanOperativoMetaAnual, _x.PlanOperativoMetaAnual);
                        },
                        x =>
                        {
                            x.aud_usuarioreg = x.aud_usuariomod = item.aud_usuariomod;
                            x.aud_ipreg = x.aud_ipmod = item.aud_ipmod;
                            x.aud_fechareg = x.aud_fechamod = item.aud_fechamod;
                        });

                    _item.codigo = item.codigo;
                    _item.actividad = item.actividad;
                    _item.periodoinicio = item.periodoinicio;
                    _item.periodofin = item.periodofin;
                    _item.resultadoid = item.resultadoid;
                    _item.presupuesto = item.presupuesto;
                    _item.supuesto = item.supuesto;


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
            using (var db = new SMECEntities())
            {
                db.PlanOperativo.Remove(db.PlanOperativo.Find(item.id));
                db.SaveChanges();
            }
        }

    }
}
