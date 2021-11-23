using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfCompilador
{
    [Serializable]
    class Estado
    {
        public bool analizado { get; set; }
        public int NoEstado { get; set; }
        public List<Produccion> Produccion_Lista { get; set; }
    }
}
