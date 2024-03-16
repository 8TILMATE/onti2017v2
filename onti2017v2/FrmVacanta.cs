using onti2017v2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onti2017v2
{
    public partial class FrmVacanta : Form
    {
        public FrmVacanta(UserModel model)
        {
            InitializeComponent();
            DatabaseHelper.InsertVacanteIntoDB();
            
        }
        public static int currentvalue = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentvalue < DatabaseHelper.vacantes.Count - 1)
            {
                currentvalue++;
            }
            else
            {
                currentvalue = 0;
            }
            IndexChanged();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled=false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentvalue < DatabaseHelper.vacantes.Count-1)
            {
                currentvalue++;
            }
            else
            {
                currentvalue = 0;
            }
            IndexChanged();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentvalue > 0)
            {
                currentvalue--;
            }
            else
            {
                currentvalue = DatabaseHelper.vacantes.Count-1;
            }
            IndexChanged();
        }
        private void IndexChanged()
        {
            pictureBox1.ImageLocation = DatabaseHelper.vacantes[currentvalue].CaleFisier;
            label1.Text = DatabaseHelper.vacantes[currentvalue].Descriere;
        }
    }
}
