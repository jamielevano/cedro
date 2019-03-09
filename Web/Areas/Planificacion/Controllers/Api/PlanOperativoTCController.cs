using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class PlanOperativoTCController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.ResultadoTC
                    .AsNoTracking()
                    .Where(x=>x.marcoid==id)
                    .Select(x => new
                    {
                        x.codigo,
                        x.nombre,
                        PlanOperativoTC = x.PlanOperativoTC.Select(y => new
                        {
                            y.id,
                            y.codigo,
                            y.actividad,
                            y.presupuesto,
                            y.periodoinicio,
                            y.periodofin,
                            eliminable = !y.PlanOperativoTareaTC.Any()
                        }).OrderBy(o=>o.codigo)
                    })
                    .OrderBy(a => a.codigo)
                    .ToList();

            }
        }

        [HttpGet]
        public IEnumerable<ListItemString> ListaTareas()
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTareaTC
                    .Select(x => new ListItemString
                    {
                        id = x.codigo,
                        nombre = x.codigo
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItemString> ListaTareasFilter(int id)
        {
            using (var db = new SMECEntities())
            {
                
                var res2 = db.PlanOperativoTareaTC
                .Where(x => x.PlanOperativoTC.ResultadoTC.marcoid == id)
                .Select(x => new ListItemString
                {
                    id = (x.id).ToString(),
                    //id = x.codigo,--cesar
                    nombre = x.codigo
                })
                .ToList();

                return res2;
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> FindByResultado(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTC
                    .Where(x => x.resultadoid == id)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo + "-" + x.actividad
                    })
                    .ToList();
            }
        }


        [HttpGet]
        public IEnumerable<ListItem> FindByTarea( int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTareaTC
                    .Where(x => x.poid == id)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo + "-" + x.tarea
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> FindAll(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTC
                    .Where(x=>x.ResultadoTC.marcoid==id)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.codigo + "-" + x.actividad,
                        codigo= x.codigo
                    })
                    .OrderBy(o=>o.codigo)
                    .ToList();
            }
        }

        [HttpGet]
        public object Find(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTC
                    .AsNoTracking()
                    .Include(x => x.PlanOperativoTareaTC)
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.resultadoid,
                        x.codigo,
                        x.actividad,
                        x.presupuesto,
                        x.periodoinicio,
                        x.periodofin,
                        x.supuesto,
                        marcoid=x.ResultadoTC.marcoid,
                        PlanOperativoTareaTC = x.PlanOperativoTareaTC.Select(m => new
                        {
                            m.id,
                            m.codigo,
                            m.tarea,
                            m.unidadid,
                            m.indicador,
                            m.fuente,
                            marcoid=m.PlanOperativoTC.ResultadoTC.marcoid,            
                            m.ficha1,
                            m.ficha2,
                            m.ficha3,
                            m.ficha4,
                            m.ficha6,
                            archivos=m.PlanOperativoTareaArchivoTC.Any()
                        }).OrderBy(o=>o.codigo)
                    })
                    .FirstOrDefault();
            }
        }

        [HttpGet]
        public object FindShort(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTC
                    .AsNoTracking()
                    .Include(x => x.PlanOperativoTareaTC)
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.resultadoid,
                        x.codigo,
                        x.actividad,
                        x.presupuesto,
                        x.periodoinicio,
                        x.periodofin,
                        x.supuesto
                    })
                    .FirstOrDefault();
            }
        }

        public void Save(PlanOperativoTC item)
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

                    foreach (var tarea in item.PlanOperativoTareaTC)
                    {
                        tarea.aud_usuarioreg = tarea.aud_usuariomod = item.aud_usuariomod;
                        tarea.aud_ipreg = tarea.aud_ipmod = item.aud_ipmod;
                        tarea.aud_fechareg = tarea.aud_fechamod = item.aud_fechamod;
                    }

                    db.PlanOperativoTC.Add(item);
                }
                else
                {
                    var _item = db.PlanOperativoTC
                        .Include(x => x.PlanOperativoTareaTC)
                        .SingleOrDefault(x => x.id == item.id);

                    db.MergeCollections(item.PlanOperativoTareaTC, _item.PlanOperativoTareaTC, x => x.id,
                        (x, _x) =>
                        {
                            _x.codigo = x.codigo;
                            _x.tarea = x.tarea;
                            _x.unidadid = x.unidadid;
                            _x.indicador = x.indicador;
                            _x.fuente = x.fuente;
                            _x.ficha1 = x.ficha1;
                            _x.ficha2 = x.ficha2;
                            _x.ficha3 = x.ficha3;
                            _x.ficha4 = x.ficha4;
                            _x.ficha6 = x.ficha6;
                            _x.aud_usuariomod = item.aud_usuariomod;
                            _x.aud_ipmod = item.aud_ipmod;
                            _x.aud_fechamod = item.aud_fechamod;

                            //db.ReplaceCollection(x.PlanOperativoTareaMetaAnualTC, _x.PlanOperativoTareaMetaAnualTC);
                        },
                        x =>
                        {
                            x.aud_usuarioreg = x.aud_usuariomod = item.aud_usuariomod;
                            x.aud_ipreg = x.aud_ipmod = item.aud_ipmod;
                            x.aud_fechareg = x.aud_fechamod = item.aud_fechamod;
                        });

                    _item.actividad = item.actividad;
                    _item.periodoinicio = item.periodoinicio;
                    _item.periodofin = item.periodofin;
                    _item.codigo = item.codigo;
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
        public void Delete(DeleteModel model)
        {
            using (var db = new SMECEntities())
            {
                db.PlanOperativoTC.Remove(db.PlanOperativoTC.Find(model.id));
                db.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> ListFichas()
        {
            using (SMECEntities db = new SMECEntities())
            {
                return new[] 
                        { 
                          new ListItem{ id=6, nombre="6"}
                       };
            }
        }

    }
}
