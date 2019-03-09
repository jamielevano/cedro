using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DatabaseContext;
using Web.Models;

namespace Web.Controllers.Api
{
    public class GeneralController : ApiController
    {
        private IEnumerable<ListItem> ListaGeneral(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == id && x.activo == 1)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .OrderBy(o=>o.id)
                    .ToList();
            }
        }

        private IEnumerable<ListItem> ListaGeneral(int id, int idcodigo)
        {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == id && x.activo == 1 && x.codigo == idcodigo)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .OrderBy(o => o.id)
                    .ToList();
            }
        }

        private IEnumerable<ListItem> ListaGeneralRelacion(int id, int relacionid)
        {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == id && x.activo == 1 && x.relacionid == relacionid)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .OrderBy(o => o.id)
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> UnidadMedida() { return ListaGeneral(1); }

        [HttpGet]
        public IEnumerable<ListItem> Sector() { return ListaGeneral(3); }

        [HttpGet]
        public IEnumerable<ListItem> Poblacion() { return ListaGeneral(4); }

        [HttpGet]
        public IEnumerable<ListItem> Tipodocumento() { return ListaGeneral(7); }

        [HttpGet]
        public IEnumerable<ListItem> Elaborado() { return ListaGeneral(9); }

        [HttpGet]
        public IEnumerable<ListItem> Servicios() { return ListaGeneral(11); }

        [HttpGet]
        public IEnumerable<ListItem> HabilitacionFisica() { return ListaGeneral(12); }

        [HttpGet]
        public IEnumerable<ListItem> ProveedorServicio() { return ListaGeneral(16); }

        [HttpGet]
        public IEnumerable<ListItem> Coordinacion() { return ListaGeneral(17); }

        [HttpGet]
        public IEnumerable<ListItem> ServiciosRed() { return ListaGeneral(18); }

        [HttpGet]
        public IEnumerable<ListItem> ActividadesRed() { return ListaGeneral(19); }

        [HttpGet]
        public IEnumerable<ListItem> TipoDifusion() { return ListaGeneral(20); }

        [HttpGet]
        public IEnumerable<ListItem> TiposCuenta() { return ListaGeneral(23); }

        [HttpGet]
        public IEnumerable<ListItem> MedioDifusion() { return ListaGeneral(25); }

        [HttpGet]
        public IEnumerable<ListItem> Nivel() { return ListaGeneral(26); }
        public IEnumerable<ListItem> Curso() { return ListaGeneral(56); }

        [HttpGet]
        public IEnumerable<ListItem> Certificado() { return ListaGeneral(27); }

        [HttpGet]
        public IEnumerable<ListItem> Evaluacion() { return ListaGeneral(28); }

        [HttpGet]
        public IEnumerable<ListItem> EspacioDifusion() { return ListaGeneral(29); }

        [HttpGet]
        public IEnumerable<ListItem> TipoDifusionWeb() { return ListaGeneral(30); }

        [HttpGet]
        public IEnumerable<ListItem> FormaTrabajo() { return ListaGeneral(31); }

        [HttpGet]
        public IEnumerable<ListItem> TipoReunion() { return ListaGeneral(53); }

        [HttpGet]
        public IEnumerable<ListItem> Region() { return ListaGeneral(1054); }

        [HttpGet]
        public IEnumerable<ListItem> Telecentros() 
        { 
            
            return ListaGeneral(54); 
        
        }
        //cesar
        public IEnumerable<ListItem> localidad22()
        {

            return ListaGeneral(54);

        }


        [HttpGet]
        public IEnumerable<ListItem> TelecentrosFilter()
        {
            using (var db = new SMECEntities())
            {
                var admin = Auth.GetTicketData().Admin;;
                var coordinador = Auth.GetTicketData().Coordinador;
                var telecentro = Auth.GetTicketData().TelecentroId;
                var eje = Auth.GetTicketData().EjeId;

                if (admin)
                    return ListaGeneral(33);
                else
                {

                    if (coordinador)
                        return ListaGeneralRelacion(33, eje);
                    else
                        return ListaGeneral(33, telecentro);
                }
                    
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> ProblemasRegion() { return ListaGeneral(34); }

        [HttpGet]
        public IEnumerable<ListItem> UnidadMasaProduccion() { return ListaGeneral(35); }

        [HttpGet]
        public IEnumerable<ListItem> UnidadMasaVenta() { return ListaGeneral(36); }

        [HttpGet]
        public IEnumerable<ListItem> Anios() {

            List<ListItem> lista = new List<ListItem>();
            var anioact = System.DateTime.Now.Year;
            var aniomin = anioact -10;

            for (int i = anioact; i > aniomin; i--)
            {
                lista.Add(new ListItem { id=i, nombre=i.ToString() });
            }

            return lista.ToList();
        }
        
        [HttpGet]
        public IEnumerable<ListItem> Meses() { return ListaGeneral(37); }

        [HttpGet]
        public IEnumerable<ListItem> Programa() { return ListaGeneral(55); }

        [HttpGet]
        public IEnumerable<ListItem> Modulos(int nivelid) {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 56 && x.activo == 1 && x.codigo == nivelid)  // ceasr 39 x 56
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }
        public IEnumerable<ListItem> Cursos(int nivelid)
        {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 56 && x.activo == 1 && x.relacionid == nivelid)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        //public IEnumerable<ListItem> Sesion() { return ListaGeneral(40); } //cesar -->se elimino para crear uno nuevo 
        public IEnumerable<ListItem> Sesion() {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 40 && x.activo == 1 && x.codigo == 9)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        //public IEnumerable<ListItem> Clase() { return ListaGeneral(41); }
        public IEnumerable<ListItem> Clase() {
            using (var db = new SMECEntities())
                return db.ListaDetalle
                    .Where(x => x.listaid == 41 && x.activo == 1 && x.codigo == 1)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
        }

        [HttpGet]
        public IEnumerable<ListItem> Sexo() { return ListaGeneral(42); }

        [HttpGet]
        public IEnumerable<ListItem> Ocupacion() { return ListaGeneral(43); }

        public IEnumerable<ListItem> Ocupacion_GDA()
        {

            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 43 && x.activo == 1 && x.relacionid == 2)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }

        }
        [HttpGet]
        public string Ocupacion_Descripcion(int id) { 
            var lista = ListaGeneral(43);
            return lista.Where(x => x.id == id).FirstOrDefault().nombre.ToString();
        }

        [HttpGet]
        public IEnumerable<ListItem> UsoEquipo() { return ListaGeneral(44); }

        [HttpGet]
        public IEnumerable<ListItem> CondicionFinal(int programa) {

            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 45 && x.activo == 1 && x.relacionid2 == programa)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }
            
        }

        [HttpGet]
        public IEnumerable<ListItem> GradoInstruccion() { return ListaGeneral(46); }


        [HttpGet]
        public IEnumerable<ListItem> EstadoCivil() { return ListaGeneral(48); }

        [HttpGet]
        public IEnumerable<ListItem> TipoComunidad() { return ListaGeneral(50); }

        [HttpGet]
        public IEnumerable<ListItem> EjeIntervencion02() { return ListaGeneral(51); }

        [HttpGet]
        public IEnumerable<ListItem> Localidad() { return ListaGeneral(54); }

        [HttpGet]
        public IEnumerable<ListItem> Organizacion() { return ListaGeneral(57); }


        [HttpGet]
        public IEnumerable<ListItem> NivelPrograma(int programa) {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 56 && x.activo == 1 && x.relacionid == programa)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> EjeIntervencion()
        {
            using (var db = new SMECEntities())
            {
                return db.EjeIntervencion
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> Eje_Telecentros(int eje)
        {
            using (var db = new SMECEntities())
            {
                return db.ListaDetalle
                    .Where(x => x.listaid == 54 && x.activo == 1 && x.relacionid == eje)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> Telecentros_Region(int telecentro)
        {
            

            using (var db = new SMECEntities())
            {

                var relacion2 = db.ListaDetalle.FirstOrDefault(x => x.listaid == 54 && x.codigo == telecentro).relacionid2;

                var resultado=  db.ListaDetalle
                    .Where(x => x.listaid == 1054 && x.activo == 1 && x.codigo == relacion2)
                    .Select(x => new ListItem
                    {
                        id = x.codigo,
                        nombre = x.nombre
                    })
                    .ToList();

                return resultado;
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> Localidad(int? eje = null)
        {
            using (var db = new SMECEntities())
            {
                IQueryable<ListaDetalle> r = db.ListaDetalle;

                if (eje.HasValue) r = r.Where(x => x.listaid==33 && x.activo==1 && x.relacionid == eje);

                return r.Select(x => new ListItem
                {
                    id = x.id,
                    nombre = x.nombre
                })
                .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<ListItem> ListUsuarios()
        {
            using (var db = new SMECEntities())
            {
                return db.Usuario
                    .Where(x => x.nombre != null)
                    .Select(x => new ListItem
                    {
                        id = x.id,
                        nombre = x.nombre
                    })
                    .OrderBy(x=>x.nombre)
                    .ToList();
            }
        }


        [HttpGet]
        public int GetUsuarioIdByLogin()
        {
            int usuario = 0;

            using (var db = new SMECEntities())
            {
                usuario = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().id;

            }
            return usuario;
        }

        [HttpGet]
        public int GetTelecentroIdByLogin()
        {
            int telecentro = 0;

            using (var db = new SMECEntities())
            {
                telecentro = db.Usuario.Where(x => x.login == User.Identity.Name).FirstOrDefault().telecentro??0;
            }

            return telecentro;
        }

        
    }
}
