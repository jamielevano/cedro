using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers.Api
{
    public class UbigeoController : ApiController
    {
        [HttpGet]
        public IEnumerable<object> Departamento()
        {
            using (var db = new SMECEntities())
            {
                return db.Ubigeo
                    .Where(x => x.provincia == "00" && x.activo)
                    .Select(x => new
                    {
                        id = x.departamento,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<object> Provincia(string d)
        {
            using (var db = new SMECEntities())
            {
                return db.Ubigeo
                    .Where(x => x.departamento == d && x.provincia != "00" && x.distrito == "00")
                    .Select(x => new
                    {
                        id = x.provincia,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }

        [HttpGet]
        public IEnumerable<object> Distrito(string d, string p)
        {
            using (var db = new SMECEntities())
            {
                return db.Ubigeo
                    .Where(x => x.departamento == d && x.provincia == p && x.distrito != "00")
                    .Select(x => new
                    {
                        id = x.distrito,
                        nombre = x.nombre
                    })
                    .ToList();
            }
        }
    }
}
