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
    
    public partial class AvancePOTC_Ficha06Integrantes
    {
        public int id { get; set; }
        public int avanceid { get; set; }
        public Nullable<int> telecentroid { get; set; }
        public string nombres { get; set; }
        public string apellidopaterno { get; set; }
        public string apellidomaterno { get; set; }
        public int sexoid { get; set; }
        public string dni { get; set; }
        public Nullable<System.DateTime> fechanacimiento { get; set; }
        public Nullable<int> edad { get; set; }
        public Nullable<int> ocupacionid { get; set; }
        public string ocupacionotro { get; set; }
        public string localidad { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string telecentro { get; set; }
    
        public virtual AvancePOTC_Ficha06 AvancePOTC_Ficha06 { get; set; }
    }
}
