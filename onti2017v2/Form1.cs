using onti2017v2.Models;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InsertVacanteIntoDB();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserModel userModel = new UserModel();
            userModel.Parola = textBox2.Text;
            userModel.Email = textBox1.Text;
            if(DatabaseHelper.CheckUser(ref userModel))
            {
                if (checkBox1.Checked)
                {
                    StreamWriter streamWriter = new StreamWriter("C:\\Users\\rafxg\\source\\repos\\onti2017v2\\onti2017v2\\Resurse\\RememberedUser.txt");
                    streamWriter.WriteLine(userModel.Email + " " + userModel.Parola);
                    streamWriter.Dispose();
                    streamWriter.Close();
                }
                FrmVacanta vac = new FrmVacanta(userModel);
                this.Hide();
                vac.ShowDialog();
                this.Close();

                
            }
            Console.WriteLine(DatabaseHelper.CheckUser(ref userModel).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Register = new FrmInregistrare();
            this.Hide();
            Register.ShowDialog();
            this.Close();
        }
    }
}
