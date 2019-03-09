using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Areas.Asistencia.Models
{
    public class InscripcionFinal
    {
        public int id { get; set; }
        public int condicionid { get; set; }
        public string observacion { get; set; }
        public decimal notafinal { get; set; }
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
}