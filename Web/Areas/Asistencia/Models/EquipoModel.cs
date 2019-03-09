using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class ImportarTelecentroModel
    {
        public int telecentro { get; set; }
        public int responsable { get; set; }

        public string fila { get; set; }
        public string fecha { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string sexo { get; set; }
        public string institucion { get; set; }
        public string localidad { get; set; }
        public string dni { get; set; }
        public string fechanac { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string actividad { get; set; }
        public string horaini { get; set; }
        public string horafin { get; set; }
    }
}