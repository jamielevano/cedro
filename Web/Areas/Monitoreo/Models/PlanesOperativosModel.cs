using DatabaseContext;
using System.Collections.Generic;

namespace Web.Areas.Monitoreo.Models
{
    public class PlanesOperativosModel
    {
        public ICollection<PlanOperativo> PlanOperativo { get; set; }
    }

    public class PlanesOperativoMetasModel
    {
        public ICollection<PlanOperativoMeta> PlanOperativoMeta { get; set; }
    }

    public class ResultadosModel
    {
        public ICollection<Resultado> Resultado { get; set; }
    }

    public class PropositoMetasModel
    {
        public ICollection<PropositoMetas> PropositoMetas { get; set; }
    }

    
}
