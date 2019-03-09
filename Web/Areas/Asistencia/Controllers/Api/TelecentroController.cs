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
    public class TelecentroController : ApiController
    {
        
        public IEnumerable<object> Search(ConfiguracionSearchModel data)
        {
            bool Admin = Auth.GetTicketData().Admin;
            string usuario = HttpContext.Current.User.Identity.Name;

            System.Web.HttpContext.Current.Session["Acceso_telecentroid"] = data.telecentroid;
            System.Web.HttpContext.Current.Session["Acceso_anioid"] = data.anioid;
            System.Web.HttpContext.Current.Session["Acceso_mesid"] = data.mesid;

            int telecentroid = 0;

            using (var db = new SMECEntities())
            {
                telecentroid = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro ?? 0;
            }

            using (var db = new SMECEntities())
            {
                return db.Configuracion
                    .AsNoTracking()
                    .Where(x
                        => (!data.telecentroid.HasValue || x.telecentroid == data.telecentroid.Value)
                        && (!data.anioid.HasValue || x.fechainicio.Value.Year == data.anioid.Value)
                        && (!data.mesid.HasValue || x.fechainicio.Value.Month == data.mesid.Value)
                        && (x.tipoid == data.tipoid)
                        && (Admin || x.telecentroid == telecentroid)
                        )
                    .Select(x => new
                    {
                        x.id,
                        x.fechainicio,
                        x.fechafin,
                        x.ejeintervencionid,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.ejeintervencionid).nombre.ToUpper(),
                        x.telecentroid,
                        telecentro = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid && z.activo == 1).nombre,
                        x.capacitadorid,
                        capacitador = db.Usuario.FirstOrDefault(z => z.id == x.capacitadorid).nombre.ToUpper(),
                        cantidad = db.Inscripcion.Where(z => z.configuracionid == x.id).Count(),
                    })
                    .OrderByDescending(d => d.fechainicio)
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<object> List(int tipoid )
        {
            using (var db = new SMECEntities())
            {
                return db.Configuracion
                    .AsNoTracking()
                    .Where(x => x.tipoid == tipoid)
                    .Select(x => new
                    {
                        x.id,
                        x.fechainicio,
                        x.fechafin,
                        x.ejeintervencionid,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.ejeintervencionid).nombre,
                        x.telecentroid,
                        telecentro = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid && z.activo==1).nombre,
                        x.capacitadorid,
                        capacitador = db.Usuario.FirstOrDefault(z => z.id == x.capacitadorid).nombre,
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public Configuracion Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Configuracion.AsNoTracking()
                    .SingleOrDefault(x => x.id == id);
            }
        }


        public string Importar(ImportarTelecentroModel item)
        {
            return new Repositorio.General().ExecuteStoredProcedure_Single(new SMECEntities(),
                     "pmnt_importar_telecentro",
                        new[] { new SqlParameter{ParameterName="@telecentro", Value= (object)item.telecentro ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@responsable", Value= (object)item.responsable ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@fecha", Value= (object)item.fecha ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@nombre", Value= (object)item.nombre ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@apellidos", Value= (object)item.apellidos ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@sexo", Value= (object)item.sexo ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@institucion", Value= (object)item.institucion ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@localidad", Value= (object)item.localidad ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@dni", Value= (object)item.dni ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@fechanac", Value= (object)item.fechanac ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@email", Value= (object)item.email ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@actividad", Value= (object)item.actividad ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@horaini", Value= (object)item.horaini ?? DBNull.Value } ,
                                new SqlParameter{ParameterName="@horafin",Value= (object)item.horafin ?? DBNull.Value } 
            });

        }

        public void Save(Configuracion item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                item.aud_usuariomod = User.Identity.Name;
                item.aud_ipmod = Request.GetClientIp();
                item.aud_fechamod = DateTime.Now.GetAudFormat();


                if (item.id == 0)
                {
                    item.aud_usuarioreg = item.aud_usuariomod;
                    item.aud_ipreg = item.aud_ipmod;
                    item.aud_fechareg = item.aud_fechamod;

                    db.Configuracion.Add(item);
                }
                else
                {
                    var _item = db.Configuracion.SingleOrDefault(x => x.id == item.id);

                    _item.fechainicio = item.fechainicio;
                    _item.fechafin = item.fechafin;
                    _item.horas = item.horas;
                    _item.ejeintervencionid = item.ejeintervencionid;
                    _item.telecentroid = item.telecentroid;
                    _item.programaid = item.programaid;
                    _item.capacitadorid = item.capacitadorid;

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
            using (SMECEntities db = new SMECEntities())
            {
                db.Configuracion.Remove(db.Configuracion.Find(item.id));
                db.SaveChanges();
            }
        }
        
    }
}
