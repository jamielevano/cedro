using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using Web.Areas.Monitoreo.Models;
using Web.Models;

namespace Web.Areas.Monitoreo.Controllers.Api
{
    public class TareaTCController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> Avances(int idTarea)
        {
            bool Admin = Auth.GetTicketData().Admin;            

            using (var db = new SMECEntities())
            {
                return db.AvancePOTC
                    .Where(x => x.tareaid == idTarea && (Admin || x.aud_usuarioreg == HttpContext.Current.User.Identity.Name))                   
                    .Select(x => new
                    {
                        x.id,
                        x.fecha,
                        usuario = x.aud_usuarioreg
                    }).ToList();
            }
        }

        private AvancePOTC _FindAvance(SMECEntities db, int id, dynamic tarea)
        {
            var q = db.AvancePOTC.Where(x => x.id == id);

            if (tarea.ficha6) q = q.Include(x => x.AvancePOTC_Ficha06.AvancePOTC_Ficha06Integrantes);
            if (tarea.ficha1) q = q.Include(x => x.AvancePOTC_Ficha01);
            if (tarea.ficha2) q = q.Include(x => x.AvancePOTC_Ficha02);
            if (tarea.ficha3) q = q.Include(x => x.AvancePOTC_Ficha03);
            if (tarea.ficha4) q = q.Include(x => x.AvancePOTC_Ficha04.AvancePOTC_Ficha04Integrantes);
            return q.FirstOrDefault();
        }

        [HttpGet]
        public object FindAvance(int id)
        {
            using (var db = new SMECEntities())
            {
                var tarea = db.AvancePOTC.Where(x => x.id == id)
                    .Select(x => new
                    {
                        id = x.PlanOperativoTareaTC.id,
                        fechaingreso= x.fechaingreso,
                        ficha1 = x.PlanOperativoTareaTC.ficha1,
                        ficha2 = x.PlanOperativoTareaTC.ficha2,
                        ficha3 = x.PlanOperativoTareaTC.ficha3,
                        ficha4 = x.PlanOperativoTareaTC.ficha4,
                        ficha6=x.PlanOperativoTareaTC.ficha6,
                        periodoinicio = x.PlanOperativoTareaTC.PlanOperativoTC.periodoinicio,
                        periodofin = x.PlanOperativoTareaTC.PlanOperativoTC.periodofin,
                        tarea = x.PlanOperativoTareaTC.codigo + "-" + x.PlanOperativoTareaTC.tarea,
                        actividad = x.PlanOperativoTareaTC.PlanOperativoTC.codigo + "-" + x.PlanOperativoTareaTC.PlanOperativoTC.actividad,
                        resultado = x.PlanOperativoTareaTC.PlanOperativoTC.ResultadoTC.codigo + "-" + x.PlanOperativoTareaTC.PlanOperativoTC.ResultadoTC.nombre,
                        horas = x.horas
                    })
                    .FirstOrDefault();

                var avance = _FindAvance(db, id, tarea);

                return new
                {
                    avance,
                    tarea
                };
            }
        }

        [HttpGet]
        public object FindCapacitacion(int id)
        {
            using (var db = new SMECEntities())
            {
                var datos = db.Configuracion.Where(x => x.id == id)
                    .Select(x => new
                    {
                      x.hombres,
                      x.mujeres,
                      x.productor,
                      x.noproductor,
                      rango1 = (x.programaid == 4) ? x.rango1b : x.rango1,
                      rango2 = (x.programaid == 4) ? x.rango2b : x.rango2,
                      rango3 = (x.programaid == 4) ? x.rango3b : x.rango3,
                      rango4 = (x.programaid == 4) ? x.rango4b : x.rango4,  
                      x.aprobados,
                      x.desaprobados,
                      x.retirados,
                      x.horas
                      
                    })
                    .FirstOrDefault();

                return datos;
            }
        }

