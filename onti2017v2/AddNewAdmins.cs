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
    public partial class AddNewAdmins : Form
    {
        public AddNewAdmins()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddNewAdmins_Load(object sender, EventArgs e)
        {
            DatabaseHelper.ListUsers();
            foreach(var item in DatabaseHelper.userModels)
            {
                comboBox1.Items.Add(item.Email);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseHelper.AdminsUpdate(comboBox1.SelectedItem.ToString());
        }
    }
}
