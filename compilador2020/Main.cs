using compilador2020.Lexico;
using compilador2020.Lexico.clases;
using compilador2020.Lexico.vistas;
using compilador2020.Sintactico.vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace compilador2020
{
    public partial class Main : Form
    {
      
       
       
        public Main()
        {
            InitializeComponent();
            btn_semantico.Enabled = false;
            btn_sintactico.Enabled = false;
        }

        private void btn_lexico_Click(object sender, EventArgs e)
        {
            winLexico winlexico = new winLexico();
            principal.Controls.Clear();
            principal.Controls.Add(winlexico);
            winlexico.BringToFront();

            if (winlexico.iniciarSintactico)
            {
                MessageBox.Show("Lexico reconocido sin errores se habilito sintactico");
                btn_sintactico.Enabled = true;
            }
            else
            {
                btn_sintactico.Enabled = false;
            }
            
        }

        private void btn_sintactico_Click(object sender, EventArgs e)
        {
            winsintactico windsin = new winsintactico();
            principal.Controls.Clear();
            principal.Controls.Add(windsin);
            windsin.BringToFront();
            btn_sintactico.Enabled = true;

           // principal.Visible = false;
            btn_semantico.Enabled = true;

        }

    }
}
