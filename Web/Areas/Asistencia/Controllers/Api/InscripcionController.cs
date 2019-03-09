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
using Microsoft.VisualBasic;

namespace Web.Areas.Asistencia.Controllers.Api
{
    public class InscripcionController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> List(int id)
        {
            List<ParticipanteNotaModel> listanotas = new List<ParticipanteNotaModel>();

            using (var db = new SMECEntities())
            {
                var listapersonas = db.Inscripcion
                    .AsNoTracking()
                    .Include(x => x.Persona)
                    .Where(x => x.configuracionid == id)
                    .Select(x => new ParticipanteNotaModel
                    {
                        id=x.id,
                        personaid=x.personaid,
                        apellidopaterno=x.Persona.apellidopaterno.ToUpper(),
                        apellidomaterno = x.Persona.apellidomaterno.ToUpper(),
                        nombre = x.Persona.nombre.ToUpper(),
                        dni=x.Persona.dni,
                        fechanacimiento=x.Persona.fechanacimiento,
                        edad = x.Persona.edad,
                        sexoid=x.Persona.sexoid,
                        localidad = x.Persona.localidad.ToUpper(),
                        ocupacion = db.ListaDetalle.FirstOrDefault(z => z.listaid == 43 && z.codigo == x.Persona.ocupacionid).nombre.ToUpper(),
                        telefono=x.Persona.telefono,
                        correo = x.Persona.email.ToUpper(),
                        condicionid=x.condicionid,
                        condicion = db.ListaDetalle.FirstOrDefault(z => z.listaid == 45 && z.codigo == x.condicionid).nombre.ToUpper(),
                        notafinal = x.notafinal,
                        lineapre_1 =x.lineapre_1,
                        lineapost_1 = x.lineapost_1,
                        lineapre_2 = x.lineapre_2,
                        lineapost_2 = x.lineapost_2,
                        lineapre_3 = x.lineapre_3,
                        lineapost_3 = x.lineapost_3,
                        lineapre_4 = x.lineapre_4,
                        lineapost_4 = x.lineapost_4,
                        lineapre_5 = x.lineapre_5,
                        lineapost_5 = x.lineapost_5,
                        lineapre_6 = x.lineapre_6,
                        lineapost_6 = x.lineapost_6,
                        lineapre_7 = x.lineapre_7,
                        lineapost_7 = x.lineapost_7,
                        lineapre_8 = x.lineapre_8,
                        lineapost_8 = x.lineapost_8,
                        horas="00:00"
                    })
                    .OrderBy(x => new { x.apellidopaterno, x.apellidomaterno, x.nombre })
                    .ToList();

                listapersonas.ForEach(x =>
                {
                    x.horas = GetHoras(id, x.personaid);
                });

               
                 return listapersonas;
            }
        }

        public String GetHoras(int configuracionid, int personaid)
        {
            using (var db = new SMECEntities())
            {
                List<int> lista = new List<int>();
                decimal suma = 0;

                db.Participantes
                .Where(x => x.personaid == personaid)
                .Select(x => new
                {
                    x.claseid
                })
                .ToList().ForEach(a =>
                {
                    lista.Add(a.claseid);
                });

                

                var clases = db.Clase
                            .AsNoTracking()
                            .FirstOrDefault(x => x.Sesion.Modulos.configuracionid == configuracionid && lista.Contains(x.id));

                try
                {
                    if (clases != null)
                    {
                        suma = db.Clase
                            .AsNoTracking()
                            .Where(x => x.Sesion.Modulos.configuracionid == configuracionid && lista.Contains(x.id))
                            //.Sum(y => y.horascalculadas??0);
                            .Sum(y => y.horas);
                    }
                }
                catch 
                {
                    suma = 0;
                }

                return string.Format("{0}:{1}", Math.Floor(suma).ToString().PadLeft(2, '0'), ((int)(((suma - Math.Floor(suma)) * 60) / 1)).ToString().PadLeft(2, '0'));

                //List<TimeAll> collection = new List<TimeAll>();
                
                //clases.ForEach(x =>
                //{   
                //    DateTime StartTime = DateTime.Parse(x.horainicio);
                //    DateTime EndTime = DateTime.Parse(x.horafin);
                //    TimeSpan Span = EndTime.Subtract(StartTime);

                //    TimeAll mb = new TimeAll();
                //    mb.TheDuration = EndTime.Subtract(StartTime);
                //    collection.Add(mb);
                //});

                //var sum = (from r in collection select r.TheDuration.Ticks).Sum();
                //TimeSpan sumedup = new TimeSpan(sum);

                //DateTime TotalTime = DateTime.Parse(sumedup.Hours + ":" + sumedup.Minutes);
                //return TotalTime.ToString("HH:mm");

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

        public void Update(InscripcionFinal item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                var _item = db.Inscripcion.SingleOrDefault(x => x.id == item.id);
                _item.condicionid = item.condicionid;
                _item.notafinal = item.notafinal;
                _item.lineapre_1 =item.lineapre_1;
                _item.lineapost_1 = item.lineapost_1;
                _item.lineapre_2 = item.lineapre_2;
                _item.lineapost_2 = item.lineapost_2;
                _item.lineapre_3 = item.lineapre_3;
                _item.lineapost_3 = item.lineapost_3;
                _item.lineapre_4 = item.lineapre_4;
                _item.lineapost_4 = item.lineapost_4;
                _item.lineapre_5 = item.lineapre_5;
                _item.lineapost_5 = item.lineapost_5;
                _item.lineapre_6 = item.lineapre_6;
                _item.lineapost_6 = item.lineapost_6;
                _item.lineapre_7 = item.lineapre_7;
                _item.lineapost_7 = item.lineapost_7;
                _item.lineapre_8 = item.lineapre_8;
                _item.lineapost_8 = item.lineapost_8;
                _item.observacion = item.observacion;
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
                            var obj = db.Inscripcion.Where(x => x.personaid == persona.id && x.configuracionid == item.id).FirstOrDefault();

                            if (obj == null)
                            {
                                db.Inscripcion.Add(new Inscripcion { personaid = persona.id, configuracionid = item.id });
                                db.SaveChanges();
                            }
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
