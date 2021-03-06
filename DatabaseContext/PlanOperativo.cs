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
    
    public partial class PlanOperativo
    {
        public PlanOperativo()
        {
            this.PlanOperativoMeta = new HashSet<PlanOperativoMeta>();
        }
    
        public int id { get; set; }
        public int resultadoid { get; set; }
        public string codigo { get; set; }
        public string actividad { get; set; }
        public decimal presupuesto { get; set; }
        public Nullable<System.DateTime> periodoinicio { get; set; }
        public Nullable<System.DateTime> periodofin { get; set; }
        public string supuesto { get; set; }
        public bool aud_anulado { get; set; }
        public long aud_fechareg { get; set; }
        public string aud_usuarioreg { get; set; }
        public string aud_ipreg { get; set; }
        public long aud_fechamod { get; set; }
        public string aud_usuariomod { get; set; }
        public string aud_ipmod { get; set; }
    
        public virtual Resultado Resultado { get; set; }
        public virtual ICollection<PlanOperativoMeta> PlanOperativoMeta { get; set; }
    }
}
