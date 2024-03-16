using onti2017v2.Models;
using onti2017v2.Properties;
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
        UserModel model1;
        public FrmVacanta(UserModel model)
        {
            InitializeComponent();
            model1 = model;
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rezerva rezerva = new Rezerva(DatabaseHelper.vacantes[currentvalue], model1);
            rezerva.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
        e.RowIndex >= 0)
            {
                string nume = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                dataGridView1.Rows.RemoveAt(e.RowIndex);

                int id =1;
                foreach(VacanteModel x in DatabaseHelper.vacantes)
                {
                    if(x.NumeVacanta==nume.Trim())
                    {
                        id = x.Id; break;
                    }
                }
                DatabaseHelper.DeleteFromRezervari(id);
            }
        }

        private void FrmVacanta_Load(object sender, EventArgs e)
        {
            DatabaseHelper.RezervariUser(model1);
            foreach(RezervariModel x in DatabaseHelper.rezervaris)
            {
                dataGridView1.Rows.Add("*");
                dataGridView1.Rows.Add(DatabaseHelper.vacantes[x.Id].NumeVacanta, x.DataSt, x.Datasf, x.Pret/ DatabaseHelper.vacantes[x.Id].Pret, x.Pret);
            }
            if (model1.TipCont == 1)
            {
                tabControl1.TabPages.RemoveAt(0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewVacante newVacante = new NewVacante();
            newVacante.ShowDialog();
            this.Show();
            DatabaseHelper.ResetVacante();
            DatabaseHelper.InsertVacanteIntoDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewAdmins x = new AddNewAdmins();
            x.ShowDialog();
        }
    }
}
