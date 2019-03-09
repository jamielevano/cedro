using System.Collections.Generic;

namespace Repositorio
{
    public class ModuloResponse
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public byte estado { get; set; }
        public byte orden { get; set; }
        public string manual { get; set; }
        public IEnumerable<SubModuloResponse> SubModuloResponse { get; set; }
    }

    public class SubModuloResponse
    {
        public int id { get; set; }
        public int moduloid { get; set; }
        public string descripcion { get; set; }
        public byte estado { get; set; }
        public byte orden { get; set; }
        public IEnumerable<PaginaResponse> PaginaResponse { get; set; }
    }

    public partial class PaginaResponse
    {
        public int id { get; set; }
        public int submoduloid { get; set; }
        public string descripcion { get; set; }
        public byte estado { get; set; }
        public byte orden { get; set; }
        public string url { get; set; }
        public string img { get; set; }
        public string main { get; set; }
    }
}