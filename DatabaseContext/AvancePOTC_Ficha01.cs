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
    
    public partial class AvancePOTC_Ficha01
    {
        public int avanceid { get; set; }
        public Nullable<bool> tipodocumento01 { get; set; }
        public Nullable<bool> tipodocumento02 { get; set; }
        public Nullable<bool> tipodocumento03 { get; set; }
        public Nullable<bool> tipodocumento04 { get; set; }
        public Nullable<bool> tipodocumento05 { get; set; }
        public Nullable<bool> tipodocumento06 { get; set; }
        public Nullable<bool> tipodocumento07 { get; set; }
        public string tipodocumentootro { get; set; }
        public string titulo { get; set; }
        public string tema { get; set; }
        public Nullable<bool> versiones01 { get; set; }
        public Nullable<bool> versiones02 { get; set; }
        public Nullable<bool> versiones03 { get; set; }
        public string versionesotro { get; set; }
        public Nullable<System.DateTime> fechaalmacenamiento { get; set; }
        public string poblacion { get; set; }
        public Nullable<bool> elaborado01 { get; set; }
        public Nullable<bool> elaborado02 { get; set; }
        public string autor { get; set; }
        public string aprobado { get; set; }
    
        public virtual AvancePOTC AvancePOTC { get; set; }
    }
}