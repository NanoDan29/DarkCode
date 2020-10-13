using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador2020.Lexico.estructuraDatos
{
   public class AFD
    {
        //cambiar a AFD
        private int estado,nEstado;
        private string lee;
        private char leyendo;

        public int Estado { get => estado; set => estado = value; }
        public char Leyendo { get => leyendo; set => leyendo = value; }
        public int NEstado { get => nEstado; set => nEstado = value; }
        public string Lee { get => lee; set => lee = value; }
       
    }
}
