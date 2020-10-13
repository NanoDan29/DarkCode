using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador2020.Lexico.estructuraDatos
{
   public class TablaSimbolos
    {
        private int numero;
        private string nombre;
        private int tipo;
        private int zise;
        private object valor;

        public TablaSimbolos()
        {

        }

        public TablaSimbolos(int numero, int tipo, int zise, object valor, string nombre)
        {
            this.numero = numero;
            this.nombre = nombre;
            this.tipo = tipo;
            this.zise = zise;
            this.valor = valor;
 
        }

        public int Numero { get => numero; set => numero = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Tipo { get => tipo; set => tipo = value; }
        public int Zise { get => zise; set => zise = value; }
        public object Valor { get => valor; set => valor = value; }
    }
}
