using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador2020.Lexico.estructuraDatos
{
    public class Tokens
    {
        private int numToken; 
        private string sinonimo; 
        private string nombreToken;
        private string lexema;

        public int NumToken { get => numToken; set => numToken = value; }
        public string Sinonimo { get => sinonimo; set => sinonimo = value; }
        public string NombreToken { get => nombreToken; set => nombreToken = value; }
        public string Lexema { get => lexema; set => lexema = value; }
    }
}
