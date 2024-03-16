using onti2017v2.Models;
using onti2017v2.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onti2017v2
{
    public static class DatabaseHelper
    {
        public static List<Models.VacanteModel> vacantes = new List<Models.VacanteModel>();
        public static void InsertVacanteIntoDB()
        {
            try
            {
                string cmdtext = "Select * from Vacante";
               using (SqlConnection conn = new SqlConnection(Resources.connectionString))
                {conn.Open();
                    using(SqlCommand sqlCommand = new SqlCommand(cmdtext, conn))
                    {
                        using(SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            reader.Read();
                            Console.WriteLine(reader.GetValue(0).ToString());
                            while(reader.Read())
                            {
                                
                                vacantes.Add(new Models.VacanteModel() { Id = Int32.Parse(reader.GetValue(0).ToString()), NumeVacanta = reader.GetValue(1).ToString(), Descriere = reader.GetValue(2).ToString(), CaleFisier = reader.GetValue(3).ToString(), Pret = Int32.Parse(reader.GetValue(4).ToString()), NrLocuri = Int32.Parse(reader.GetValue(5).ToString()) });
                            }
                            
                        }
                    }
                }
                
            }
            catch
            {
                using (StreamReader rdr = new StreamReader(Resources.vacanteString))
                {
                    while (rdr.Peek() >= 1)
                    {
                        var line = rdr.ReadLine().Split('|');
                        using (SqlConnection con = new SqlConnection(Resources.connectionString))
                        {
                            con.Open();
                            string cmdText = "Insert into Vacante values(@n,@d,@c,@p,@l)";

                            PictureBox pictureBox = new PictureBox();
                            try
                            {
                                pictureBox.ImageLocation = Resources.imaginiPath + line[0].Trim() + ".jpg";
                            }
                            catch
                            {
                                try
                                {
                                    pictureBox.ImageLocation = Resources.imaginiPath + line[0].Trim() + ".gif";
                                }
                                catch
                                {
                                    try
                                    {
                                        pictureBox.ImageLocation = Resources.imaginiPath + line[0].Trim() + ".bmp";
                                    }
                                    catch
                                    {
                                        pictureBox.ImageLocation = Resources.imaginiPath + "implicit.jpg";

                                    }
                                }
                            }
                            Console.WriteLine(pictureBox.ImageLocation);

                            using (SqlCommand cmd = new SqlCommand(cmdText, con))
                            {
                                cmd.Parameters.AddWithValue("n", line[0]);
                                cmd.Parameters.AddWithValue("d", line[1]);
                                cmd.Parameters.AddWithValue("c", pictureBox.ImageLocation);
                                cmd.Parameters.AddWithValue("p", line[2]);
                                cmd.Parameters.AddWithValue("l", line[3]);
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                }
            }
        }
        public static bool CheckUser(ref UserModel model)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Select * From Utilizatori where Email=@e and Parola=@p";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("e", model.Email);
                    cmd.Parameters.AddWithValue("p", model.Parola);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            rdr.Read();
                            model.Nume = rdr.GetString(1);
                            model.Prenume = rdr.GetString(2);
                            model.TipCont = Int32.Parse(rdr.GetValue(5).ToString());
                            return true;

                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }
        }
        public static void InsertUser(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Insert into Utilizatori values(@n,@p,@e,@pa,@t)";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("n", model.Nume);
                    cmd.Parameters.AddWithValue("p", model.Prenume);
                    cmd.Parameters.AddWithValue("e", model.Email);
                    cmd.Parameters.AddWithValue("pa", model.Parola);
                    cmd.Parameters.AddWithValue("t", model.TipCont);
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }

}
