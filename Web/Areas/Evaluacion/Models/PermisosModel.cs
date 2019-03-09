using DatabaseContext;
using System.Collections.Generic;

namespace Web.Areas.Evaluacion.Models
{
    public class RolesModel
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public IEnumerable<ModulosModel> Modulo { get; set; }
    }

    public class ModulosModel
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public IEnumerable<SubModulosModel> SubModulo { get; set; }
    }

    public class SubModulosModel
    {
        public int id { get; set; }
        public string descripcion { get; set; }        
        public IEnumerable<PaginasModel> Pagina { get; set; }
    }

    public partial class PaginasModel
    {
        
        public int id { get; set; }
        public bool acceder { get; set; }
        public bool agregar { get; set; }
        public bool modificar { get; set; }
        public bool eliminar { get; set; }
    }
}
