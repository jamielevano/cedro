using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.Asistencia.Controllers.Api
{
    public class ModuloController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.Modulos
                    .AsNoTracking()
                    .Where(x => x.configuracionid == id)
                    .Select(x => new
                    {
                        x.id,
                        x.moduloid,
                        modulo = db.ListaDetalle.FirstOrDefault(z => z.listaid == 56 && z.codigo == x.moduloid).nombre, //cesar cambio 39 x 56
                        x.orden,
                        archivos = x.ModuloArchivo.Any()
                    })
                    .OrderBy(x => x.orden)
                    .ToList();
            }
        }

        [HttpGet]
        public object Find(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.Modulos
                    .AsNoTracking()
                    .Include(x => x.Sesion.Select(y => y.Clase))
                    .Where(x => x.id == id)
                    .Select(m => new
                    {
                        m.id,
                        m.moduloid,
                        m.orden,
                        Sesion = m.Sesion.Select(s => new
                        {
                            s.id,
                            s.moduloid,
                            s.listaid,
                            s.orden,
                            Clase = s.Clase.Select(c => new
                            {
                                c.id,
                                c.sesionid,
                                c.listaid,
                                c.orden,
                                c.fecha,
                                c.horas,
                                c.horascalculadas,
                                c.horainicio,
                                c.horafin
                            }).OrderBy(o => o.orden)
                        }).OrderBy(o => o.orden)
                    })
                    .OrderBy(x => x.orden)
                    .SingleOrDefault();
            }
        }


        public void Save(Modulos item)
        {
            using (var db = new SMECEntities())
            {
                if (item.id == 0)
                {   
                    db.Modulos.Add(item);
                }
                else
                {
                    var _item = db.Modulos
                        .Include(x => x.Sesion.Select(y => y.Clase))
                        .SingleOrDefault(x => x.id == item.id);

                    // ----------------------

                    var deletedSesion = item.Sesion.Where(x => x.id != 0).Select(x => x.id).ToList();
                    _item.Sesion.Where(x => !deletedSesion.Contains(x.id)).ToList().ForEach(r =>
                    {
                        r.Clase.ToList().ForEach(y => db.Clase.Remove(y));
                        db.Sesion.Remove(r);
                        _item.Sesion.Remove(r);
                    });

                    foreach (var _r in _item.Sesion)
                    {
                        var r = item.Sesion.First(x => _r.id == x.id);
                        _r.listaid = r.listaid;
                        _r.moduloid = r.moduloid;
                        _r.orden = r.orden;
                        
                        // ----------------------

                        var deletedClase = r.Clase.Where(x => x.id != 0).Select(x => x.id).ToList();
                        _r.Clase.Where(x => !deletedClase.Contains(x.id)).ToList().ForEach(rm =>
                        {
                            db.Clase.Remove(rm);
                            _r.Clase.Remove(rm);
                        });

                        foreach (var _rm in _r.Clase)
                        {
                            var rm = r.Clase.First(x => _rm.id == x.id);
                            _rm.listaid = rm.listaid;
                            _rm.orden = rm.orden;
                            _rm.horas = rm.horas;
                            _rm.horainicio = rm.horainicio;
                            _rm.horafin = rm.horafin;
                            _rm.fecha = rm.fecha;
                        }

                        r.Clase.Where(x => x.id == 0).ToList().ForEach(rm =>
                        {
                            _r.Clase.Add(rm);
                        });

                        // ----------------------
                    }

                    item.Sesion.Where(x => x.id == 0).ToList().ForEach(r =>
                    {
                        
                        r.Clase.ToList().ForEach(rm =>
                        {
                        });

                        _item.Sesion.Add(r);
                    });

                    // ----------------------

                    _item.moduloid = item.moduloid;
                    _item.orden = item.orden;
                }

                db.SaveChanges(); 
            }
        }

        [HttpPost]
        public void Delete(DeleteModel item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                db.Modulos.Remove(db.Modulos.Find(item.id));
                db.SaveChanges();
            }
        }        
    }
}
