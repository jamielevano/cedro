//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inscripcion
    {
        public int id { get; set; }
        public int configuracionid { get; set; }
        public int personaid { get; set; }
        public string horainicio { get; set; }
        public string horafin { get; set; }
        public Nullable<int> usoid { get; set; }
        public string usootro { get; set; }
        public Nullable<int> condicionid { get; set; }
        public Nullable<decimal> notafinal { get; set; }
        public string calificacion { get; set; }
        public Nullable<int> lineapre_1 { get; set; }
        public Nullable<int> lineapost_1 { get; set; }
        public Nullable<int> lineapre_2 { get; set; }
        public Nullable<int> lineapost_2 { get; set; }
        public Nullable<int> lineapre_3 { get; set; }
        public Nullable<int> lineapost_3 { get; set; }
        public Nullable<int> lineapre_4 { get; set; }
        public Nullable<int> lineapost_4 { get; set; }
        public string lineapre_5 { get; set; }
        public string lineapost_5 { get; set; }
        public Nullable<int> lineapre_6 { get; set; }
        public Nullable<int> lineapost_6 { get; set; }
        public Nullable<int> lineapre_7 { get; set; }
        public Nullable<int> lineapost_7 { get; set; }
        public Nullable<int> lineapre_8 { get; set; }
        public Nullable<int> lineapost_8 { get; set; }
        public string observacion { get; set; }
    
        public virtual Configuracion Configuracion { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
