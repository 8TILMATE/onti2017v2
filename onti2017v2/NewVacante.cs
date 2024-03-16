using onti2017v2.Models;
using onti2017v2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onti2017v2
{
    public partial class NewVacante : Form
    {
        public NewVacante()
        {
            InitializeComponent();
        }

        private void NewVacante_Load(object sender, EventArgs e)
        {
            StreamReader rdr = new StreamReader(Resources.vacanteString);
            textBox1.Text=rdr.ReadToEnd();
            rdr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter(Resources.vacanteString);
            streamWriter.WriteLine(textBox1.Text);
            streamWriter.Dispose();
            streamWriter.Close();
        }
    }
}
