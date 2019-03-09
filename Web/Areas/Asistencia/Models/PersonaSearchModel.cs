using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class PersonaSearchModel
    {
        public int? telecentroid { get; set; }
        public string dni { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
    }

    public class PersonaSaveFotoModel
    {
        public int id { get; set; }
        public byte[] foto { get; set; }
    }
}