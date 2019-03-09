using DatabaseContext;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Web.Areas.Monitoreo.Models;

namespace Web.Areas.Monitoreo.Controllers.Api
{
    public class ActividadController : ApiController
    {
        public void Save(PlanesOperativosModel data)
        {
            using (var db = new SMECEntities())
            {
                foreach (var tmp in data.PlanOperativo)
                {
                    foreach (var item in tmp.PlanOperativoMeta)
                    {
                        var _item = db.PlanOperativoMeta
                            .Include(x => x.AvancePO)
                            .SingleOrDefault(x => x.id == item.id);

                        db.MergeCollections(item.AvancePO, _item.AvancePO, x => x.id, (x, _x) =>
                        {
                            _x.fechainicio = x.fechainicio;
                            _x.fechafin = x.fechafin;
                            _x.monto = x.monto;
                            _x.cantidad = x.cantidad;
                            _x.observacion = x.observacion;
                        });

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
