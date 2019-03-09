using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class TimeAll { 
        public TimeSpan TheDuration { get; set; } 
    };

    public class ParticipanteModel
    {
        public int claseid { get; set; }
        public  IEnumerable<Persona> persona { get; set; }
       
    }

    public class ParticipanteNotaModel
    {
        public int id { get; set; }
        public int personaid { get; set; }
        public string horas { get; set; }
        public string apellidopaterno{ get; set; }
        public string apellidomaterno { get; set; }
        public string nombre { get; set; }
        public string dni{ get; set; }
        public DateTime? fechanacimiento{ get; set; }
        public int? edad { get; set; }
        public int? sexoid{ get; set; }
        public string localidad { get; set; }
        public string ocupacion { get; set; }
        public string telefono{ get; set; }
        public string correo { get; set; }
        public int? condicionid { get; set; }
        public string condicion { get; set; }
        public decimal? notafinal { get; set; }
        public int? lineapre_1 { get; set; }
        public int? lineapost_1 { get; set; }
        public int? lineapre_2 { get; set; }
        public int? lineapost_2 { get; set; }
        public int? lineapre_3 { get; set; }
        public int? lineapost_3 { get; set; }
        public int? lineapre_4 { get; set; }
        public int? lineapost_4 { get; set; }
        public string lineapre_5 { get; set; }
        public string lineapost_5 { get; set; }
        public int? lineapre_6 { get; set; }
        public int? lineapost_6 { get; set; }
        public int? lineapre_7 { get; set; }
        public int? lineapost_7 { get; set; }
        public int? lineapre_8 { get; set; }
        public int? lineapost_8 { get; set; }
    }


    public class PersonaModel
    {
        public int id { get; set; }
        public int personaid { get; set; }
        public string codigo { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public string nombre { get; set; }
        public string dni { get; set; }
        public DateTime? fecharegistro { get; set; }
        public DateTime? fechanacimiento { get; set; }
        public int? edad { get; set; }
        public int? sexoid { get; set; }
        public string sexo { get; set; }
        public string localidad { get; set; }
        public int? ocupacionid { get; set; }
        public string ocupacion { get; set; }
        public string lugarnacimiento { get; set; }

        public string telefono { get; set; }
        public string celular { get; set; }

        public bool estado { get; set; }
        public bool fotohash { get; set; }
        public long fotoLength { get; set; }

        public string fotosize { get; set; }
        public bool eliminable { get; set; }

        public int? telecentroid { get; set; }
        public string telecentro { get; set; }
        public int? ejeintervencionid { get; set; }
        public string ejeintervencion { get; set; }
        
    }
}