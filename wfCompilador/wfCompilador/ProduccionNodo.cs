using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfCompilador
{
    [Serializable]
    public class ProduccionNodo
    {
        public string padre { get; set; }
        public string hijo { get; set; }
        public List<string> lookahead { get; set; }

        public bool Existe(ProduccionNodo nodo, string dato)
        {
            if (nodo.lookahead != null)
            {
                for (int i = 0; i < nodo.lookahead.Count; i++)
                {
                    if (nodo.lookahead[i].Equals(dato))
                        return true;
                }
            }
            return false;
        }
    }
}
