using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace wfCompilador
{
    public partial class Form1 : Form
    {
        Producciones produccionesGlobal;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog documento = new OpenFileDialog();
                documento.Title = "Abrir Gramática";

                if (documento.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(documento.FileName))
                    {
                        string direccion = documento.FileName;
                        txtDocumento.Text = documento.FileName;
                        StreamReader reader = new StreamReader(direccion, System.Text.Encoding.Default);
                        txtTexto.Text = reader.ReadToEnd();
                        reader.Close();
                    }
                }
            } catch (Exception)
            {
                MessageBox.Show("No se pudo abrir el documento");
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            try
            {
                if(guardar.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter documento = File.CreateText(guardar.FileName);
                    documento.Write(txtTexto.Text);
                    documento.Flush();
                    documento.Close();
                    txtDocumento.Text = guardar.FileName;
                }
            } catch(Exception)
            {

            }
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {


            string regexp = "";
            regexp = txtTexto.Text;

            Parser parser = new Parser();
            parser.Parse(regexp);
   

            if (!parser.gramatica_aceptada())
            {
                MessageBox.Show("Gramática No Aceptada " + parser.mensaje_error(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Scanner scanner = new Scanner(regexp);
            List<Produccion> gramatica = new List<Produccion> { };
            Token nexToken;

            txtResultado.Clear();

            string token;
            string valor;
            string principal = "";
            int temp_index = -1;
            int index = 0;
            bool es_or = false;

            do
            {
                nexToken = scanner.GetToken();
                token = nexToken.Tag.ToString();
                valor = nexToken.Value;

                if (!token.Equals("EOF"))
                {
                    if (token.Equals("NTerminal") && (index > temp_index) && !es_or)
                    {
                        temp_index = index;
                        principal = valor;
                        gramatica.Add(new Produccion() { Produccion_Valor = valor, Produccion_Detalle = new List<ProduccionDetalle> { }, lookahead = new List<string> { "" } });
                    }
                    else
                    {
                        if (!token.Equals("Colon") && !token.Equals("SColon") && !token.Equals("Pipe"))
                        {
                            if (token.Equals("Terminal"))
                                gramatica[index].Produccion_Detalle.Add(new ProduccionDetalle() { Puntito = false, Produccion_Valor = valor, Terminal = true, Final = false });
                            else
                                gramatica[index].Produccion_Detalle.Add(new ProduccionDetalle() { Puntito = false, Produccion_Valor = valor, Terminal = false, Final = false });
                        } else if (token.Equals("Pipe"))
                        {
                            es_or = true;
                            index++;
                            gramatica.Add(new Produccion() { Produccion_Valor = principal, Produccion_Detalle = new List<ProduccionDetalle> { }, lookahead = new List<string> { "" }});
                        }

                        if(token.Equals("SColon"))
                        {
                            index++;
                            es_or = false;
                        }
                    }
                }

            } while (nexToken.Tag != TokenType.EOF);

            tabControl1.SelectedTab = tabControl1.TabPages["resultado"];


            Producciones producciones = new Producciones(gramatica);
            produccionesGlobal = producciones;
            List<TablaParcer> tabla_parcer =  producciones._TablaParser();
            gvTablaParser.DataSource = tabla_parcer;

            txtResultado.AppendText("GRAMÁTICA" + System.Environment.NewLine);
            foreach (Produccion produccion in gramatica)
            {
                token = produccion.Produccion_Valor;
                txtResultado.AppendText(token + " -> ");
                foreach (ProduccionDetalle detalle in produccion.Produccion_Detalle)
                {
                    valor = detalle.Produccion_Valor;
                    txtResultado.AppendText(" " + valor);
                }
                txtResultado.AppendText(System.Environment.NewLine);
            }
            
            txtResultado.AppendText(System.Environment.NewLine);

            txtResultado.AppendText(String.Join(Environment.NewLine, producciones._Imprimr()));

          //  List<Produccion> gramatica_parse = producciones.GetGramatica();

            string test = producciones.buscarValor("'true'", "I0");
            int numero_test = producciones.buscarPop("R1");
            string padre = producciones.buscarPadre("R2");
            Console.WriteLine(test);
            test = test + "";

          //  Hashtable regla = producciones.reglas();
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["gramatica"];
        }

        private void btnAnalizarEntrada_Click(object sender, EventArgs e)
        {
            /*string test = produccionesGlobal.buscarValor("A", "I3");
            int numero_test = produccionesGlobal.buscarPop("R1");
            string padre = produccionesGlobal.buscarPadre("R2");
            Console.WriteLine(numero_test);*/
            string cadena = textEntrada.Text;
            Stack<string> estados = new Stack<string>();
            estados.Push("I0");
            Stack<string> pila = new Stack<string>();
            pila.Push("#");
            Stack<string> leer = new Stack<string>();
            leer.Push("$");
            Stack<string> accion = new Stack<string>();

            DataTable cadenaValidada = new DataTable();
            cadenaValidada.TableName = "Validacion";
            cadenaValidada.Columns.Add("Estados", typeof(string));
            cadenaValidada.Columns.Add("Pila", typeof(string));
            cadenaValidada.Columns.Add("Cadena a leer", typeof(string));
            cadenaValidada.Columns.Add("Acción", typeof(string));
            DataRow dr1;

            void DumpDataTable(DataTable dt)
            {
                if (dt == null)
                {
                    Console.Error.WriteLine("There are no rows");
                }
                else
                {
                    foreach (DataColumn c in dt.Columns)
                    {
                        //Console.Write($"\"{c.ColumnName}\",");
                        //txtResultado.AppendText($"\"{c.ColumnName}\",");
                    }
                    Console.WriteLine("");
                    //txtResultado.AppendText("");

                    gvPila.DataSource = dt;

                    foreach (DataRow drt in dt.Rows)
                    {
                        for (int i = 0; i < drt.ItemArray.Length; i++)
                        {
                            //Console.Write($"\"{drt.ItemArray[i]}\",");
                            //txtResultado.AppendText($"\"{drt.ItemArray[i]}\",");
                        }
                        Console.WriteLine("");
                    }
                }
            }

            

            void llenarPorLeer(string cadenas)
            {

                string limpia = Regex.Replace(cadenas,@"\s+", " ");
                Console.WriteLine(limpia);
                string[] str = limpia.Split(new Char[] { '\r', '\n', ' ' });

                
                Array.Reverse(str);

                /*foreach (string item in str)
                {
                    Console.WriteLine(item);
                }*/

                /*foreach (string c in str)
                {
                    string valorComillas = "'" + c + "'";
                    leer.Push(valorComillas.ToString());
                }*/

                for (int i = 0; i < str.Length; i++)
                {
                    string valorComillas = "'" + str[i] + "'";
                    leer.Push(valorComillas.ToString());
                }

                /*foreach (string item in leer)
                {
                    Console.WriteLine(item);
                }*/

            }

            //llenarPorLeer(cadena);

            algoritmo(cadena);

            

            

            foreach (string item in leer)
            {
                Console.WriteLine(item);
            }

            void algoritmo(string validarCadena)
            {
                string joinedString1 = "";
                string joinedString2 = "";
                string joinedString3 = "";
                string joinedString4 = "";



                llenarPorLeer(validarCadena);

                string resultado;
                //resultado = buscarValor(leer.Peek().ToString(), estados.Peek());
                resultado = produccionesGlobal.buscarValor(leer.Peek().ToString(), estados.Peek());
                accion.Push(resultado);

                
                while (leer.Count() != 0)
                {
                    Console.WriteLine(leer.Peek().ToString());
                    Console.WriteLine(estados.Peek());
                    resultado = produccionesGlobal.buscarValor(leer.Peek().ToString(), estados.Peek());
                    accion.Push(resultado);
                    
                    Console.WriteLine(resultado);

                    foreach (string item in leer)
                    {
                        Console.WriteLine(item);
                    }

                    if (resultado == "Accept")
                    {
                        Console.WriteLine("Cadena aceptada");
                        lblValidar.Text = "Cadena aceptada";
                        lblValidar.BackColor = Color.Green;

                        break;
                    }
                    else if (resultado == "")
                    {
                        Console.WriteLine("Cadena no aceptada");
                        lblValidar.Text = "Cadena no aceptada";
                        lblValidar.BackColor = Color.Red;

                        break;
                    }

                    char[] chf = new char[resultado.Length];

                    if (chf.Length == 2)
                    {
                        if (resultado[0].ToString() == "S")
                        {

                            joinedString1 = String.Join("", estados);
                            joinedString2 = String.Join("", pila);
                            joinedString3 = String.Join("", leer);



                            dr1 = cadenaValidada.NewRow();
                            dr1["Estados"] = joinedString1;
                            dr1["Pila"] = joinedString2;
                            dr1["Cadena a leer"] = joinedString3;

                            dr1["Acción"] = accion.Peek();


                            cadenaValidada.Rows.Add(dr1);

                            estados.Push("I" + resultado[1].ToString());

                            pila.Push(leer.Peek());

                            leer.Pop();
                            foreach (string item in estados)
                            {
                                Console.WriteLine(item);
                            }


                        }
                        else if (resultado[0].ToString() == "R")
                        {
                            joinedString1 = String.Join("", estados);
                            joinedString2 = String.Join("", pila);
                            joinedString3 = String.Join("", leer);



                            dr1 = cadenaValidada.NewRow();
                            dr1["Estados"] = joinedString1;
                            dr1["Pila"] = joinedString2;
                            dr1["Cadena a leer"] = joinedString3;

                            dr1["Acción"] = accion.Peek();


                            cadenaValidada.Rows.Add(dr1);


                            string valorquenecestio = (produccionesGlobal.buscarPop("R" + resultado[1].ToString())).ToString();
                            string valorquenecestioPadre = produccionesGlobal.buscarPadre("R" + resultado[1].ToString());
                            Console.WriteLine(valorquenecestio);
                            Console.WriteLine(valorquenecestioPadre);
                            

                            for (int i = 0; i < int.Parse(valorquenecestio); i++)
                            {

                                estados.Pop();


                                pila.Pop();

                                

                            }

                            pila.Push(valorquenecestioPadre);

                            


                            resultado = produccionesGlobal.buscarValor(valorquenecestioPadre, estados.Peek());



                            accion.Push("S" + resultado);
                            joinedString1 = String.Join("", estados);
                            joinedString2 = String.Join("", pila);
                            joinedString3 = String.Join("", leer);



                            dr1 = cadenaValidada.NewRow();
                            dr1["Estados"] = joinedString1;
                            dr1["Pila"] = joinedString2;
                            dr1["Cadena a leer"] = joinedString3;

                            dr1["Acción"] = accion.Peek();
                            estados.Push("I" + resultado);

                            foreach (string item in estados)
                            {
                                Console.WriteLine(item);
                            }
                            cadenaValidada.Rows.Add(dr1);
                            resultado = produccionesGlobal.buscarValor(leer.Peek().ToString(), estados.Peek());

                        }
                        
                    }

                    
                }
                
                /*while (leer.Count() != 0)
                {
                   

                

                    resultado = produccionesGlobal.buscarValor(leer.Peek().ToString(), estados.Peek());
                    accion.Push(resultado);



                    if (resultado == "ACCEPT")
                    {
                        Console.WriteLine("Cadena aceptada");
                       
                        break;
                    }
                    else if (resultado == "error")
                    {
                        Console.WriteLine("Cadena no aceptada");
                        
                        break;
                    }
                 
                    char[] chf = new char[resultado.Length];

                    if (chf.Length == 2)
                    {
                     

                        if (resultado[0].ToString() == "S")
                        {

                            joinedString1 = String.Join("", estados);
                            joinedString2 = String.Join("", pila);
                            joinedString3 = String.Join("", leer);
                       


                            dr1 = cadenaValidada.NewRow();
                            dr1["Estados"] = joinedString1;
                            dr1["Pila"] = joinedString2;
                            dr1["Cadena a leer"] = joinedString3;
                    
                            dr1["Acción"] = accion.Peek();


                            cadenaValidada.Rows.Add(dr1);

                            estados.Push("I" + resultado[1].ToString());

                            pila.Push(leer.Peek());

                            leer.Pop();


                        }
                        else if (resultado[0].ToString() == "R")
                        {
                            joinedString1 = String.Join("", estados);
                            joinedString2 = String.Join("", pila);
                            joinedString3 = String.Join("", leer);
                      


                            dr1 = cadenaValidada.NewRow();
                            dr1["Estados"] = joinedString1;
                            dr1["Pila"] = joinedString2;
                            dr1["Cadena a leer"] = joinedString3;
                  
                            dr1["Acción"] = accion.Peek();


                            cadenaValidada.Rows.Add(dr1);

                            
                                string valorquenecestio = (produccionesGlobal.buscarPop("R"+ resultado[1].ToString())).ToString();
                                string valorquenecestioPadre = produccionesGlobal.buscarPadre("R"+ resultado[1].ToString());


                                for (int i = 0; i < valorquenecestio.Length; i++)
                                {

                                    estados.Pop();


                                    pila.Pop();

                                pila.Push(valorquenecestioPadre);


                                resultado = produccionesGlobal.buscarValor(valorquenecestioPadre, estados.Peek());



                                accion.Push("S" + resultado);
                                joinedString1 = String.Join("", estados);
                                joinedString2 = String.Join("", pila);
                                joinedString3 = String.Join("", leer);



                                dr1 = cadenaValidada.NewRow();
                                dr1["Estados"] = joinedString1;
                                dr1["Pila"] = joinedString2;
                                dr1["Cadena a leer"] = joinedString3;

                                dr1["Acción"] = accion.Peek();
                                estados.Push("I" + resultado);

                                cadenaValidada.Rows.Add(dr1);
                                resultado = produccionesGlobal.buscarValor(leer.Peek().ToString(), estados.Peek());

                            }


                                



                            



                            foreach (var item in reglas)
                            {
                              
                                if (item.Key.ToString() == resultado[1].ToString())
                                {
                                    string valorquenecestio = item.Value[1];
                                    string valorquenecestioPadre = item.Value[0];
                            

                                    for (int i = 0; i < valorquenecestio.Length; i++)
                                    {

                                        estados.Pop();
                                       

                                        pila.Pop();

                                    }


                                    pila.Push(valorquenecestioPadre);

                                 
                                    resultado = produccionesGlobal.buscarValor(valorquenecestioPadre, estados.Peek());
                                


                                    accion.Push("S" + resultado);
                                    joinedString1 = String.Join("", estados);
                                    joinedString2 = String.Join("", pila);
                                    joinedString3 = String.Join("", leer);
                                   


                                    dr1 = cadenaValidada.NewRow();
                                    dr1["Estados"] = joinedString1;
                                    dr1["Pila"] = joinedString2;
                                    dr1["Cadena a leer"] = joinedString3;
                             
                                    dr1["Acción"] = accion.Peek();
                                    estados.Push("I" + resultado);

                                    cadenaValidada.Rows.Add(dr1);
                                    resultado = produccionesGlobal.buscarValor(leer.Peek().ToString(), estados.Peek());



                                }


                            }

                        }



                    }
                    else if ((chf.Length < 2))
                    {
                        resultado = "";
                    }
                }*/


                joinedString1 = String.Join("", estados);
                joinedString2 = String.Join("", pila);
                joinedString3 = String.Join("", leer);


                dr1 = cadenaValidada.NewRow();
                dr1["Estados"] = joinedString1;
                dr1["Pila"] = joinedString2;
                dr1["Cadena a leer"] = joinedString3;
                dr1["Acción"] = accion.Peek();


                cadenaValidada.Rows.Add(dr1);



            }
            DumpDataTable(cadenaValidada);
            


            /*foreach (string item in leer)
            {
                Console.WriteLine(item);
            }*/
        }
    }
}
