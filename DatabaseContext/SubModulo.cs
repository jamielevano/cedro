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
    
    public partial class SubModulo
    {
        public SubModulo()
        {
            this.Pagina = new HashSet<Pagina>();
        }
    
        public int id { get; set; }
        public int moduloid { get; set; }
        public string descripcion { get; set; }
        public byte estado { get; set; }
        public byte orden { get; set; }
        public bool aud_anulado { get; set; }
        public long aud_fechareg { get; set; }
        public string aud_usuarioreg { get; set; }
        public string aud_ipreg { get; set; }
        public long aud_fechamod { get; set; }
        public string aud_usuariomod { get; set; }
        public string aud_ipmod { get; set; }
    
        public virtual Modulo Modulo { get; set; }
        public virtual ICollection<Pagina> Pagina { get; set; }
    }
}
