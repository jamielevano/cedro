using System;
namespace Web.Areas.Sistemas.Models
{
    public class UsuarioSearchModel
    {
        public int? telecentro { get; set; }
        public int? rol { get; set; }
        public string login { get; set; }
        public string cargo { get; set; }
    }
}