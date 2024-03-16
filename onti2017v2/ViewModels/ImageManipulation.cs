using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onti2017v2.ViewModels
{
    public static class ImageManipulation
    {
        public static List<string> Captcha = new List<string>();

        public static void GetCaptcha()
        {
            List<Image> list = new List<Image>();
            foreach(string file in Directory.EnumerateFiles("C:\\Users\\rafxg\\source\\repos\\onti2017v2\\onti2017v2\\Resurse\\Logare\\", "*.png"))
            {

                Captcha.Add(file);               
            }
          // Captcha=list;
        }
    }
}
