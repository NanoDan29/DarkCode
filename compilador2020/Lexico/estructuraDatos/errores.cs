using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador2020.Lexico.estructuraDatos
{
     public class errores
    {
        private int numError;
        private string msjError;

        public int NumError { get => numError; set => numError = value; }
        public string MsjError { get => msjError; set => msjError = value; }
    }
}
