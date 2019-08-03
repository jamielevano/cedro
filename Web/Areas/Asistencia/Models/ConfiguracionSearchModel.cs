using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class ConfiguracionSearchModel
    {
        public int? id { get; set; }
        public int? ejeintervencionid { get; set; }//cesar
        public int? telecentroid { get; set; }
        public int tipoid { get; set; }
        public int? anioid { get; set; }
        public int? mesid { get; set; }
        public int? programaid { get; set; }
        public int? nivelid { get; set; }
        public int? moduloid { get; set; }
        public int? organizacion { get; set; }
        public int? capacitadorid { get; set; }

        public DateTime? fechaini { get; set; }
        public DateTime? fechafin { get; set; }
        public int? participaid { get; set; }        
    }
}