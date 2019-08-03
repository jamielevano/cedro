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
    public class ConfiguracionController : ApiController
    {
        
        public IEnumerable<object> Search(ConfiguracionSearchModel data)
        {
            bool Admin = Auth.GetTicketData().Admin;
            string usuario=  HttpContext.Current.User.Identity.Name;

            System.Web.HttpContext.Current.Session["Configuracion_ejeintervencionid"] = data.ejeintervencionid;//cesar
            System.Web.HttpContext.Current.Session["Configuracion_telecentroid"] = data.telecentroid;
            System.Web.HttpContext.Current.Session["Configuracion_id"] = data.id ;
            System.Web.HttpContext.Current.Session["Configuracion_programaid"] = data.programaid;
            System.Web.HttpContext.Current.Session["Configuracion_nivelid"] = data.nivelid;
            System.Web.HttpContext.Current.Session["Configuracion_moduloid"] = data.moduloid;
            System.Web.HttpContext.Current.Session["Configuracion_anioid"] = data.anioid;
            System.Web.HttpContext.Current.Session["Configuracion_mesid"] = data.mesid;
            System.Web.HttpContext.Current.Session["Configuracion_organizacion"] = data.organizacion;
            System.Web.HttpContext.Current.Session["Configuracion_organizacion"] = data.capacitadorid;
            System.Web.HttpContext.Current.Session["Configuracion_fechaini"] = data.fechaini;
            System.Web.HttpContext.Current.Session["Configuracion_fechafin"] = data.fechafin;
            System.Web.HttpContext.Current.Session["Configuracion_participaid"] = data.participaid;
            //int telecentroid=0;

            //using (var db = new SMECEntities())
            //{
            //    telecentroid = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro??0;
            //}

            using (var db = new SMECEntities())
            {
                return db.Configuracion
                    .AsNoTracking()
                    .Where(x
                        => (!data.id.HasValue || x.id == data.id.Value)
                        && (!data.ejeintervencionid.HasValue || x.ejeintervencionid == data.ejeintervencionid.Value)
                        && (!data.telecentroid.HasValue || x.telecentroid == data.telecentroid.Value)
                        && (!data.programaid.HasValue || x.programaid == data.programaid.Value)
                        && (!data.nivelid.HasValue || x.nivelid == data.nivelid.Value)
                        && (!data.moduloid.HasValue || x.Modulos.Any(m=>m.moduloid == data.moduloid.Value))
                        //&& (!data.anioid.HasValue || x.fechafin.Value.Year == data.anioid.Value)
                        //&& (!data.mesid.HasValue || x.fechafin.Value.Month == data.mesid.Value)
                        && (!data.fechafin.HasValue || x.fechafin.Value >= data.fechaini.Value)
                        && (!data.fechafin.HasValue || x.fechafin.Value <= data.fechafin.Value)
                        && (!data.organizacion.HasValue || x.organizacion == data.organizacion.Value)
                        && (x.tipoid == data.tipoid)
                        && (!data.capacitadorid.HasValue || x.capacitadorid == data.capacitadorid.Value)
                        && (!data.participaid.HasValue || x.participaid == data.participaid.Value)
                        //&& (Admin || x.telecentroid == telecentroid)
                        )
                    .Select(x => new
                    {
                        x.id,
                        x.fechainicio,
                        x.fechafin,
                        nombre=x.nombre.ToUpper(),
                        x.ejeintervencionid,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.ejeintervencionid).nombre.ToUpper(),
                        x.telecentroid,
                        telecentro = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid && z.activo == 1).nombre.ToUpper(),
                        x.programaid,
                        programa = db.ListaDetalle.FirstOrDefault(z => z.listaid == 55 && z.codigo == x.programaid).nombre.ToUpper(),
                        x.organizacion,
                        organizacionotro = db.ListaDetalle.FirstOrDefault(z => z.listaid == 57 && z.codigo == x.organizacion).nombre.ToUpper(),
                        programacorto = db.ListaDetalle.FirstOrDefault(z => z.listaid == 55 && z.codigo == x.programaid).nombrecorto.ToUpper(),
                        x.nivelid,
                        nivel = db.ListaDetalle.FirstOrDefault(z => z.listaid == 56 && z.codigo == x.nivelid).nombre.ToUpper(),
                        x.capacitadorid,
                        capacitador = db.Usuario.FirstOrDefault(z => z.id == x.capacitadorid).nombre.ToUpper(),
                        cantidad = db.Inscripcion.Where(z => z.configuracionid == x.id).Count(),
                        x.cerrado,
                        x.fechacierre,
                        moduloid= (db.Modulos.FirstOrDefault(m=>m.configuracionid==x.id)==null)?0:db.Modulos.FirstOrDefault(m=>m.configuracionid==x.id).moduloid,
                        modulo = (x.programaid == 1 || x.programaid == 4) ? "" : db.ListaDetalle.FirstOrDefault(z => z.listaid == 39 &&
                                                                                     z.codigo == (db.Modulos.FirstOrDefault(m => m.configuracionid == x.id).moduloid)).nombre,
                        error = (x.cerrado == true) ? (db.Inscripcion.Where(z => z.configuracionid == x.id).Count() != db.Inscripcion.Where(z => z.configuracionid == x.id && z.condicionid!=null).Count() ) : false,
                        participacion = db.ListaDetalle.FirstOrDefault(z => z.listaid == 57 && z.codigo == x.organizacion).nombre.ToUpper()
                    })
                    .OrderByDescending(d=>d.fechainicio)
                    .ToList();
            }
        }


        
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
                        nombre=x.nombre.ToUpper(),
                        x.ejeintervencionid,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(z => z.id == x.ejeintervencionid).nombre.ToUpper(),
                        x.telecentroid,
                        telecentro = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid && z.activo == 1).nombre.ToUpper(),
                        x.programaid,
                        programa = db.ListaDetalle.FirstOrDefault(z => z.listaid == 55 && z.codigo == x.programaid).nombre.ToUpper(),
                        x.nivelid,
                        nivel = db.ListaDetalle.FirstOrDefault(z => z.listaid == 56 && z.codigo == x.programaid).nombre.ToUpper(),
                        x.capacitadorid,
                        capacitador = db.Usuario.FirstOrDefault(z => z.id == x.capacitadorid).nombre.ToUpper(),
                        x.cerrado,
                        x.organizacion,
                        organizacionotro= db.ListaDetalle.FirstOrDefault(z => z.listaid == 57 && z.codigo == x.organizacion).nombre.ToUpper()

                    })
                    .OrderByDescending(d => d.fechafin)
                    .ToList();
            }
        }


        [HttpGet]
        public Configuracion Find(int id)
        {
            using (SMECEntities db = new SMECEntities())
            {
                return db.Configuracion
                    .AsNoTracking()
                    .SingleOrDefault(x => x.id == id);
            }
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

                    if(item.cerrado == true)
                    {
                        var a = db.Modulos.FirstOrDefault(x => x.configuracionid == item.id);
                        var b = db.Sesion.FirstOrDefault(x => x.moduloid == a.id);
                        var d = db.Clase.FirstOrDefault(x => x.Sesion.id == b.id);
                        var c = db.Participantes.FirstOrDefault(x => x.claseid == d.id &&
                                                                (x.p4a == null || x.p4b == null || x.p4c == null || x.p4d == null ||
                                                                x.p5 == null || x.p6 == null || x.p7 == null || x.p8 == null || x.p9 == null ||
                                                                !(x.p10a != null || x.p10b != null || x.p10c != null) ||
                                                                !(x.p11a != null || x.p11b != null || x.p11c != null) ||
                                                                !(x.p12a != null || x.p12b != null || x.p12c != null) ||
                                                                x.p13 == null)
                        );

                        if (c != null)
                            return; 
                    }



                    var _item = db.Configuracion.SingleOrDefault(x => x.id == item.id);
                    _item.nombre = item.nombre;
                    _item.fechainicio = item.fechainicio;
                    _item.fechafin = item.fechafin;
                    _item.ejeintervencionid = item.ejeintervencionid;
                    _item.telecentroid = item.telecentroid;
                    _item.programaid = item.programaid;
                    _item.nivelid = item.nivelid;
                    _item.capacitadorid = item.capacitadorid;
                    _item.cerrado = item.cerrado;
                    _item.localidad = item.localidad;
                    _item.aud_usuariomod = item.aud_usuariomod;
                    _item.aud_ipmod = item.aud_ipmod;
                    _item.aud_fechamod = item.aud_fechamod;
                    _item.organizacion = item.organizacion;
                    _item.organizacionotro = item.organizacionotro;
                    _item.participaid = item.participaid;

                    if (_item.cerrado == true)
                    {
                        var inscripcion = db.Inscripcion.Where(x => x.configuracionid == _item.id);
                        var modulo = db.Modulos.Where(y => y.configuracionid == _item.id);                       
                        decimal horascurso =0;
                        foreach (var mod in modulo)
                        {
                            var _hora = db.Clase.SingleOrDefault(x => x.id == mod.id);//CESAR VM
                            if (_hora !=null) {                          
                            if (_hora.horas > 1)
                                horascurso = _hora.horas;
                               
                        }
                        }

                        var edad1 = 0;
                        var edad2 = 0;
                        var edad3 = 0;
                        var edad4 = 0;
                        var aprobados = 0;
                        var desaprobados = 0;
                        var retiradosdos = 0;
                        var nromujeres = 0;
                        var nrohombres = 0;
                        foreach (var ins in inscripcion)
                        {
                            var _persona = db.Persona.SingleOrDefault(x => x.id == ins.personaid);

                            //if (_persona.edad >= 5 && _persona.edad <= 11)
                            //    edad1++;

                            if (_persona.edad >= 12 && _persona.edad <= 17)
                                edad2++;

                            if (_persona.edad >= 18 && _persona.edad <= 30)
                                edad3++;

                            if (_persona.edad >= 31 && _persona.edad <= 200)
                                edad4++;
                            if (ins.condicionid == 1)
                                aprobados++;

                            if (ins.condicionid == 2)
                                desaprobados++;

                            if (ins.condicionid == 3)
                                retiradosdos++;

                            if (ins.condicionid == 9)
                                aprobados++;

                            if (ins.condicionid == 10)
                                desaprobados++;

                            if (_persona.sexoid == 1)
                                nrohombres++;

                            if (_persona.sexoid == 2)
                                nromujeres++;

                        }

                        _item.rango1b = edad1;
                        _item.rango2b = edad2;
                        _item.rango3b = edad3;
                        _item.rango4b = edad4;

                        _item.aprobados = aprobados;
                        _item.desaprobados = desaprobados;

                        _item.hombres = nrohombres;
                        _item.mujeres = nromujeres;

                        _item.aprobados = aprobados;
                        _item.desaprobados = desaprobados;
                        _item.retirados = retiradosdos;
                        _item.horas =Convert.ToInt16(horascurso);
                        _item.fechacierre = DateTime.Now;
                    }

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
