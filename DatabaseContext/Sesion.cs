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
    
    public partial class Sesion
    {
        public Sesion()
        {
            this.Clase = new HashSet<Clase>();
        }
    
        public int id { get; set; }
        public int moduloid { get; set; }
        public int listaid { get; set; }
        public int orden { get; set; }
    
        public virtual Modulos Modulos { get; set; }
        public virtual ICollection<Clase> Clase { get; set; }
    }
}
