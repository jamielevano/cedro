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
    
    public partial class PlanOperativoMetaArchivo
    {
        public int id { get; set; }
        public int pometaid { get; set; }
        public byte[] archivo { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public System.DateTime fecha { get; set; }
    
        public virtual PlanOperativoMeta PlanOperativoMeta { get; set; }
    }
}
