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
    
    public partial class Participantes
    {
        public int id { get; set; }
        public int personaid { get; set; }
        public int claseid { get; set; }
        public Nullable<int> notaentrada { get; set; }
        public Nullable<int> notasalida { get; set; }

        public Nullable<int> p4a { get; set; }
        public Nullable<int> p4b { get; set; }
        public Nullable<int> p4c { get; set; }
        public Nullable<int> p4d { get; set; }
        public Nullable<int> p5 { get; set; }
        public Nullable<int> p6 { get; set; }
        public Nullable<int> p7 { get; set; }
        public Nullable<int> p8 { get; set; }
        public Nullable<int> p9 { get; set; }
        public Nullable<int> p10a { get; set; }
        public Nullable<int> p10b { get; set; }
        public Nullable<int> p10c { get; set; }
        public Nullable<int> p11a { get; set; }
        public Nullable<int> p11b { get; set; }
        public Nullable<int> p11c { get; set; }
        public Nullable<int> p12a { get; set; }
        public Nullable<int> p12b { get; set; }
        public Nullable<int> p12c { get; set; }
        public Nullable<int> p13 { get; set; }

        public virtual Clase Clase { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
