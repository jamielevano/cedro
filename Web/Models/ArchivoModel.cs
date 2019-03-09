using DatabaseContext;
using System.Collections.Generic;
namespace Web.Models
{
    public class ArchivoModel
    {
        public int id { get; set; }
        public IEnumerator<object> archivos { get; set; }
    }
}