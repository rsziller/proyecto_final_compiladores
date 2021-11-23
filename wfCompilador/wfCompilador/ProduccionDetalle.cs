using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfCompilador
{
    [Serializable]
    public class ProduccionDetalle
    {
        public bool Puntito { get; set; }
        public bool Terminal { get; set; }
        public bool Final { get; set; }
        public string Produccion_Valor { get; set; }
    }
}
