﻿using onti2017v2.Models;
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
            try
            {
                label2.Text = "Pret: " + (model1.Pret * Int32.Parse(textBox1.Text)).ToString();
            }
            catch
            {
                label2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RezervariModel model = new RezervariModel();
            model.Pret = model1.Pret * Int32.Parse(textBox1.Text);
            model.IdVacanta = model1.Id;
            model.IdUser = user1.id;
            model.DataSt = dateTimePicker1.Value;
            model.Datasf = dateTimePicker2.Value;
            DatabaseHelper.InsertRezervari(model);
            this.Hide
                ();
            FrmVacanta frmVacanta = new FrmVacanta(user1);
            frmVacanta.ShowDialog();
            this.Close();
        }
    }
}
