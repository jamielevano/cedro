using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Web.Models;

namespace Web.Areas.Planificacion.Controllers.Api
{
    public class PlanOperativoTareaMetaTCController : ApiController
    {

        [HttpGet]
        public object Find(int id)
        {
            using (var db = new SMECEntities())
            {
                return db.PlanOperativoTareaTC
                    .AsNoTracking()
                    .Include(x => x.PlanOperativoTareaMetaTC)
                    .Where(x => x.id == id)
                    .Select(x => new
                    {
                        x.id,
                        x.PlanOperativoTC.periodoinicio,
                        x.PlanOperativoTC.periodofin,
                        PlanOperativoTareaMetaTC = x.PlanOperativoTareaMetaTC.Select(y => new
                        {
                            y.id,
                            y.anio,
                            y.ejeintervencionid,
                            y.telecentro,
                            y.mes1,
                            y.mes2,
                            y.mes3,
                            y.mes4,
                            y.mes5,
                            y.mes6,
                            y.mes7,
                            y.mes8,
                            y.mes9,
                            y.mes10,
                            y.mes11,
                            y.mes12
                        }).OrderBy(o => o.anio)
                    })
                    .FirstOrDefault(); ;
            }
        }

        public void Save(PlanOperativoTareaTC item)
        {
            using (var db = new SMECEntities())
            {

                var _item = db.PlanOperativoTareaTC
                    .Include(x => x.PlanOperativoTareaMetaTC)
                    .SingleOrDefault(x => x.id == item.id);

                var deletedPlanOperativoMetaTC = item.PlanOperativoTareaMetaTC.Where(x => x.id != 0).Select(x => x.id).ToList();
                _item.PlanOperativoTareaMetaTC.Where(x => !deletedPlanOperativoMetaTC.Contains(x.id)).ToList().ForEach(pm =>
                {
                    db.PlanOperativoTareaMetaTC.Remove(pm);
                    _item.PlanOperativoTareaMetaTC.Remove(pm);
                });

                foreach (var pm in _item.PlanOperativoTareaMetaTC)
                {
                    var PlanOperativoMetaTC = item.PlanOperativoTareaMetaTC.First(x => pm.id == x.id);
                    pm.anio = PlanOperativoMetaTC.anio;
                    pm.ejeintervencionid = PlanOperativoMetaTC.ejeintervencionid;
                    pm.telecentro = PlanOperativoMetaTC.telecentro;
                    pm.mes1 = PlanOperativoMetaTC.mes1;
                    pm.mes2 = PlanOperativoMetaTC.mes2;
                    pm.mes3 = PlanOperativoMetaTC.mes3;
                    pm.mes4 = PlanOperativoMetaTC.mes4;
                    pm.mes5 = PlanOperativoMetaTC.mes5;
                    pm.mes6 = PlanOperativoMetaTC.mes6;
                    pm.mes7 = PlanOperativoMetaTC.mes7;
                    pm.mes8 = PlanOperativoMetaTC.mes8;
                    pm.mes9 = PlanOperativoMetaTC.mes9;
                    pm.mes10 = PlanOperativoMetaTC.mes10;
                    pm.mes11 = PlanOperativoMetaTC.mes11;
                    pm.mes12 = PlanOperativoMetaTC.mes12;
                }

                item.PlanOperativoTareaMetaTC.Where(x => x.id == 0).ToList().ForEach(pm =>
                {
                    _item.PlanOperativoTareaMetaTC.Add(pm);
                });
                

                db.SaveChanges();
            }
        }
    }
}
