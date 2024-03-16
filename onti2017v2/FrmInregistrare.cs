using onti2017v2.Models;
using onti2017v2.ViewModels;
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
    public partial class FrmInregistrare : Form
    {
        bool isRobot = true;
        public FrmInregistrare()
        {
            InitializeComponent();
        }

        private void FrmInregistrare_Load(object sender, EventArgs e)
        {
            ViewModels.ImageManipulation.GetCaptcha();
            Random random = new Random();
            int index = random.Next(1, ImageManipulation.Captcha.Count);
            pictureBox2.SizeMode=PictureBoxSizeMode.StretchImage;
            pictureBox2.ImageLocation = ImageManipulation.Captcha[index];
            pictureBox2.Refresh();
            pictureBox2.Tag = ImageManipulation.Captcha[index].Remove(0,65);
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if(textBox5.Text+".png" == pictureBox2.Tag.ToString())
            {
                isRobot = false;
                MessageBox.Show("Well Done Nigga!");
                textBox5.TextChanged -= textBox5_TextChanged;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isRobot == false)
            {
                UserModel userModel = new UserModel();
                userModel.Nume = textBox1.Text; userModel.Prenume = textBox2.Text;
                userModel.Email = textBox3.Text; userModel.Parola = textBox4.Text;
                userModel.TipCont = 1;
                DatabaseHelper.InsertUser(userModel);
            }
        }
    }
}
