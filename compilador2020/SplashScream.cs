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
    public partial class SplashScream : Form
    {
        public SplashScream()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fund_pogress();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SplashScream_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;
        }
        public void fund_pogress()
        {
            progressBar1.Increment(2);
            label1.Text = progressBar1.Value.ToString ()+ "%";
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                this.Hide();
                Main f1 = new Main();
                f1.Show();

            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
