using compilador2020.Lexico.clases;
using compilador2020.Lexico.estructuraDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace compilador2020.Lexico.vistas
{
    public partial class winLexico : UserControl
    {

       public bool iniciarSintactico = false;
  
        public winLexico()
        {
            lexico miLexico = new lexico();
            InitializeComponent();

            // Presento las estructuras
            tblMT.DataSource = miLexico.ListAFD;
            tblAlfabeto.DataSource = miLexico.Alfabeto;

            // Llamar al reconocedor para reconocer archivo fuente
            txtEntrada.Text = miLexico.texto_file_name;
            

            // Presentar listas en sus respectivas tablas
            tblMovimeintos.DataSource = miLexico.listaMovimientos;

            //presentamos la taba de tokens reconocidos 
            tblTR.DataSource = miLexico.lista_tks_reconocidos;


            //presentamos la tabla de errores
            tblErrores.DataSource = miLexico.listError;

            if (miLexico.listError.Count == 0) {
               
                iniciarSintactico = true;
            }



            // Necesitamos poner el tipo y el ancho por
            // cada una de la variables
            tblTDS.DataSource = miLexico.listTDS;
            CabecerasDatagrid();
        }

       public void CabecerasDatagrid()
        {
            //CABECERAS AUTOMATA (AFD)
            tblMT.Columns[0].HeaderText = "Estado";
            tblMT.Columns["Leyendo"].Visible = false;
            tblMT.Columns[3].HeaderText = "Leyendo";
            tblMT.Columns[2].HeaderText = "Nuevo estado";
            

            //CABECERAS ALFABETO
            tblAlfabeto.Columns[0].HeaderText = "Número de token";
            tblAlfabeto.Columns[1].HeaderText = "Símbolo";
            tblAlfabeto.Columns[2].HeaderText = "Nombre de token";
            tblAlfabeto.Columns[3].HeaderText = "Lexema";

            //CABECERAS MOVIMIENTOS
            tblMovimeintos.Columns[0].HeaderText = "Estado";
            tblMovimeintos.Columns[1].HeaderText = "Leyendo";
            tblMovimeintos.Columns[2].HeaderText = "Nuevo estado";
            tblMovimeintos.Columns[3].HeaderText = "Descripción";

            //CABECERAS TOKENS RECONOCIDO
            tblTR.Columns[0].HeaderText = "Número de token";
            tblTR.Columns[1].HeaderText = "Simbolo";
            tblTR.Columns[2].HeaderText = "Nombre de token";
            tblTR.Columns[3].HeaderText = "Lexema";

            //CABECERAS ERROR
            tblErrores.Columns[0].HeaderText = "Número de error";
            tblErrores.Columns[1].HeaderText = "Mensaje de error";

        }
    }
}
