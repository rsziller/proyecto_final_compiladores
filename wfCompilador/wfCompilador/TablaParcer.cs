using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfCompilador
{
    public class TablaParcer
    {
        public int estado { get; set; }
        public string valor_puntito { get; set; }
        public string accion { get; set; }
        public int regla { get; set; }
        public bool terminal { get; set; }
        public string descripcion { get; set; }
    }
}
