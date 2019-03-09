using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Web.Areas.Planificacion.Models;
using Web.Models;

namespace Web.Areas.Sistema.Controllers.Api
{
    public class RolController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List()
        {
            using (var db = new SMECEntities())
            {
                return db.Rol
                    .Select(x => new
                    {
                        id = x.id,
                        nombre = x.descripcion,
                        eliminable = !x.Usuario.Any()
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public object Find(int id)
        {
            using (var db = new SMECEntities())
            {
                
                var Modulo= db.Modulo
                    .AsNoTracking()
                    .Include(x => x.SubModulo.Select(y => y.Pagina))
                    .Where(x => x.id != 10)
                    .OrderBy(x=>x.orden)
                    .Select(x => new
                    {
                        x.id,
                        x.descripcion,
                        SubModulo = x.SubModulo.Select(m => new
                        {
                            m.id,
                            m.descripcion,
                            m.orden,
                            Pagina = m.Pagina.Select(p => new
                            {
                                p.id,
                                p.descripcion,
                                p.orden,
                                acceder = (bool?)db.Permiso.FirstOrDefault(e=>e.paginaid==p.id && e.rolid==id ).acceder??false,
                                agregar = (bool?)db.Permiso.FirstOrDefault(e => e.paginaid == p.id && e.rolid == id).agregar ?? false,
                                modificar = (bool?)db.Permiso.FirstOrDefault(e => e.paginaid == p.id && e.rolid == id).modificar ?? false,
                                eliminar = (bool?)db.Permiso.FirstOrDefault(e => e.paginaid == p.id && e.rolid == id).eliminar ?? false,
                                reporte = (p.SubModulo.Modulo.descripcion == "Reportes")?false:true
                            }).OrderBy(p=>p.orden),
                        }).OrderBy(m=>m.orden)
                    }).ToList();


                var descripcion = id == 0 ? string.Empty : db.Rol
                    .Where(x => x.id == id)
                    .Select(x => x.descripcion)
                    .Single();

                return new
                {
                    id,
                    descripcion,
                    Modulo
                };
            }
        }

        [HttpPost]
        public void Save(RolesModel item)
        {
            
            using (var db = new SMECEntities())
            {
                List<PaginasModel> listapagina = new List<PaginasModel>();

                #region creo lista
                foreach (var modulo in item.Modulo)
                {
                    foreach (var submodulo in modulo.SubModulo)
                    {
                        if (submodulo.Pagina == null) break;

                        foreach (var pagina in submodulo.Pagina)
                        {

                            listapagina.Add(new PaginasModel
                            {
                                id = pagina.id,
                                acceder = pagina.acceder,
                                agregar = pagina.agregar,
                                modificar = pagina.modificar,
                                eliminar = pagina.eliminar,
                            });

                        }
                    }
                }
                #endregion

                if (item.id == 0)
                {
                    Rol rol = new Rol { descripcion = item.descripcion, estado = 1 };

                    rol.aud_usuarioreg = rol.aud_usuariomod = User.Identity.Name;
                    rol.aud_ipreg =  rol.aud_ipmod = Request.GetClientIp();
                    rol.aud_fechareg =rol.aud_fechamod = DateTime.Now.GetAudFormat();

                    foreach (var pagina in listapagina)
                    {
                        if (pagina.acceder || pagina.agregar || pagina.modificar || pagina.eliminar)
                        {
                            rol.Permiso.Add(new Permiso
                            {
                                paginaid = pagina.id,
                                acceder = pagina.acceder,
                                agregar = pagina.agregar,
                                modificar = pagina.modificar,
                                eliminar = pagina.eliminar,
                            });
                        }
                    }

                    db.Rol.Add(rol);
                }
                else
                {

                    foreach (var pagina in listapagina)
                    {
                        var per = db.Permiso.FirstOrDefault(x => x.paginaid == pagina.id && x.rolid == item.id);

                        if (per == null)
                            db.Permiso.Add(new Permiso
                            {
                                rolid = item.id,
                                paginaid = pagina.id,
                                acceder = pagina.acceder,
                                agregar = pagina.agregar,
                                modificar = pagina.modificar,
                                eliminar = pagina.eliminar,
                            });
                        else
                        {
                            per.acceder = pagina.acceder;
                            per.agregar = pagina.agregar;
                            per.modificar = pagina.modificar;
                            per.eliminar = pagina.eliminar;
                        }
                    }
                }

                db.SaveChanges();
            }
          
        }

        [HttpPost]
        public void Delete(DeleteModel item)
        {
            using (var db = new SMECEntities())
            {
                db.Rol.Delete(item.id);
                db.SaveChanges();
            }
        }
    }
}
