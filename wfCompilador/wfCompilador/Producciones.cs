using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

namespace wfCompilador
{
    [Serializable]
    class Producciones
    {
        List<Estado> _estados = new List<Estado> { };
        List<Produccion> _gramatica = new List<Produccion> { };
        int _contador_estado = 0;

        public Producciones(List<Produccion> gramatica)
        {
            /*
             * Se hace gramática aumentada y se recorren las producciones para crear los nodos y sus relaciones
             * se verifica estados iguales y se comunican si alguna regla genera una producción igual en a base a su lookhead
             * 
             * PD: el calculo del lookhead desde los first, me robo el sueño! - JGCA
             */
            _GramaticaAumentada(gramatica);
            _RecorreGramatica();
        }

        void _GramaticaAumentada(List<Produccion> gramatica)
        {
            string inicial = gramatica[0].Produccion_Valor;
            gramatica.Insert(0, new Produccion() { Siguiente_Estado = -1, Produccion_Valor = inicial + "'", Produccion_Detalle = new List<ProduccionDetalle> { }, lookahead = new List<string> { "$" } });
            gramatica[0].Produccion_Detalle.Add(new ProduccionDetalle() { Puntito = true, Produccion_Valor = inicial, Terminal = false, Final = false });
            int index_produccion = 0;
            foreach (Produccion produccion in gramatica)
            {
                foreach (ProduccionDetalle detalle in produccion.Produccion_Detalle)
                {
                    detalle.Puntito = true;
                    break;
                }
                produccion.no_produccion = index_produccion;
                produccion.Produccion_Detalle.Add(new ProduccionDetalle() { Puntito = false, Produccion_Valor = "", Terminal = false, Final = true });
                index_produccion++;
            }
            this._gramatica = DeepClone(gramatica);
            this._estados.Add(new Estado() { NoEstado = this._contador_estado, analizado = false, Produccion_Lista = DeepClone(gramatica) });
        }

        void _RecorreGramatica()
        {
            int index_estado = 0;
            // Recorre cada Estado
            List<Estado> temp_estados = new List<Estado>(this._estados);
            foreach (Estado estado in temp_estados)
            {
                //if (!estado.analizado)
                //{
                    this._estados[index_estado].analizado = true;
                    estado.analizado = true;
                    int index_produccion = 0;
                    _LookHead(index_estado);
                    // se obtine el lado izquierdo de cada producción
                    foreach (Produccion produccion in estado.Produccion_Lista)
                    {
                        Produccion temp_produccion = new Produccion();
                        temp_produccion = DeepClone(produccion);
                        int sub_index = 0;
                        // se obtiene el lado derecho de cada produccion
                        foreach (ProduccionDetalle detalle in temp_produccion.Produccion_Detalle)
                        {
                            if (detalle.Puntito)
                            {
                                _NuevoEstado(produccion, temp_produccion, DeepClone(sub_index), index_produccion, DeepClone(index_estado));
                                break;
                            }
                            sub_index++;
                        }
                        index_produccion++;
                    }
                //}
                index_estado++;
            }
            if (_recorrer_gramatica())
            {
                this._RecorreGramatica();
            }
        }

        void _NuevoEstado(Produccion produccion, Produccion produccion_copy, int index_detalle, int index_produccion, int index_estado)
        {
            string valor_produccion = "";
            if (!produccion_copy.Produccion_Detalle[index_detalle].Final)
            {
                produccion_copy.Produccion_Detalle[index_detalle].Puntito = false;
                produccion_copy.Produccion_Detalle[index_detalle + 1].Puntito = true;
                valor_produccion = produccion_copy.Produccion_Detalle[index_detalle + 1].Produccion_Valor;

                if (!produccion_copy.Produccion_Detalle[index_detalle + 1].Terminal)
                {
                    this._contador_estado++;

                    produccion.Siguiente_Estado = this._contador_estado;
                    List<Produccion> temp_producciones = _ListaProducciones(valor_produccion);
                    if (temp_producciones.Count > 0)
                    {
                        temp_producciones[0].lookahead = produccion.lookahead;
                    }
                    Estado estado = new Estado() { NoEstado = this._contador_estado, analizado = false, Produccion_Lista = new List<Produccion> { DeepClone(produccion_copy) } };
                    foreach (Produccion result in temp_producciones)
                    {
                        estado.Produccion_Lista.Add(result);
                    }

                    // buscamos un estado ya creado que sea igual
                    int estado_siguiente = ExisteEstado(estado.Produccion_Lista, valor_produccion);

                    if (estado_siguiente > 0)
                    {
                        _estados[index_estado].Produccion_Lista[index_produccion].Siguiente_Estado = DeepClone(estado_siguiente);
                        this._contador_estado = this._contador_estado - 1;
                    }
                    else
                    {
                        this._estados.Add(estado);
                    }
                }
            }
        }

