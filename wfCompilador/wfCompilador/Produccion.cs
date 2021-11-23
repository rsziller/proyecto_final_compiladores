using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfCompilador
{
    [Serializable]
    public class Produccion
    {
        public string Produccion_Valor { get; set; }
        public int Siguiente_Estado { get; set; }
        public List<ProduccionDetalle> Produccion_Detalle { get; set; }
        public List<string> lookahead { get; set; }
        public int no_produccion { get; set; }
        /*
        public bool Puntito { get; set; }
        public string Produccion_Valor { get; set; }
        public bool Terminal { get; set; }
        public bool Final { get; set; }
        public int Siguiente_Estado { get; set; }
        public List<Produccion> Produccion_Lista { get; set; }
        public List<string> Lookheed { get; set; }
        */
    }
}
