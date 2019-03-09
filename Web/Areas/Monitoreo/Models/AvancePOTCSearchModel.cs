using System;
namespace Web.Areas.Monitoreo.Models
{
    public class AvancePOTCSearchModel
    {
        public int? id { get; set; }
        public string usuario { get; set; }
        public DateTime? fechainicio { get; set; }
        public DateTime? fechafin { get; set; }
        public int? ejeintervencionid { get; set; }
        public int? telecentroid { get; set; }
        public int marcoid { get; set; }
        public string tarea { get; set; }
    }
}