        int ExisteEstado(List<Produccion> nueva_produccion, string valor_produccion)
        {
            int result = -1;
            foreach (Estado estado in _estados)
            {
                if ((estado.Produccion_Lista.Count == nueva_produccion.Count))
                {
                    for (int i = 0; i < estado.Produccion_Lista.Count; i++)
                    {
                        if (estado.Produccion_Lista[i].Produccion_Valor.Equals(nueva_produccion[i].Produccion_Valor))
                        {
                            if (estado.Produccion_Lista[i].Produccion_Detalle.Count == nueva_produccion[i].Produccion_Detalle.Count)
                            {
                                bool encontrado = false;
                                for (int j = 0; j < nueva_produccion[i].Produccion_Detalle.Count; j++)
                                {
                                    string valor = estado.Produccion_Lista[i].Produccion_Detalle[j].Produccion_Valor;
                                    bool puntito = estado.Produccion_Lista[i].Produccion_Detalle[j].Puntito;
                                    List<string> lookahead = estado.Produccion_Lista[0].lookahead;

                                    string valor_compara = nueva_produccion[i].Produccion_Detalle[j].Produccion_Valor;
                                    bool puntito_compara = nueva_produccion[i].Produccion_Detalle[j].Puntito;
                                    List<string> lookahead_compara = nueva_produccion[0].lookahead;


                                    if (valor.Equals(valor_compara))
                                    {
                                        encontrado = true;
                                    }

                                    if (puntito == puntito_compara)
                                    {
                                        encontrado = true;
                                    }

                                    if (encontrado)
                                    {
                                        // valido si lookahead es igual en ambos

                                        if (lookahead_compara.Count == lookahead.Count)
                                        {
                                            for (int index_lookahead = 0; index_lookahead < lookahead.Count; index_lookahead++)
                                            {
                                                if (lookahead[index_lookahead].Equals(lookahead_compara[index_lookahead]))
                                                {
                                                    encontrado = true;
                                                }
                                                else
                                                {
                                                    encontrado = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            encontrado = false;
                                        }

                                    }

                                    if (j == (nueva_produccion[i].Produccion_Detalle.Count - 1) && encontrado)
                                    {
                                        result = estado.NoEstado;
                                        break;
                                    }
                                }
                            }
                            else
                                break;

                        }
                        else
                            break;
                    }

                }
            }
            return result;
        }

        List<Produccion> _ListaProducciones(string valor_produccion)
        {
            // devuelve lista de producciones
            List<Produccion> result = new List<Produccion> { };
            string valor = valor_produccion;
            //    bool salir = false;
            //    while (!salir)
            //    {
            foreach (Produccion produccion in _gramatica)
            {
                if (valor.Equals(produccion.Produccion_Valor))
                {
                    result.Add(DeepClone(produccion));
                }
            }
            //   }
            return DeepClone(result);
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        void _LookHead(int numero_estado)
        {
            Estado estado = _estados[numero_estado];
            string produccion_padre = "";
            List<string> lookhead_heredado = new List<string> { };
            List<ProduccionNodo> producciones_a_obtener_lookhead = new List<ProduccionNodo> { };
            for (int j = 0; j < estado.Produccion_Lista.Count; j++)
            {
                if (j == 0)
                {
                    lookhead_heredado = DeepClone(estado.Produccion_Lista[0].lookahead);

                    if (estado.Produccion_Lista[j].Produccion_Detalle.Count > 0)
                    {
                        for (int index = 0; index < estado.Produccion_Lista[j].Produccion_Detalle.Count; index++)
                        {
                            if (estado.Produccion_Lista[j].Produccion_Detalle[index].Puntito
                                && !estado.Produccion_Lista[j].Produccion_Detalle[index].Terminal)
                            {
                                produccion_padre = DeepClone(estado.Produccion_Lista[j].Produccion_Detalle[index].Produccion_Valor);
                            }
                        }
                    }
                }
                else if (produccion_padre.Equals(estado.Produccion_Lista[j].Produccion_Valor) && produccion_padre.Length > 0)
                {
                    for (int i = 0; i < estado.Produccion_Lista[j].Produccion_Detalle.Count; i++)
                    {

                        ProduccionNodo nodo = new ProduccionNodo();
                        ProduccionDetalle detalle = estado.Produccion_Lista[j].Produccion_Detalle[i];
                        if (detalle.Puntito && !detalle.Final)
                        {

                            nodo.padre = estado.Produccion_Lista[j].Produccion_Detalle[i + 1].Produccion_Valor;
                            nodo.hijo = "";

                            if (!estado.Produccion_Lista[j].Produccion_Detalle[i + 1].Terminal)
                            {
                                if (!nodo.Existe(nodo, nodo.padre))
                                    nodo.lookahead = _First(nodo.padre);
                            }
                            if (nodo.lookahead.Count > 0)
                                producciones_a_obtener_lookhead.Add(nodo);
                        }
                    }
                    _estados[numero_estado].Produccion_Lista[j].lookahead = lookhead_heredado;
                    estado.Produccion_Lista[j].lookahead = lookhead_heredado;
                }
                else
                {
                    for (int aa = 0; aa < estado.Produccion_Lista[j].Produccion_Detalle.Count; aa++)
                    {
                        ProduccionNodo nodo = new ProduccionNodo();
                        ProduccionDetalle detalle = estado.Produccion_Lista[j].Produccion_Detalle[aa];
                        if (detalle.Puntito && !detalle.Final)
                        {
                            nodo.padre = estado.Produccion_Lista[j].Produccion_Detalle[aa + 1].Produccion_Valor;
                            nodo.hijo = "";
                            if (!nodo.Existe(nodo, nodo.padre))
                                nodo.lookahead = _First(nodo.padre);

                            if (nodo.lookahead.Count > 0)
                                producciones_a_obtener_lookhead.Add(nodo);

                        }
                    }
                    string valor_produccion_padre = estado.Produccion_Lista[j].Produccion_Valor;
                    for (int z = 0; z < producciones_a_obtener_lookhead.Count; z++)
                    {
                        if (producciones_a_obtener_lookhead[z].padre.Equals(valor_produccion_padre))
                        {
                            _estados[numero_estado].Produccion_Lista[j].lookahead = producciones_a_obtener_lookhead[z].lookahead;
                            estado.Produccion_Lista[j].lookahead = producciones_a_obtener_lookhead[z].lookahead;
                        }
                    }
                }
            }

        }

        List<string> _First(string valor_detalle)
        {
            List<string> first = new List<string> { };
            if (valor_detalle.Trim().Length == 0)
            {
                return first;
            }
            else
            {
                List<Produccion> producciones = _ListaProducciones(valor_detalle);
                foreach (Produccion produccion in producciones)
                {
                    foreach (ProduccionDetalle detalle in produccion.Produccion_Detalle)
                    {
                        string valor = detalle.Produccion_Valor;
                        bool puntito = detalle.Puntito;
                        bool terminal = detalle.Terminal;
                        bool final = detalle.Final;

                        if (!final && puntito && !valor.Equals(valor_detalle))
                        {
                            if (!terminal)
                            {
                                List<string> first_or = new List<string> { };
                                first_or = _First(valor);
                                foreach (string terminal_or in first_or)
                                {
                                    first.Add(terminal_or);
                                }
                            }
                            else
                            {
                                // Es un no terminal, formará parte de lookhead
                                first.Add(valor);
                            }
                        }
                    }
                }
                return first;
            }
        }

        bool _recorrer_gramatica()
        {
            bool result = false;
            // se verifica que no relación entre nodos
            foreach (Estado estado in _estados)
            { 
                if(estado.Produccion_Lista.Count > 1)
                {
                    if(estado.Produccion_Lista[1].Siguiente_Estado < 1)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public List<TablaParcer> _TablaParser()
        {
            List<TablaParcer> tabla_parcer = new List<TablaParcer> { };
            foreach (Estado estado in _estados)
            {
                foreach (Produccion produccion in estado.Produccion_Lista)
                {
                    int estado_nodo = estado.NoEstado;
                    bool es_terminal = false;
                    string valor_puntito = "";
                    int regla = 0;
                    string accion = "";
                    string produccion_lookahead = "";
                    for(int index_detalle = 0; index_detalle < produccion.Produccion_Detalle.Count; index_detalle ++)
                    {
                        ProduccionDetalle detalle = produccion.Produccion_Detalle[index_detalle];
                        if (!detalle.Final)
                        {
                            if (detalle.Puntito)
                            {
                                valor_puntito = detalle.Produccion_Valor;
                                es_terminal = detalle.Terminal;
                            }
                        }else if (detalle.Puntito)
                        {
                            foreach (String lookaheed in produccion.lookahead)
                            {
                                if(produccion.Siguiente_Estado < 0)
                                    tabla_parcer.Add(new TablaParcer() { estado = estado.NoEstado, valor_puntito = lookaheed, accion = "ACCEPT", regla = produccion.no_produccion, terminal = es_terminal, descripcion = produccion_lookahead });
                                else
                                tabla_parcer.Add(new TablaParcer() { estado = estado.NoEstado, valor_puntito = lookaheed, accion = "REDUCE", regla = produccion.no_produccion, terminal = es_terminal, descripcion = produccion_lookahead });
                            }
                        }
                    }
                    foreach (String lookaheed in produccion.lookahead)
                    {
                        produccion_lookahead = produccion_lookahead + lookaheed;
                    }
                    string describe = "(I" + estado.NoEstado.ToString() + ", " + valor_puntito + ") =";

                        regla = produccion.Siguiente_Estado;
                        if (regla == 0)
                            accion = "ACCEPT";
                        else
                        {
                            describe = describe + "REDUCE " + regla.ToString();
                            accion = "REDUCE";
                        }
                    
                    if (es_terminal)
                    {
                        accion = "SHIFT";
                        regla = produccion.Siguiente_Estado;
                        describe = describe + "SHIFT " + regla.ToString();
                    }
                    else
                    {
                        accion = "GOTO";
                        regla = produccion.Siguiente_Estado;
                        describe = describe + "GOTO " + regla.ToString();
                    }
                    if(valor_puntito.Length > 0)
                    tabla_parcer.Add(new TablaParcer() { estado = estado.NoEstado, valor_puntito = valor_puntito, accion = accion, regla = regla, terminal = es_terminal, descripcion = produccion_lookahead});
                }
            }
            return tabla_parcer;
        }

        public List<string> _Imprimr()
        {
            string token = "";
            string valor = "";
            string valor_puntito = "";
            List<string> result = new List<string> { };
            List<TablaParcer> tabla_parcer = new List<TablaParcer> { };
            result.Add("ESTADOS");
            foreach (Estado estado in _estados)
            {
                result.Add("I" + estado.NoEstado.ToString());

                foreach (Produccion produccion in estado.Produccion_Lista)
                {
                    string temp = "";
                    string temp_lookheed = "";
                    string puntito;
                    token = produccion.Produccion_Valor;
                    temp = token + " -> ";

                    foreach (ProduccionDetalle detalle in produccion.Produccion_Detalle)
                    {
                        puntito = "";
                        valor = detalle.Produccion_Valor;
                        if (detalle.Puntito)
                        {
                            valor_puntito = detalle.Produccion_Valor;
                            puntito = ((char)183).ToString();
                        }
                        temp = temp + " " + puntito + valor; ;
                    }

                    foreach (String lookheed in produccion.lookahead)
                    {
                        temp_lookheed = temp_lookheed + lookheed;
                    }
                    temp = temp + ", " + temp_lookheed + " (I" + estado.NoEstado.ToString() + ", " + valor_puntito + ") = I" + produccion.Siguiente_Estado.ToString();

                    result.Add(temp);
                    result.Add("");
                }
            }
            return result;
        }

        public List<Produccion> GetGramatica()
        {
            return this._gramatica;
        }

        public string buscarPadre(string estado_actual)
        {
            string result = "";
            int numero_produccion = int.Parse(estado_actual.Replace("R", ""));
            result = _gramatica[numero_produccion].Produccion_Valor;

            return result;
        }

        public int buscarPop(string estado_actual)
        {
            int result = 0;
            int numero_produccion = int.Parse(estado_actual.Replace("R", ""));
            result = _gramatica[numero_produccion].Produccion_Detalle.Count -1;
            
            return result;
        }

        public string buscarValor(string cadena_lectura, string estado_actual)
        {
            string result = "";
            int estado = int.Parse(estado_actual.Replace("I", ""));

            List<TablaParcer> tabla_parcer = _TablaParser();
            for (int i = 0; i < tabla_parcer.Count; i++)
            {
                if(tabla_parcer[i].estado == estado && cadena_lectura.EndsWith(tabla_parcer[i].valor_puntito))
                {
                    if(tabla_parcer[i].accion.Equals("GOTO"))
                        result = tabla_parcer[i].regla.ToString();
                    else if(tabla_parcer[i].accion.Equals("SHIFT"))
                        result = "S" + tabla_parcer[i].regla.ToString();
                    else if (tabla_parcer[i].accion.Equals("REDUCE"))
                        result = "R" + tabla_parcer[i].regla.ToString();
                    else if (tabla_parcer[i].accion.Equals("ACCEPT"))
                        result = "Accept";
                }
            }
            return result;
        }

    }
}
