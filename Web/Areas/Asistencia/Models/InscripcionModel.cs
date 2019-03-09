using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class InscripcionModel
    {
        public int id { get; set; }
        public  IEnumerable<Persona> persona { get; set; }
    }
}