using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlytaloWPF
{
    
    public class Lokitieto
    {
        public string KeittioValo{ get; set; }
        public string OlohuoneValo { get; set; }
        public string TaloTermostaatti { get; set; }

        public string SaunaPaallaData { get; set; }

        public string SaunanLampotila { get; set; }

        public DateTime aika { get; set; }

        public Boolean AjastinPaallaTieto { get; set; }
    }
}
