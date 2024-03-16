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
    public partial class Rezerva : Form
    {
        VacanteModel model1;
        UserModel user1;
        public Rezerva(VacanteModel model,UserModel model2)
        {
            InitializeComponent();
            model1 = model;
            user1 = model2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text="Pret: "+ (model1.Pret*Int32.Parse(textBox1.Text)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }
    }
}
