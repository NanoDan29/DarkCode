using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador2020.Sintactico.Estructura_datos
{
  public class reglas_de_produccion
    {
        private int numero_regla;
        private string part_izquierda;
        private string part_derecha;
        private string izq_sinonimo;
        private string der_sinonimo;
        private int size;
   

        public int Numero_regla { get => numero_regla; set => numero_regla = value; }
        public string Part_izquierda { get => part_izquierda; set => part_izquierda = value; }
        public string Part_derecha { get => part_derecha; set => part_derecha = value; }
        public string Izq_sinonimo { get => izq_sinonimo; set => izq_sinonimo = value; }
        public string Der_sinonimo { get => der_sinonimo; set => der_sinonimo = value; }
        public int Size { get => size; set => size = value; }
    }
}
