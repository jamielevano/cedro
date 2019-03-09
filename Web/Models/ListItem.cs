using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ListItem
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
    }

    public class ListItemString
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }
}