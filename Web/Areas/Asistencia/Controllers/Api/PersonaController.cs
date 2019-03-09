using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Web.Areas.Asistencia.Models;
using Web.Models;

namespace Web.Areas.Asistencia.Controllers.Api
{
    public class PersonaController : ApiController
    {
        
        public IEnumerable<object> Search(PersonaSearchModel data)
        {
            bool Admin = Auth.GetTicketData().Admin;
            bool Coordinador = Auth.GetTicketData().Coordinador;
            int Ejeid = Auth.GetTicketData().EjeId;
            string usuario = HttpContext.Current.User.Identity.Name;

            System.Web.HttpContext.Current.Session["Persona_telecentroid"] = data.telecentroid;
            System.Web.HttpContext.Current.Session["Persona_dni"] = data.dni;
            System.Web.HttpContext.Current.Session["Persona_apellidopaterno"] = data.apellidopaterno;
            System.Web.HttpContext.Current.Session["Persona_apellidomaterno"] = data.apellidomaterno;
            System.Web.HttpContext.Current.Session["Persona_nombre"] = data.nombre;
            System.Web.HttpContext.Current.Session["Persona_codigo"] = data.codigo; 

            using (var db = new SMECEntities())
            {
                var lista= db.vPersona
                    .AsNoTracking()
                    .Where(x
                        => (!data.telecentroid.HasValue || x.telecentroid == data.telecentroid.Value)
                        && (string.IsNullOrEmpty(data.dni) || x.dni == data.dni)
                        && (string.IsNullOrEmpty(data.apellidopaterno) || x.apellidopaterno == data.apellidopaterno)
                        && (string.IsNullOrEmpty(data.apellidomaterno) || x.apellidomaterno == data.apellidomaterno)
                        && (string.IsNullOrEmpty(data.nombre) || x.nombre == data.nombre)
                        && (string.IsNullOrEmpty(data.codigo) || x.codigo == data.codigo)
                        )
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        x.fecharegistro,
                        apellidopaterno=x.apellidopaterno.ToUpper(),
                        apellidomaterno=x.apellidomaterno.ToUpper(),
                        nombre=x.nombre.ToUpper(),
                        x.sexoid,
                        sexo=x.sexo.ToUpper(),
                        x.telecentroid,
                        telecentro=x.telecentro.ToUpper(),
                        x.ejeintervencionid,
                        ejeintervencion=x.ejeintervencion.ToUpper(),
                        localidad=x.localidad.ToUpper(),
                        dni = x.dni,
                        x.fechanacimiento,
                        x.edad,
                        lugarnacimiento=x.lugarnacimiento.ToUpper(),
                        x.ocupacionid,
                        ocupacion=x.ocupacion.ToUpper(),
                        x.telefono,
                        x.celular,
                        estado = false,
                        x.fotohash,
                        x.fotomb,
                        eliminable= (
                                        Admin || 
                                        //((Coordinador && x.ejeintervencionid== Ejeid)?true:false) ||
                                        ((x.eliminable==1)?true:false)
                                    )
                    })
                    .OrderBy(x => new { x.apellidopaterno, x.apellidomaterno, x.nombre })
                    .ToList();
                 return lista;
            }
        }

        public IEnumerable<object> SearchSimple(PersonaSearchModel data)
        {
            bool Admin = Auth.GetTicketData().Admin;
            string usuario = HttpContext.Current.User.Identity.Name;

            
            System.Web.HttpContext.Current.Session["Persona_telecentroid"] = data.telecentroid;
            System.Web.HttpContext.Current.Session["Persona_dni"] = data.dni;
            System.Web.HttpContext.Current.Session["Persona_apellidopaterno"] = data.apellidopaterno;
            System.Web.HttpContext.Current.Session["Persona_apellidomaterno"] = data.apellidomaterno;
            System.Web.HttpContext.Current.Session["Persona_nombre"] = data.nombre;
            System.Web.HttpContext.Current.Session["Persona_codigo"] = data.codigo;

            using (var db = new SMECEntities())
            {
                return db.vPersona
                    .AsNoTracking()
                    .Where(x
                        => (!data.telecentroid.HasValue || x.telecentroid == data.telecentroid.Value)
                        && (string.IsNullOrEmpty(data.dni) || x.dni == data.dni)
                        && (string.IsNullOrEmpty(data.apellidopaterno) || x.apellidopaterno == data.apellidopaterno)
                        && (string.IsNullOrEmpty(data.apellidomaterno) || x.apellidomaterno == data.apellidomaterno)
                        && (string.IsNullOrEmpty(data.nombre) || x.nombre == data.nombre)
                        && (string.IsNullOrEmpty(data.codigo) || x.codigo == data.codigo)
                        )
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        apellidopaterno=x.apellidopaterno.ToUpper(),
                        apellidomaterno=x.apellidomaterno.ToUpper(),
                        nombre=x.nombre.ToUpper(),
                        telecentro=x.telecentro.ToUpper(),
                        dni = x.dni,
                        estado = false,
                        x.fotohash,
                        x.fotomb,
                        eliminable = (Admin || ((x.eliminable == 1) ? true : false))
                    })
                    .OrderBy(x => new { x.apellidopaterno, x.apellidomaterno, x.nombre })
                    .ToList();
            }
        }

        public int EliminarDuplicados()
        {
            new Repositorio.General().ExecuteStoredProcedure_Single(new SMECEntities(), "pmnt_eliminar_duplicados");
            return 1;
        }


        
        public IEnumerable<object> List()
        {
            using (var db = new SMECEntities())
            {
                return db.Persona
                    .AsNoTracking()
                    .Select(x => new
                    {
                        x.id,
                        x.codigo,
                        apellidopaterno=x.apellidopaterno.ToUpper(),
                        apellidomaterno=x.apellidomaterno.ToUpper(),
                        nombre=x.nombre.ToUpper(),
                        x.sexoid,
                        sexo = db.ListaDetalle.FirstOrDefault(z => z.listaid == 42 && z.codigo == x.sexoid).nombre.ToUpper(),
                        x.telecentroid,
                        telecentro= db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid).nombre.ToUpper(),
                        x.ejeintervencionid,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.ejeintervencionid).nombre.ToUpper(),
                        localidad=x.localidad.ToUpper(),
                        x.dni,
                        x.fechanacimiento,
                        x.edad,
                        lugarnacimiento=x.lugarnacimiento.ToUpper(),
                        x.ocupacionid,
                        ocupacion = db.ListaDetalle.FirstOrDefault(z => z.listaid == 43 && z.codigo == x.ocupacionid).nombre.ToUpper(),
                        x.aud_usuariomod,
                        x.telefono,
                        x.celular,
                        estado = false,
                    })
                    .OrderBy(x => new { x.apellidopaterno, x.apellidomaterno, x.nombre })
                    .ToList();
            }
        }

        [HttpGet]
        public Persona Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Persona.AsNoTracking()
                    .SingleOrDefault(x => x.id == id);
            }
        }

        public Persona Find_Dni(string id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Persona.AsNoTracking()
                    .SingleOrDefault(x => x.dni == id);
            }
        }

        public string Save(Persona item)
        {
            string respuesta = "";

            try
            {

                using (SMECEntities db = new SMECEntities())
                {
                    item.aud_usuariomod = User.Identity.Name;
                    item.aud_ipmod = Request.GetClientIp();
                    item.aud_fechamod = DateTime.Now.GetAudFormat();



                    if (item.id == 0)
                    {
                        if (item.dni != "00000000")
                        {
                            var userdni = db.Persona.Where(x => x.dni == item.dni).FirstOrDefault();

                            if (userdni != null)
                            {
                                respuesta = "El dni ingresado ya existe!";
                                throw new Exception("El dni ingresado ya existe!");
                            }
                        }

                        item.aud_usuarioreg = item.aud_usuariomod;
                        item.aud_ipreg = item.aud_ipmod;
                        item.aud_fechareg = item.aud_fechamod;

                        db.Persona.Add(item);
                    }
                    else
                    {
                        if (item.dni != "00000000")
                        {
                            var userdni = db.Persona.Where(x => x.dni == item.dni && x.id != item.id).FirstOrDefault();

                            if (userdni != null)
                            {
                                respuesta = "El dni ingresado ya existe!";
                                throw new Exception("El dni ingresado ya existe!");
                            }
                        }

                        var _item = db.Persona.SingleOrDefault(x => x.id == item.id);

                        _item.apellidopaterno = item.apellidopaterno;
                        _item.apellidomaterno = item.apellidomaterno;
                        _item.nombre = item.nombre;
                        _item.estadocivilid = item.estadocivilid;
                        _item.sexoid = item.sexoid;
                        _item.telecentroid = item.telecentroid;
                        _item.ejeintervencionid = item.ejeintervencionid;
                        _item.localidad = item.localidad;
                        _item.direccion = item.direccion;
                        _item.gradoinstruccionid = item.gradoinstruccionid;
                        _item.dni = item.dni;
                        _item.fechanacimiento = item.fechanacimiento;
                        _item.lugarnacimiento = item.lugarnacimiento;
                        _item.ocupacionid = item.ocupacionid;
                        _item.ocupacionotro = item.ocupacionotro;
                        _item.centrotrabajo = item.centrotrabajo;
                        _item.telefono = item.telefono;
                        _item.celular = item.celular;
                        _item.email = item.email;

                        _item.asociacion = item.asociacion;
                        _item.cargoocupa = item.cargoocupa;
                        _item.hectareas = item.hectareas;
                        _item.observacion = item.observacion;

                        _item.tipocomunidadid = item.tipocomunidadid;
                        _item.tiempoorganizacion = item.tiempoorganizacion;
                        _item.tiempotelecentro = item.tiempotelecentro;

                        _item.aud_usuariomod = item.aud_usuariomod;
                        _item.aud_ipmod = item.aud_ipmod;
                        _item.aud_fechamod = item.aud_fechamod;

                        _item.usodatos = item.usodatos;
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            { 
            
            }

            return respuesta;
            
        }

        [HttpPost]
        public void Delete(DeleteModel item)
        {
            using (SMECEntities db = new SMECEntities())
            {
                db.Persona.Remove(db.Persona.Find(item.id));
                db.SaveChanges();
            }
        }


        
    }
}
