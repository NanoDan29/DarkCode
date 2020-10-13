using compilador2020.Lexico.estructuraDatos;
using System;
using System.Collections.Generic;
using System.IO;

namespace compilador2020.Lexico.clases
{
    class lexico
    {

        public List<AFD> ListAFD;
        public List<Tokens> Alfabeto;
        public List<TablaSimbolos> listTDS;
        public List<Tokens> lista_tks_reconocidos;
        public List<AFD> listaMovimientos;
        public List<errores> listError;
        public string texto_file_name;

        public lexico()
        {
            cargarArchivos cargarArchivos = new cargarArchivos();
            // Cargar los archivos AFD.xml
            ListAFD = cargarArchivos.automataAFD();
            // cargar el archivo Alfabeto.xml
            Alfabeto = cargarArchivos.listaTokens();
            // Declarar los tipos de tabla de simbolos
            listTDS = new List<TablaSimbolos>();
            // Declara la lista de tokens reconocidos

            // Aperturar el archivo fuente de entrada
            Cargar_Archivo_Fuente();

            Reconocedor_Lexico();



        }

        // Función que carga el archivo fuente a compilar
        public void Cargar_Archivo_Fuente()
        {
            string ruta_file = Path.GetFullPath("../../Archivos/Lexico");

            // subir a una estructura para ir leyendo caracter x caracter
            // 
            texto_file_name = codigoFuente(ruta_file);
            // Mostrar en txtEntrada

        }


        static string codigoFuente(string ruta)

        {

            string path = ruta + "\\archivo.cs";
            string texto_archivo = File.ReadAllText(path);

            return texto_archivo;
        }



        public void Reconocedor_Lexico()
        {
            listaMovimientos = new List<AFD>();
            lista_tks_reconocidos = new List<Tokens>();
            listError = new List<errores>();
            listTDS = new List<TablaSimbolos>();

            int estado = 0, newestado = 0, nidentificador = 0;
            char simbolo;
            string lexema = null;
            bool flag_main = false;

            // Formateando el texto de entrada tomar en cuenta que al quitar
            // el blando dentro de lo que este en comillas se podra como #
            // Hacer un replace siempre y cuando no este entre comillas
            string entrada = texto_file_name.Replace('\n', '#').Replace('\t', '#').Replace('\r', '#').Replace(' ', '#') + "#";
            char[] palabras = entrada.ToCharArray();

            int cuenta_simbolos = entrada.Length;
            Tokens tk = new Tokens();
            int j = 0;

            AFD move;

            while (j < palabras.Length)
            {
                simbolo = Convert.ToChar(palabras[j]);

                newestado = movimiento_AFD(estado, simbolo);
                if (!(simbolo.Equals('#') && (estado == 0)))
                {
                    move = new AFD()
                    {
                        Estado = estado,
                        Leyendo = simbolo,
                        NEstado = newestado
                    };
                    if (newestado < 0)
                    {
                        move.Lee = "Token Reconocido " + (-newestado);
                    }

                    listaMovimientos.Add(move);
                }

                if (newestado == 998)
                {
                    estado = 0;
                    lexema = "";
                    j++;
                    continue;
                }

                if (newestado == 999)
                {
                    // Presentar mensaje error
                    errores miError = new errores()
                    {
                        NumError = 1,
                        MsjError = "Error léxico: simbolo " + simbolo + " no reconocido en el lexema " + lexema
                    };
                    listError.Add(miError);
                    // Volvemos a estado 0 y lexema para que vuelva areconocer otro token
                    while (true)
                    {
                        if (palabras[j] != '#')
                        {
                            j++;
                            continue;
                        }
                        break;
                    }
                    estado = 0;
                    lexema = "";
                    j++;
                    continue;
                }
                else if (newestado < 0)
                {

                    newestado = -newestado;
                    BuscarToken(newestado, lexema);
                    if (newestado == 22) flag_main = true;
                    // Verificar si es un identificador
                    if (newestado == 1 && !(flag_main))
                    {

                        // Quedarnos solo con 8 carateres significativos
                        if (!(lexema.Length < 8))
                        {
                            lexema = lexema.Substring(0, 8);
                        }

                        // Se reconocio un identificador guardar en la tabla de  simbolos
                        TablaSimbolos identificador = new TablaSimbolos()
                        {
                            Numero = nidentificador++,
                            Nombre = lexema
                            // Guardar tipo y ancho
                        };
                        //Buscar identificador: si esta error 
                       

                        listTDS.Add(identificador);
       
                    }
                    estado = 0;
                    lexema = "";
                }
                else
                {
                    if (!simbolo.Equals('#'))
                    {
                        lexema += simbolo;
                    }
                    estado = newestado;
                }

                j++;

            }

        }

        public int movimiento_AFD(int estado, char simbolo)
        {
            int caso = 0;
            if (char.IsLetter(simbolo))
            {
                //es una letra
                caso = 2;
                if (char.IsUpper(simbolo))
                {
                    //es una mayuscula
                    caso = 1;
                }
            }
            else if (char.IsDigit(simbolo))
            {
                //es un digitp 
                caso = 3;
            }
            else
            {
                //validar que el simbolo exista en nuestro alfabeto
                //es otro caracter
                string simbolos = "!#$%&<>_=:,?'+-/*().{};\\";
                string simboloString = simbolo.ToString();
                if (!simbolos.Contains(simboloString))
                {
                    //Presentar mensaje de error
                    errores miError = new errores()
                    {
                        NumError = 2,
                        MsjError = "Error léxico: simbolo " + simbolo + " no es parte de nuestro lenguaje"
                    };
                    listError.Add(miError);
                    return 998;
                }

                caso = 4;
            }

            foreach (AFD transicion in ListAFD)
            {
                if (transicion.Estado == estado)
                {
                    if ((transicion.Lee.Equals("letra") && caso == 2) ||
                       (transicion.Lee.Equals("mayuscula") && caso == 1) ||
                       (transicion.Lee.Equals("digito") && caso == 3) ||
                       (transicion.Lee.Equals("simbolo") && caso == 4))
                    {

                        return transicion.NEstado;
                    }
                    String letra = simbolo.ToString();
                    if (transicion.Lee.Equals(letra))
                    {
                        return transicion.NEstado;
                    }
                }
            }

            return 999;
        }

        public void BuscarToken(int nrtk, string lexema)
        {
            Tokens tk1;
            foreach (Tokens item in Alfabeto)
            {
                if (item.NumToken == nrtk)
                {
                    tk1 = new Tokens()
                    {
                        NumToken = item.NumToken,
                        NombreToken = item.NombreToken,
                        Sinonimo = item.Sinonimo,
                        Lexema = lexema
                    };

                    // Guardar en lista tokens reconocidos
                    if (tk1.NumToken != 46)
                    {
                        lista_tks_reconocidos.Add(tk1);
                    }
                   
                }
            }

        }
     

    }
}
