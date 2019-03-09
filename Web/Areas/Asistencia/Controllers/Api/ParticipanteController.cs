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
    public class ParticipanteController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List(int id)
        {
            using (var db = new SMECEntities())
            {
                int configuracionid = db.Clase.Include(x => x.Sesion.Modulos).FirstOrDefault(x => x.id == id).Sesion.Modulos.configuracionid;

                return db.Inscripcion
                    .AsNoTracking()
                    .Include(x => x.Persona)
                    .Where(x => x.configuracionid == configuracionid)
                    .Select(x => new
                    {
                        id=x.personaid,
                        apellidopaterno = x.Persona.apellidopaterno.ToUpper(),
                        apellidomaterno = x.Persona.apellidomaterno.ToUpper(),
                        nombre = x.Persona.nombre.ToUpper(),
                        dni = x.Persona.dni,
                        ejeintervencionid = x.Persona.ejeintervencionid,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.Persona.ejeintervencionid).nombre.ToUpper(),
                        //localidad = x.Persona.localidad.ToUpper(),
                        localidad = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.Persona.telecentroid).nombre,
                        estado = db.Participantes.Any(z => z.claseid == id && z.personaid == x.personaid),
                        notaentrada = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).notaentrada,
                        notasalida = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).notasalida
                    })                                                                      
                    .OrderBy(x => new { x.apellidopaterno, x.apellidomaterno, x.nombre })
                    .ToList();
            }   
        }


        public void Save(ParticipanteModel item)
        {
            foreach (var persona in item.persona)
            {
                if (persona.estado == true)
                {
                    try
                    {
                        using (SMECEntities db = new SMECEntities())
                        {

                            var participante = db.Participantes.SingleOrDefault(x => x.personaid == persona.id && 
                                                                          x.claseid == item.claseid);

                            if (participante == null)
                                db.Participantes.Add(new Participantes { personaid = persona.id, 
                                    claseid = item.claseid ,
                                    notaentrada= persona.notaentrada,
                                    notasalida = persona.notasalida
                                });
                            else
                            {
                                participante.notaentrada = persona.notaentrada;
                                participante.notasalida = persona.notasalida;
                            }

                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        using (SMECEntities db = new SMECEntities())
                        {
                            db.Participantes.Remove(db.Participantes.FirstOrDefault(x => x.personaid==persona.id && x.claseid == item.claseid));
                            db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            }
            
        }
    }
}
