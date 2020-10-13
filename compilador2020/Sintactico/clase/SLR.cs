using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador2020.Sintactico.clase
{
    public class SLR
    {
        private int estado;
        private string simbolos_lee;
        private int new_estados;

        public int Estado { get => estado; set => estado = value; }
        public  string Simbolos_lee { get => simbolos_lee; set => simbolos_lee = value; }
        public int New_estados { get => new_estados; set => new_estados = value; }
    }
}
