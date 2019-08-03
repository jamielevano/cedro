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
                        notasalida = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).notasalida,

                        p4a = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p4a,
                        p4b = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p4b,
                        p4c = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p4c,
                        p4d = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p4d,

                        p5 = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p5,
                        p6 = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p6,
                        p7 = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p7,
                        p8 = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p8,
                        p9 = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p9,

                        p10a = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p10a,
                        p10b = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p10b,
                        p10c = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p10c,

                        p11a = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p11a,
                        p11b = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p11b,
                        p11c = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p11c,

                        p12a = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p12a,
                        p12b = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p12b,
                        p12c = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p12c,

                        p13 = db.Participantes.FirstOrDefault(z => z.claseid == id && z.personaid == x.personaid).p13

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
                                    notasalida = persona.notasalida,

                                    p4a = persona.p4a,
                                    p4b = persona.p4b,
                                    p4c = persona.p4c,
                                    p4d = persona.p4d,

                                    p5 = persona.p5,
                                    p6 = persona.p6,
                                    p7 = persona.p7,
                                    p8 = persona.p8,
                                    p9 = persona.p9,

                                    p10a = persona.p10a,
                                    p10b = persona.p10b,
                                    p10c = persona.p10c,

                                    p11a = persona.p11a,
                                    p11b = persona.p11b,
                                    p11c = persona.p11c,

                                    p12a = persona.p12a,
                                    p12b = persona.p12b,
                                    p12c = persona.p12c,

                                    p13 = persona.p13
                                });
                            else
                            {
                                participante.notaentrada = persona.notaentrada;
                                participante.notasalida = persona.notasalida;

                                participante.p4a = persona.p4a;
                                participante.p4b = persona.p4b;
                                participante.p4c = persona.p4c;
                                participante.p4d = persona.p4d;

                                participante.p5 = persona.p5;
                                participante.p6 = persona.p6;
                                participante.p7 = persona.p7;
                                participante.p8 = persona.p8;
                                participante.p9 = persona.p9;

                                participante.p10a = persona.p10a;
                                participante.p10b = persona.p10b;
                                participante.p10c = persona.p10c;

                                participante.p11a = persona.p11a;
                                participante.p11b = persona.p11b;
                                participante.p11c = persona.p11c;

                                participante.p12a = persona.p12a;
                                participante.p12b = persona.p12b;
                                participante.p12c = persona.p12c;

                                participante.p13 = persona.p13;
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
