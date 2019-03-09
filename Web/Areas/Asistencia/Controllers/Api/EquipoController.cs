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
using Web.Areas.Asistencia.Models;
using Web.Models;

namespace Web.Areas.Asistencia.Controllers.Api
{
    public class EquipoController : ApiController
    {
        
        [HttpGet]
        public IEnumerable<object> List(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.Inscripcion
                    .AsNoTracking()
                    .Include(x => x.Persona)
                    .Where(x => x.configuracionid == id)
                    .Select(x => new
                    {
                        x.id,
                        x.personaid,
                        apellidopaterno=x.Persona.apellidopaterno.ToUpper(),
                        apellidomaterno = x.Persona.apellidomaterno.ToUpper(),
                        nombre = x.Persona.nombre.ToUpper(),
                        dni=x.Persona.dni,
                        x.horainicio,
                        x.horafin,
                        x.usoid,
                        uso = db.ListaDetalle.FirstOrDefault(z => z.listaid == 44 && z.codigo == x.usoid).nombre
                        //ejeintervencionid=x.Persona.ejeintervencionid,
                        //ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.Persona.ejeintervencionid).nombre,
                        //localidadid=x.Persona.localidadid,
                        //localidad = db.Localidad.FirstOrDefault(z => z.ejeid == x.Persona.ejeintervencionid && z.id == x.Persona.localidadid).nombre,
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public Inscripcion Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Inscripcion.AsNoTracking()
                    .SingleOrDefault(x => x.id == id);
            }
        }

        public void Update(EquipoModel item)
        {   
            using (SMECEntities db = new SMECEntities())
            {
                var _item = db.Inscripcion.SingleOrDefault(x => x.id == item.id);
                _item.horainicio = item.horainicio;
                _item.horafin = item.horafin;
                _item.usoid = item.usoid;
                _item.usootro = item.usootro;
                db.SaveChanges();
            }
        }


        public void Save(InscripcionModel item)
        {
            foreach (var persona in item.persona)
            {
                if (persona.estado == true)
                {
                    try
                    {
                        using (SMECEntities db = new SMECEntities())
                        {
                            db.Inscripcion.Add(new Inscripcion { personaid = persona.id, configuracionid = item.id });
                            db.SaveChanges();
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }   
            }
            
        }

        [HttpPost]
        public void Delete(DeleteModel item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                db.Inscripcion.Remove(db.Inscripcion.Find(item.id));
                db.SaveChanges();
            }
        }
        
    }
}
