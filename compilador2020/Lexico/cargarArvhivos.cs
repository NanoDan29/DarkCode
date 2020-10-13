using compilador2020.Lexico.estructuraDatos;
using compilador2020.Sintactico.clase;
using compilador2020.Sintactico.Estructura_datos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace compilador2020.Lexico
{
    public class cargarArchivos

    {
        string path_file_lex = Path.GetFullPath("../../Archivos/Lexico");
        string path_file_sin = Path.GetFullPath("../../Archivos/Sintactico");
        public reglas_de_produccion misReglasProduccion;

        // Archivos para lexico
        public List<AFD> automataAFD()
        {
            string path = path_file_lex + "\\AFD.json";
            List<AFD> listaTokens = new List<AFD>();
            string archivo = File.ReadAllText(path);
            listaTokens = JsonConvert.DeserializeObject<List<AFD>>(archivo);
            return listaTokens;
        }

        public List<Tokens> listaTokens()
        {
            string path = path_file_lex + "\\Alfabeto.json";
            List<Tokens> listaTokens = new List<Tokens>();
            string archivo = File.ReadAllText(path);
            listaTokens = JsonConvert.DeserializeObject<List<Tokens>>(archivo);
            return listaTokens;
        }


        // Archivos para Sintactico

        public List<firstnext> cargarFirstNext()
        {
            string path = path_file_sin + "\\firstnext.json";
            List<firstnext> lista = new List<firstnext>();
            string archivo = File.ReadAllText(path);
            lista = JsonConvert.DeserializeObject<List<firstnext>>(archivo);
            return lista;
        }

        public List<SLR> Cargar_SLR()
        {
            string path = path_file_sin + "\\SLR.json";
            List<SLR> listaFN = new List<SLR>();
            SLR miSLR;
            string archivo = File.ReadAllText(path);
            dynamic data = JObject.Parse(archivo);
            for (int i = 0; i < data.SLR.Count; i++)
            {
                string lee = data.SLR[i].lee;
                char[] cadenaLee = lee.ToCharArray();
                for (int j = 0; j < cadenaLee.Length; j++)
                {                
                    miSLR = new SLR()
                    {
                        Estado = data.SLR[i].estado,
                        Simbolos_lee = cadenaLee[j].ToString(),
                        New_estados = data.SLR[i].nestado[j]
                    };
                    listaFN.Add(miSLR);
                }
            }
            return listaFN;
        }

        public List<reglas_de_produccion> cargarReglasProduccion()
        {
            string path = path_file_sin + "\\gramatica.json";
            List<reglas_de_produccion> listreglasProduccion = new List<reglas_de_produccion>();
            string archivo = File.ReadAllText(path);
            dynamic data = JObject.Parse(archivo);
            int cont = 1;
            foreach (var item in data.gramatica.reglas.regla)
            {
                if (item.size != null)
                {
                    misReglasProduccion = new reglas_de_produccion()
                    {
                        Numero_regla = cont,
                        Part_derecha = item.parteDerechaNS,
                        Part_izquierda = item.parteIzquierdaNS,
                        Izq_sinonimo = item.parteIzquierda,
                        Der_sinonimo = item.parteDerecha,
                        Size = item.size
                    };

                    listreglasProduccion.Add(misReglasProduccion);
                    cont++;
                }
                

            }

            return listreglasProduccion;
        }

       


    }
}
