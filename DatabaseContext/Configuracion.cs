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
    
    public partial class Configuracion
    {
        public Configuracion()
        {
            this.Inscripcion = new HashSet<Inscripcion>();
            this.Modulos = new HashSet<Modulos>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> fechainicio { get; set; }
        public Nullable<System.DateTime> fechafin { get; set; }
        public string nombre { get; set; }
        public int ejeintervencionid { get; set; }
        public int telecentroid { get; set; }
        public Nullable<int> programaid { get; set; }
        public Nullable<int> nivelid { get; set; }
        public Nullable<int> capacitadorid { get; set; }
        public int tipoid { get; set; }
        public Nullable<bool> cerrado { get; set; }
        public Nullable<System.DateTime> fechacierre { get; set; }
        public Nullable<int> hombres { get; set; }
        public Nullable<int> mujeres { get; set; }
        public Nullable<int> productor { get; set; }
        public Nullable<int> noproductor { get; set; }
        public Nullable<int> rango1 { get; set; }
        public Nullable<int> rango2 { get; set; }
        public Nullable<int> rango3 { get; set; }
        public Nullable<int> rango4 { get; set; }
        public Nullable<int> rango1b { get; set; }
        public Nullable<int> rango2b { get; set; }
        public Nullable<int> rango3b { get; set; }
        public Nullable<int> rango4b { get; set; }
        public Nullable<int> aprobados { get; set; }
        public Nullable<int> desaprobados { get; set; }
        public Nullable<int> retirados { get; set; }
        public Nullable<int> horas { get; set; }
        public string localidad { get; set; }
        public bool aud_anulado { get; set; }
        public long aud_fechareg { get; set; }
        public string aud_usuarioreg { get; set; }
        public string aud_ipreg { get; set; }
        public long aud_fechamod { get; set; }
        public string aud_usuariomod { get; set; }
        public string aud_ipmod { get; set; }
	public Nullable<int> organizacion { get; set; }
	public string organizacionotro { get; set; }
        public Nullable<int> participaid { get; set; }


        public virtual ICollection<Inscripcion> Inscripcion { get; set; }
        public virtual ICollection<Modulos> Modulos { get; set; }
    }
}