        [HttpGet]
        public object FindPersona(string dni)
        {
            using (var db = new SMECEntities())
            {
                var datos = db.Persona.Where(x => x.dni == dni)
                    .Select(x => new
                    {
                        x.apellidopaterno,
                        x.apellidomaterno,
                        nombres=x.nombre,
                        x.sexoid,
                        x.dni,
                        x.edad,
                        x.fechanacimiento,
                        x.ocupacionid,
                        x.ocupacionotro,
                        localidad = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid).nombre,
                        x.telefono,
                        x.celular,
                        correo=x.email,
                        x.telecentroid,
                        telecentro = db.ListaDetalle.FirstOrDefault(z => z.listaid == 54 && z.codigo == x.telecentroid).nombre
                    })
                    .FirstOrDefault();

                return datos;
            }
        }

        public int MoveSave(MoveAvanceModel item)
        {
            using (var db = new SMECEntities())
            {
                var _item = db.AvancePOTC.SingleOrDefault(x => x.id == item.id);
                _item.tareaid = item.idTarea;
                db.SaveChanges();
            }

            return 1;
        }

        public int SaveAvance(AvancePOTC item)
        {
            int id=0;

            try
            {
                bool newrow = false;
                //if (item.descripcion == null) item.descripcion = string.Empty;

                using (var db = new SMECEntities())
                {
                    var tarea = db.PlanOperativoTareaTC.Where(x => x.id == item.tareaid)
                                .Select(x => new
                                {
                                    x.ficha1,
                                    x.ficha2,
                                    x.ficha3,
                                    x.ficha4,
                                    x.ficha6
                                })
                                .FirstOrDefault();

                    item.PlanOperativoTareaTC = null;




                   

                    if (tarea.ficha1 && item.AvancePOTC_Ficha01 != null) item.AvancePOTC_Ficha01.AvancePOTC = item; else item.AvancePOTC_Ficha01 = null;
                    if (tarea.ficha2 && item.AvancePOTC_Ficha02 != null) item.AvancePOTC_Ficha02.AvancePOTC = item; else item.AvancePOTC_Ficha02 = null;
                    if (tarea.ficha3 && item.AvancePOTC_Ficha03 != null) item.AvancePOTC_Ficha03.AvancePOTC = item; else item.AvancePOTC_Ficha03 = null;
                    if (tarea.ficha4 && item.AvancePOTC_Ficha04 != null) item.AvancePOTC_Ficha04.AvancePOTC = item; else item.AvancePOTC_Ficha04 = null;
                    if (tarea.ficha6 && item.AvancePOTC_Ficha06 != null) item.AvancePOTC_Ficha06.AvancePOTC = item; else item.AvancePOTC_Ficha06 = null;


                    item.aud_usuariomod = User.Identity.Name;
                    item.aud_ipmod = Request.GetClientIp();
                    item.aud_fechamod = DateTime.Now.GetAudFormat();



                    if (item.id == 0)
                    {
                        item.aud_usuarioreg = item.aud_usuariomod;
                        item.aud_ipreg = item.aud_ipmod;
                        item.aud_fechareg = item.aud_fechamod;



                        db.AvancePOTC.Add(item);

                        newrow = true;
                    }
                    else
                    {
                        var _item = _FindAvance(db, item.id, tarea);

                        #region AvancePOTC
                        //_item.fecha = item.fecha;
                        _item.ejeintervencionid = item.ejeintervencionid;
                        _item.sectorid = item.sectorid;
                        _item.telecentroid = item.telecentroid;
                        _item.fechainicio = item.fechainicio;
                        _item.fechafin = item.fechafin;
                        _item.fechareporte = item.fechareporte;
                        _item.promotorid = item.promotorid;
                        _item.descripcion = item.descripcion;
                        _item.discapacitados = item.discapacitados;
                        _item.nodiscapacitados = item.nodiscapacitados;
                        _item.poblacion01 = item.poblacion01;
                        _item.poblacion02 = item.poblacion02;
                        _item.poblacion03 = item.poblacion03;
                        _item.poblacion04 = item.poblacion04;
                        _item.poblacion05 = item.poblacion05;
                        _item.poblacion06 = item.poblacion06;
                        _item.poblacion07 = item.poblacion07;
                        _item.poblacion08 = item.poblacion08;
                        _item.poblacion09 = item.poblacion09;
                        _item.poblacion10 = item.poblacion10;
                        _item.poblacion06 = item.poblacion06;
                        _item.poblacion12 = item.poblacion12;
                        _item.poblacion13 = item.poblacion13;
                        _item.poblacion14 = item.poblacion14;
                        _item.poblacion15 = item.poblacion15;
                        _item.poblacion16 = item.poblacion16;
                        _item.poblacionotro = item.poblacionotro;
                        _item.retirados = item.retirados;
                        _item.evaluacion = item.evaluacion;
                        _item.certificados = item.certificados;
                        _item.totalparticipantes = item.totalparticipantes;
                        _item.horas = item.horas;

                        
                        _item.sustentoficha1 = item.sustentoficha1;
                        _item.sustentoficha2 = item.sustentoficha2;
                        _item.sustentoficha3 = item.sustentoficha3;
                        _item.sustentoficha4 = item.sustentoficha4;
                        _item.sustentoficha5 = item.sustentoficha5;
                        _item.sustentoficha6 = item.sustentoficha6;
                        _item.sustentovideos = item.sustentovideos;
                        _item.sustentootros = item.sustentootros;

                        _item.participanteshombres = item.participanteshombres;
                        _item.participantesmujeres = item.participantesmujeres;
                        _item.lugar = item.lugar;
                        _item.lugarnumerohoras = item.lugarnumerohoras;
                        _item.lugarcostohora = item.lugarcostohora;
                        _item.costototalalimentacion = item.costototalalimentacion;
                        _item.costototalmovilidad = item.costototalmovilidad;
                        _item.difusionemisionesradio = item.difusionemisionesradio;
                        _item.difusionemisionestv = item.difusionemisionestv;
                        _item.difusionemisionesprensa = item.difusionemisionesprensa;
                        _item.difusionsegundosradio = item.difusionsegundosradio;
                        _item.difusionsegundostv = item.difusionsegundostv;
                        _item.difusionsegundosprensa = item.difusionsegundosprensa;
                        _item.difusioncostoradio = item.difusioncostoradio;
                        _item.difusioncostotv = item.difusioncostotv;
                        _item.difusioncostoprensa = item.difusioncostoprensa;
                        _item.voluntariosnumero = item.voluntariosnumero;
                        _item.voluntarioshoras = item.voluntarioshoras;
                        _item.voluntarioscostohora = item.voluntarioscostohora;
                        _item.consultoresnumero = item.consultoresnumero;
                        _item.consultoreshoras = item.consultoreshoras;
                        _item.consultorescostohora = item.consultorescostohora;
                        _item.asesoriahoras = item.asesoriahoras;
                        _item.asesoriacostohora = item.asesoriacostohora;
                        _item.codigocapacitacion = item.codigocapacitacion;

                        _item.productores = item.productores;
                        _item.noproductores = item.noproductores;
                        _item.desaprobados = item.desaprobados;

                        _item.aud_fechamod = item.aud_fechamod;
                        _item.aud_usuariomod = item.aud_usuariomod;
                        _item.aud_ipmod = item.aud_ipmod;
                        #endregion

                        #region AvancePOTC_Ficha01
                        if (item.AvancePOTC_Ficha01 != null)
                        {
                            _item.AvancePOTC_Ficha01.tipodocumento01 = item.AvancePOTC_Ficha01.tipodocumento01;
                            _item.AvancePOTC_Ficha01.tipodocumento02 = item.AvancePOTC_Ficha01.tipodocumento02;
                            _item.AvancePOTC_Ficha01.tipodocumento03 = item.AvancePOTC_Ficha01.tipodocumento03;
                            _item.AvancePOTC_Ficha01.tipodocumento04 = item.AvancePOTC_Ficha01.tipodocumento04;
                            _item.AvancePOTC_Ficha01.tipodocumento05 = item.AvancePOTC_Ficha01.tipodocumento05;
                            _item.AvancePOTC_Ficha01.tipodocumento06 = item.AvancePOTC_Ficha01.tipodocumento06;
                            _item.AvancePOTC_Ficha01.tipodocumento07 = item.AvancePOTC_Ficha01.tipodocumento07;
                            _item.AvancePOTC_Ficha01.tipodocumentootro = item.AvancePOTC_Ficha01.tipodocumentootro;
                            
                            _item.AvancePOTC_Ficha01.titulo = item.AvancePOTC_Ficha01.titulo;
                            _item.AvancePOTC_Ficha01.tema = item.AvancePOTC_Ficha01.tema;
                            _item.AvancePOTC_Ficha01.versiones01 = item.AvancePOTC_Ficha01.versiones01;
                            _item.AvancePOTC_Ficha01.versiones02 = item.AvancePOTC_Ficha01.versiones02;
                            _item.AvancePOTC_Ficha01.versiones03 = item.AvancePOTC_Ficha01.versiones03;
                            _item.AvancePOTC_Ficha01.versionesotro = item.AvancePOTC_Ficha01.versionesotro;
                            _item.AvancePOTC_Ficha01.fechaalmacenamiento = item.AvancePOTC_Ficha01.fechaalmacenamiento;
                            _item.AvancePOTC_Ficha01.poblacion = item.AvancePOTC_Ficha01.poblacion;
                            _item.AvancePOTC_Ficha01.elaborado01 = item.AvancePOTC_Ficha01.elaborado01;
                            _item.AvancePOTC_Ficha01.elaborado02 = item.AvancePOTC_Ficha01.elaborado02;  
                            _item.AvancePOTC_Ficha01.autor = item.AvancePOTC_Ficha01.autor;
                            _item.AvancePOTC_Ficha01.aprobado = item.AvancePOTC_Ficha01.aprobado;
                        }
                        #endregion


                        #region AvancePOTC_Ficha02
                        if (item.AvancePOTC_Ficha02 != null)
                        {
                            _item.AvancePOTC_Ficha02.plataformatemas = item.AvancePOTC_Ficha02.plataformatemas;
                            _item.AvancePOTC_Ficha02.plataformafechacreacion = item.AvancePOTC_Ficha02.plataformafechacreacion;
                            _item.AvancePOTC_Ficha02.plataformafechadisponible = item.AvancePOTC_Ficha02.plataformafechadisponible;
                            _item.AvancePOTC_Ficha02.plataformafechaultima = item.AvancePOTC_Ficha02.plataformafechaultima;
                            _item.AvancePOTC_Ficha02.plataformaresponsable = item.AvancePOTC_Ficha02.plataformaresponsable;
                            _item.AvancePOTC_Ficha02.plataformanumeroentradas = item.AvancePOTC_Ficha02.plataformanumeroentradas;
                            _item.AvancePOTC_Ficha02.plataformanumerocomentarios = item.AvancePOTC_Ficha02.plataformanumerocomentarios;

                            _item.AvancePOTC_Ficha02.disenosmsimagenes = item.AvancePOTC_Ficha02.disenosmsimagenes;
                            _item.AvancePOTC_Ficha02.disenosmstexto = item.AvancePOTC_Ficha02.disenosmstexto;
                            _item.AvancePOTC_Ficha02.disenosmsaudio = item.AvancePOTC_Ficha02.disenosmsaudio;
                            _item.AvancePOTC_Ficha02.disenosmsvideo = item.AvancePOTC_Ficha02.disenosmsvideo;
                            _item.AvancePOTC_Ficha02.disenosmsfechaelaboracion = item.AvancePOTC_Ficha02.disenosmsfechaelaboracion;
                            _item.AvancePOTC_Ficha02.disenosmsfechadifusion = item.AvancePOTC_Ficha02.disenosmsfechadifusion;
                            _item.AvancePOTC_Ficha02.disenosmsdescripcion = item.AvancePOTC_Ficha02.disenosmsdescripcion;

                            _item.AvancePOTC_Ficha02.produccionafiche = item.AvancePOTC_Ficha02.produccionafiche;
                            _item.AvancePOTC_Ficha02.produccionposter = item.AvancePOTC_Ficha02.produccionposter;
                            _item.AvancePOTC_Ficha02.produccionlienzo = item.AvancePOTC_Ficha02.produccionlienzo;
                            _item.AvancePOTC_Ficha02.producciongigantografia = item.AvancePOTC_Ficha02.producciongigantografia;
                            _item.AvancePOTC_Ficha02.producciontriptico = item.AvancePOTC_Ficha02.producciontriptico;
                            _item.AvancePOTC_Ficha02.produccionvolante = item.AvancePOTC_Ficha02.produccionvolante;
                            _item.AvancePOTC_Ficha02.produccionjuegos = item.AvancePOTC_Ficha02.produccionjuegos;
                            _item.AvancePOTC_Ficha02.produccionotro = item.AvancePOTC_Ficha02.produccionotro;
                            _item.AvancePOTC_Ficha02.produccionfechaelaboracion = item.AvancePOTC_Ficha02.produccionfechaelaboracion;
                            _item.AvancePOTC_Ficha02.produccionimpresion = item.AvancePOTC_Ficha02.produccionimpresion;
                            _item.AvancePOTC_Ficha02.produccionejemplares = item.AvancePOTC_Ficha02.produccionejemplares;
                            _item.AvancePOTC_Ficha02.producciondescripcion = item.AvancePOTC_Ficha02.producciondescripcion;


                            _item.AvancePOTC_Ficha02.difusionspotvideo = item.AvancePOTC_Ficha02.difusionspotvideo;
                            _item.AvancePOTC_Ficha02.difusionspotaudio = item.AvancePOTC_Ficha02.difusionspotaudio;
                            _item.AvancePOTC_Ficha02.difusionspototro = item.AvancePOTC_Ficha02.difusionspototro;
                            _item.AvancePOTC_Ficha02.difusionmensaje = item.AvancePOTC_Ficha02.difusionmensaje;
                            _item.AvancePOTC_Ficha02.difusionduracion = item.AvancePOTC_Ficha02.difusionduracion;
                            _item.AvancePOTC_Ficha02.difusionfechaproduccion = item.AvancePOTC_Ficha02.difusionfechaproduccion;
                            _item.AvancePOTC_Ficha02.difusionlugarproduccion = item.AvancePOTC_Ficha02.difusionlugarproduccion;
                            _item.AvancePOTC_Ficha02.difusionresponsableproduccion = item.AvancePOTC_Ficha02.difusionresponsableproduccion;
                            _item.AvancePOTC_Ficha02.difusionfechaelaboracion1 = item.AvancePOTC_Ficha02.difusionfechaelaboracion1;
                            _item.AvancePOTC_Ficha02.difusiontitulo1 = item.AvancePOTC_Ficha02.difusiontitulo1;
                            _item.AvancePOTC_Ficha02.difusionfechaelaboracion2 = item.AvancePOTC_Ficha02.difusionfechaelaboracion2;
                            _item.AvancePOTC_Ficha02.difusiontitulo2 = item.AvancePOTC_Ficha02.difusiontitulo2;
                            _item.AvancePOTC_Ficha02.difusionfrecuencianombre1 = item.AvancePOTC_Ficha02.difusionfrecuencianombre1;
                            _item.AvancePOTC_Ficha02.difusionfrecuenciaveces1 = item.AvancePOTC_Ficha02.difusionfrecuenciaveces1;
                            _item.AvancePOTC_Ficha02.difusionfrecuencianombre2 = item.AvancePOTC_Ficha02.difusionfrecuencianombre2;
                            _item.AvancePOTC_Ficha02.difusionfrecuenciaveces2 = item.AvancePOTC_Ficha02.difusionfrecuenciaveces2;
                           
                        }
                        #endregion



                        #region AvancePOTC_Ficha03
                        if (item.AvancePOTC_Ficha03 != null)
                        {
                            _item.AvancePOTC_Ficha03.tiporeunion = item.AvancePOTC_Ficha03.tiporeunion;
                            _item.AvancePOTC_Ficha03.reunionotra = item.AvancePOTC_Ficha03.reunionotra;
                            _item.AvancePOTC_Ficha03.reuniontema = item.AvancePOTC_Ficha03.reuniontema;
                            _item.AvancePOTC_Ficha03.reunionlocal = item.AvancePOTC_Ficha03.reunionlocal;
                            _item.AvancePOTC_Ficha03.reunionfecha = item.AvancePOTC_Ficha03.reunionfecha;
                            _item.AvancePOTC_Ficha03.reunionadministrador = item.AvancePOTC_Ficha03.reunionadministrador;

                            _item.AvancePOTC_Ficha03.reunioncuerdo1 = item.AvancePOTC_Ficha03.reunioncuerdo1;
                            _item.AvancePOTC_Ficha03.reunioncuerdo2 = item.AvancePOTC_Ficha03.reunioncuerdo2;
                            _item.AvancePOTC_Ficha03.reunioncuerdo3 = item.AvancePOTC_Ficha03.reunioncuerdo3;
                            _item.AvancePOTC_Ficha03.reunioncuerdo4 = item.AvancePOTC_Ficha03.reunioncuerdo4;
                            _item.AvancePOTC_Ficha03.reunioncuerdo5 = item.AvancePOTC_Ficha03.reunioncuerdo5;
                            _item.AvancePOTC_Ficha03.fechaproxima = item.AvancePOTC_Ficha03.fechaproxima;
                            _item.AvancePOTC_Ficha03.reunionacomentarios = item.AvancePOTC_Ficha03.reunionacomentarios;
                        }
                        #endregion

                        #region AvancePOTC_Ficha04
                        if (item.AvancePOTC_Ficha04 != null)
                        {
                            if (_item.AvancePOTC_Ficha04 == null) _item.AvancePOTC_Ficha04 = new AvancePOTC_Ficha04 { };
                            db.MergeCollections(item.AvancePOTC_Ficha04.AvancePOTC_Ficha04Integrantes, _item.AvancePOTC_Ficha04.AvancePOTC_Ficha04Integrantes, x => x.id, (x, _x) =>
                            {
                                _x.nombres = x.nombres;
                                _x.apellidopaterno = x.apellidopaterno;
                                _x.apellidomaterno = x.apellidomaterno;
                                _x.sexoid = x.sexoid;
                                _x.dni = x.dni;
                                _x.fechanacimiento = x.fechanacimiento;
                                _x.ocupacionid = x.ocupacionid;
                                //_x.localidad = x.telecentro;
                                _x.localidad = x.localidad;
                                _x.telefono = x.telefono;
                                _x.celular = x.celular;
                                _x.correo = x.correo;
                                _x.telecentroid = x.telecentroid;

                            });
                        }
                        #endregion

                        #region AvancePOTC_Ficha06
                        if (item.AvancePOTC_Ficha06 != null)
                        {
                            if (_item.AvancePOTC_Ficha06 == null) _item.AvancePOTC_Ficha06 = new AvancePOTC_Ficha06 { };
                            db.MergeCollections(item.AvancePOTC_Ficha06.AvancePOTC_Ficha06Integrantes, _item.AvancePOTC_Ficha06.AvancePOTC_Ficha06Integrantes, x => x.id, (x, _x) =>
                            {
                                _x.nombres = x.nombres;
                                _x.apellidopaterno = x.apellidopaterno;
                                _x.apellidomaterno = x.apellidomaterno;
                                _x.sexoid = x.sexoid;
                                _x.dni = x.dni;
                                _x.fechanacimiento = x.fechanacimiento;
                                _x.ocupacionid = x.ocupacionid;
                                _x.localidad = x.localidad;
                                _x.telefono = x.telefono;
                                _x.celular = x.celular;
                                _x.correo = x.correo;
                                _x.telecentroid = x.telecentroid;

                            });
                        }
                        #endregion

                        Configuracion oConfiguracion = db.Configuracion.FirstOrDefault(x => x.id == item.codigocapacitacion);
                        if(oConfiguracion != null) oConfiguracion.horas = _item.horas;

                    }

                    db.SaveChanges();

                    if (newrow)
                    {
                        db.AvancePOTC_Archivo.Where(x => x.avanceid == item.temporalid).ToList().ForEach(x =>
                        {
                            x.avanceid = item.id;
                        });



                        db.SaveChanges();
                    }
                }

                id = item.id;
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                using (var db = new SMECEntities())
                {
                    db.Error.Add(new Error
                    {
                        mensaje = string.Format("Entity Validation Failed - errors follow:\n", sb.ToString()),
                        login = User.Identity.Name
                    });
                    db.SaveChanges();
                }


                throw;
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

             return id;
        }

        [HttpPost]
        public void Delete(DeleteModel model)
        {
            using (SMECEntities db = new SMECEntities())
            {
                db.AvancePOTC.Remove(db.AvancePOTC.Find(model.id));
                db.SaveChanges();
            }
        }

        public IEnumerable<object> Search(AvancePOTCSearchModel data)
        {
            //if (!Auth.GetTicketData().Admin) data.usuario = User.Identity.Name;
            bool Admin = Auth.GetTicketData().Admin;
            string usuario = HttpContext.Current.User.Identity.Name;

            System.Web.HttpContext.Current.Session["Tarea_ejeintervencionid"] = data.ejeintervencionid;
            System.Web.HttpContext.Current.Session["Tarea_telecentroid"] = data.telecentroid;
            System.Web.HttpContext.Current.Session["Tarea_id"] = data.id;
            System.Web.HttpContext.Current.Session["Tarea_fechainicio"] = data.fechainicio;
            System.Web.HttpContext.Current.Session["Tarea_fechafin"] = data.fechafin;
            System.Web.HttpContext.Current.Session["Tarea_tarea"] = data.tarea;
            System.Web.HttpContext.Current.Session["Tarea_usuario"] = data.usuario;

            
            using (var db = new SMECEntities())
            {
                int tarea = Convert.ToInt32(data.tarea);
                var listado =  db.AvancePOTC
                    .Where(x
                        => (!data.id.HasValue || x.id == data.id.Value)
                        && (!data.fechainicio.HasValue || x.fechainicio >= data.fechainicio.Value)
                        && (!data.fechafin.HasValue || x.fechafin <= data.fechafin.Value)
                        && (!data.ejeintervencionid.HasValue || x.ejeintervencionid == data.ejeintervencionid)
                        && (!data.telecentroid.HasValue || x.telecentroid == data.telecentroid)
                        ////&& (string.IsNullOrEmpty(data.tarea) || x.PlanOperativoTareaTC.codigo == data.tarea)
                        && (string.IsNullOrEmpty(data.tarea) || x.PlanOperativoTareaTC.id == tarea)
                        && (string.IsNullOrEmpty(data.usuario) || x.aud_usuarioreg == data.usuario)
                        && (x.PlanOperativoTareaTC.PlanOperativoTC.ResultadoTC.marcoid== data.marcoid)
                        //&& (Admin || (x.telecentroid == telecentroid || x.telecentroinvitadoid == telecentroid))
                        )
                    .Select(x => new
                    {
                        x.id,
                        ejeintervencion = db.EjeIntervencion.FirstOrDefault(e => e.id == x.ejeintervencionid).nombre,
                        x.telecentroid,
                        telecentro = db.ListaDetalle.FirstOrDefault(e => e.listaid == 54 && e.codigo == x.telecentroid).nombre,
                        tarea = x.PlanOperativoTareaTC.codigo + "-" + x.PlanOperativoTareaTC.tarea,
                        x.fechainicio,
                        x.fechafin,
                        x.fechaingreso,
                        archivosficha0 = db.AvancePOTC_Archivo.Where(a => a.ficha == 0 && a.avanceid == x.id).Any(),
                        archivosficha1 = db.AvancePOTC_Archivo.Where(a => a.ficha == 1 && a.avanceid == x.id).Any(),
                        archivosficha2 = db.AvancePOTC_Archivo.Where(a => a.ficha == 2 && a.avanceid == x.id).Any(),
                        archivosficha3 = db.AvancePOTC_Archivo.Where(a => a.ficha == 3 && a.avanceid == x.id).Any(),
                        archivosficha4 = db.AvancePOTC_Archivo.Where(a => a.ficha == 4 && a.avanceid == x.id).Any(),
                        archivosficha6 = db.AvancePOTC_Archivo.Where(a => a.ficha == 6 && a.avanceid == x.id).Any(),

                        x.PlanOperativoTareaTC.ficha1,
                        x.PlanOperativoTareaTC.ficha2,
                        x.PlanOperativoTareaTC.ficha3,
                        x.PlanOperativoTareaTC.ficha4,
                        x.PlanOperativoTareaTC.PlanOperativoTC.ResultadoTC.marcoid,
                        x.PlanOperativoTareaTC.ficha6,
                        x.aud_usuarioreg,
                        x.codigocapacitacion
                    })
                    .OrderByDescending(y => y.fechainicio)
                    .ToList();

                listado = listado.OrderBy(x => x.tarea).ThenBy(x => x.telecentro).ThenByDescending(x => x.fechainicio).ToList();


                return listado;

            }
        }
    }
}
