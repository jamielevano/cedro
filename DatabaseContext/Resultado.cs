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
    
    public partial class Resultado
    {
        public Resultado()
        {
            this.PlanOperativo = new HashSet<PlanOperativo>();
            this.ResultadoMetas = new HashSet<ResultadoMetas>();
        }
    
        public int id { get; set; }
        public int marcoid { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string supuesto { get; set; }
        public bool aud_anulado { get; set; }
        public long aud_fechareg { get; set; }
        public string aud_usuarioreg { get; set; }
        public string aud_ipreg { get; set; }
        public long aud_fechamod { get; set; }
        public string aud_usuariomod { get; set; }
        public string aud_ipmod { get; set; }
    
        public virtual MarcoLogico MarcoLogico { get; set; }
        public virtual ICollection<PlanOperativo> PlanOperativo { get; set; }
        public virtual ICollection<ResultadoMetas> ResultadoMetas { get; set; }
    }
}
