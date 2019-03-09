using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class EquipoModel
    {
        public int id { get; set; }
        public string horainicio { get; set; }
        public string horafin { get; set; }
        public int usoid { get; set; }
        public string usootro { get; set; }
    }
}