using compilador2020.Lexico;
using compilador2020.Lexico.estructuraDatos;
using compilador2020.Sintactico.Estructura_datos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace compilador2020.Sintactico.clase
{
    class sintactico
    {
        //carga la lista 
        public List<reglas_de_produccion> list_reglas;
        public List<reglas_de_produccion> list_reglas_reconocidas;
        public List<SLR> list_SLR;
        public List<Tokens> list_tokens_reconocido = new List<Tokens>();
        public List<TablaSimbolos> list_TDS = new List<TablaSimbolos>();
        public List<Tokens> list_Alfabeto = new List<Tokens>();
        public List<AFD> list_movimientos = new List<AFD>();
        public Stack<object> pila_sintactica = new Stack<object>();
        public List<firstnext> list_FN = new List<firstnext>();
        public List<errores> listaErroresSintactico = new List<errores>();
        public List<pilaSintactica> movimientoPila = new List<pilaSintactica>();
        int contadorGlobalTokens = 0;



        private void guardarMovimientoPila()
        {
            pilaSintactica miPila;
            string pila = "[ ";
            int contador = 1;
            foreach (var item in pila_sintactica.Reverse())
            {
                if (pila_sintactica.Count == contador) { 
                pila += item + "";
                }
                else
                {
                    pila += item + " , ";
                }
                contador++;
            }
            pila += " ]";
            miPila = new pilaSintactica()
            {
                Pila = pila
            };
            movimientoPila.Add(miPila);
        }

        public sintactico(List<Tokens> list_tok_reconocidos, List<TablaSimbolos> list_tds, List<Tokens> list_alfabeto)
        {
            cargarArchivos cargarArchivos = new cargarArchivos();
            //cargo mi reglas (Gramatica original)
            list_reglas = cargarArchivos.cargarReglasProduccion();

            //cargar el automata SLR
            list_SLR = cargarArchivos.Cargar_SLR();
            //
            list_tokens_reconocido = list_tok_reconocidos;
            //
            list_TDS = list_tds;
            //
            list_Alfabeto = list_alfabeto;

            //Cargar First y next
            list_FN = cargarArchivos.cargarFirstNext();

            //añadir al final del los tokens reconocidos el $ como
            //fin de terminacion del analizador
            Tokens tk = new Tokens()
            {
                NumToken = 1000,
                Sinonimo = "ñ",
                NombreToken = "fin",
                Lexema = "EOF"
            };
            list_tokens_reconocido.Add(tk);



        }

        //algiriitmo del analizador
        public void AnalizadorSLR()
        {
            Tokens tk = new Tokens();
            list_reglas_reconocidas = new List<reglas_de_produccion>();
            bool band = true;
            string simbolo;
            int estado = 0;
            int newestado = 0;
            int cont = 0;

            //tomar el token 
            tk = list_tokens_reconocido[cont];
            simbolo = tk.Sinonimo;

            //Guarda el movimiento quese hizo en pila
            pila_sintactica.Push(estado);
            guardarMovimientoPila();

            //bucle del reconosedor hasta cuando reconosca Apectar o Error
            while (band)
            {
                // Movernos en la matriz sintactica con el simbolo
                // para buscar el nuevo estado         
                newestado = devolverNuevoEstado(estado, simbolo);

                Console.WriteLine(estado + " leyendo: " + simbolo + " va a: " + newestado);
                // Verificar si encontro el nuevo estado
                if (newestado != 997)
                {
                    // Si encontro nuevo estado
                    if (newestado < 0)
                    {
                        
                        //se reconocio regla
                        newestado = -newestado;
                        reglas_de_produccion rule = devolverRegla(newestado);

                        if (rule == null)
                        {
                            //Error no encontró regla
                            continue;
                        }
                        //Guardar la regla en lista de reglas reconocidas
                        list_reglas_reconocidas.Add(rule);

                        int cuenta = rule.Der_sinonimo.Length;
                        cuenta = cuenta * 2;
                        // Agregamos a la pila sintactica
                        guardarMovimientoPila();
                        //bajar de la pila
                        for (int i = 0; i < cuenta; i++)
                        {
                            pila_sintactica.Pop();
                        }
                        //encontro la regla y tenemos que sacar la parte izquierda
                        estado = (int)pila_sintactica.Peek();
                        // Guardamos en la pila el no terminal que esta la parte izquierda de la regla
                        pila_sintactica.Push(rule.Izq_sinonimo);
                        //movernos en la SLR para ver a que estado va
                        string s = rule.Izq_sinonimo;
                        newestado = devolverNuevoEstado(estado, s);

                        if (newestado != 997)
                        {
                            pila_sintactica.Push(newestado);
                            estado = newestado;
                        }
                        else
                        {
                            // Presentar mensaje de error
                            errores miError = new errores()
                            {
                                NumError = 1,
                                MsjError = "Error sintáctico: " + s + " no se encuentra en la matriz transciones " + newestado
                            };
                            listaErroresSintactico.Add(miError);
                            band = false;
                        }
                        // Guardar movimiento de la pila
                        guardarMovimientoPila();
                    }
                    else if (newestado == 1000)
                    {
                        //Aceptar 
                        //bajar de la pila
                        for (int i = 0; i < 3; i++)
                        {
                            pila_sintactica.Pop();
                        }
                        guardarMovimientoPila();
                        break;
                    }
                    else
                    {
                        // Guardar movimientos en la lista de movimientos
                        // para presentar en la tabla mov sintactica
                        AFD miMove = new AFD()
                        {
                            Estado = estado,
                            Leyendo = Convert.ToChar(simbolo),
                            NEstado = newestado
                        };
                        list_movimientos.Add(miMove);
                        //moverse dentrodel automata
                        pila_sintactica.Push(simbolo);
                        pila_sintactica.Push(newestado);
                        estado = newestado;
                        //tomar el token 
                        cont++;
                        tk = list_tokens_reconocido[cont];
                        simbolo = tk.Sinonimo;
                    }
                }
                else
                {
                    // Guardar movimientos en la lista de movimientos
                    // para presentar en la tabla mov sintactica
                    AFD miMove = new AFD()
                    {
                        Estado = estado,
                        Leyendo = Convert.ToChar(simbolo),
                        NEstado = newestado
                    };
                    list_movimientos.Add(miMove);
                    //Buscar y presentar posible simbolo con el que debe ir
                    string simbolosPosibles = "";
                    foreach (var item in list_SLR)
                    {
                        if (estado == item.Estado)
                        {
                            if (!Char.IsUpper(Convert.ToChar(item.Simbolos_lee)))
                            {
                                //Buscar en alfabeto
                                string lexema=buscarEnAlfabeto(item.Simbolos_lee);
                                simbolosPosibles += lexema + " ";
                               
                            }

                        }
                    }

                    // Presentar mensaje de error
                    errores miError = new errores()
                    {
                        NumError = 1,
                        MsjError = "Error sintáctico: " + simbolo +
                        " no se encuentra en la matriz transciones  talvex quizas omitio uno de estos simbolos: " + simbolosPosibles
                    };
                    listaErroresSintactico.Add(miError);
                    band = false;
                    //tomar el token para recuperarse del error
                    cont++;

                    tk = list_tokens_reconocido[cont];
                    simbolo = tk.Sinonimo;


                }
            }
        }

        public reglas_de_produccion devolverRegla(int regla)
        {
            
               
            for (int i = 0; i < list_reglas.Count; i++)
            {
                if (list_reglas[i].Numero_regla == regla)
                {
                    // verificar si la regla 5 6 7 8 9
                    switch (regla)
                    {
                        case 5: // c --> e                         
                            buscarEnTokensIdentificador(1, 2, "e");
                            break;
                        case 6: // c --> r 

                            buscarEnTokensIdentificador(2, 4, "r");
                            break;
                        case 7: // c --> c                           
                            buscarEnTokensIdentificador(3, 1, "c");
                            break;
                        case 8: // c --> t                         
                            buscarEnTokensIdentificador(4, 256, "t");
                            break;
                        case 9: // c --> b                        
                            buscarEnTokensIdentificador(5, 1, "b");
                            break;
                        default:
                            break;
                    }
                    return list_reglas[i];
                }
            }

            return null;
        }

        public void buscarEnTokensIdentificador(int tipo, int size, string sinonimo)
        {
            Tokens item;
            bool flag = false;
            string variable;

            while (contadorGlobalTokens < list_tokens_reconocido.Count)
            {
                item = list_tokens_reconocido[contadorGlobalTokens++];
                if (item.Sinonimo.Equals(sinonimo))
                {
                    flag = true;
                }
                else
                {
                    if (item.Sinonimo.Equals("i") && flag)
                    {
                        variable = item.Lexema;
                        buscarEnTDSIdentificador(variable, tipo, size);
                    }
                    if (item.Sinonimo.Equals(";"))
                    {
                        return;
                    }

                }

            }


        }
        public void buscarEnTDSIdentificador(string variable, int tipo, int size)
        {
            int incrementar = 0;
            foreach (var item in list_TDS)
            {
               
                if (item.Nombre.Equals(variable))
                {

                    if (item.Tipo == 0)
                    {
                        item.Tipo = tipo;
                        item.Zise = size;
                    }
                    else
                    {
                        errores miError = new errores()
                        {
                            NumError = 1,
                            MsjError = "Error sintáctico: " + variable + " variable ya definida "+incrementar
                        };
                        listaErroresSintactico.Add(miError);
                        
                    }
                   
                }
                incrementar++;
            }
        }

        public int devolverNuevoEstado(int estado, string simbolo)
        {
            foreach (SLR e in list_SLR)
            {

                if (e.Estado == estado && simbolo == e.Simbolos_lee)
                {
                    return (e.New_estados);
                }
            }
            return 997;
        }

        public string buscarEnAlfabeto(string simbolo)
        {
            foreach (var item in list_Alfabeto)
            {
                if (simbolo.Equals(item.Sinonimo))
                {

                    return item.Lexema;
                }
            }
            return "";
        }
    }
}
