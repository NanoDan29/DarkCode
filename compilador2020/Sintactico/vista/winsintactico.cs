using compilador2020.Lexico.clases;
using compilador2020.Sintactico.clase;
using System;
using System.Windows.Forms;

namespace compilador2020.Sintactico.vista
{
    public partial class winsintactico : UserControl
    {
        public winsintactico()
        {
            lexico miLexico = new lexico();
            InitializeComponent();
            //hago la llamada a métodos staticos de mi clase lexico 
            //lista de tokens reconcidos  
            tblTR.DataSource = miLexico.lista_tks_reconocidos;
            //tabla de simbolos       
            // llamo al los metodos del sintactico
            sintactico parser = new sintactico(miLexico.lista_tks_reconocidos, miLexico.listTDS, miLexico.Alfabeto);
            //cargo la tabla gramatica original 
            tblGOR.DataSource = parser.list_reglas;
            //Cargar TBL first next
            tblFN.DataSource = parser.list_FN;
            tblSLR.DataSource = parser.list_SLR;
            //Llamar al analizador
            parser.AnalizadorSLR();
            //Presentar lista de reglas reconocidas
            tblRR.DataSource = parser.list_reglas_reconocidas;
            //tabla Movimientos  
            tblMov.DataSource = parser.list_movimientos;
            tblMov.Columns[3].Visible = false;


            // Mostrar movimientos pila
            tblMP.DataSource = parser.movimientoPila;
            // Mostrat lista de TDS corregida
            tblTDS.DataSource = parser.list_TDS;
            // Mostrar lista de errores
            tblError.DataSource = parser.listaErroresSintactico;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
