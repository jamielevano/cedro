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
    
    public partial class ListaDetalle
    {
        public int id { get; set; }
        public int listaid { get; set; }
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string nombrecorto { get; set; }
        public Nullable<int> headid { get; set; }
        public Nullable<int> relacionid { get; set; }
        public Nullable<int> headid2 { get; set; }
        public Nullable<int> relacionid2 { get; set; }
        public int activo { get; set; }
	public int unidadid { get; set; }
        public decimal meta1 { get; set; }
        public decimal avance1 { get; set; }
        public decimal meta2 { get; set; }
        public decimal avance2 { get; set; }
        public decimal meta3 { get; set; }
        public decimal avance3 { get; set; }
        public decimal meta4 { get; set; }
        public decimal avance4 { get; set; }
        public decimal meta5 { get; set; }
        public decimal avance5 { get; set; }
        public decimal metaprogramada { get; set; }
	public decimal avanceacumulado { get; set; }
	public decimal porcentajeavance { get; set; }
	public string codigo2 { get; set; }
        public bool aud_anulado { get; set; }
        public long aud_fechareg { get; set; }
        public string aud_usuarioreg { get; set; }
        public string aud_ipreg { get; set; }
        public long aud_fechamod { get; set; }
        public string aud_usuariomod { get; set; }
        public string aud_ipmod { get; set; }
    
        public virtual Lista Lista { get; set; }
    }
}