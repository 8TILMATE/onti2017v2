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
        public static List<Models.RezervariModel> rezervaris= new List<RezervariModel>();
        public static List<Models.UserModel> userModels = new List<Models.UserModel>();
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
                            model.id= Int32.Parse(rdr.GetValue(0).ToString());
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
        public static void ListUsers()
        {
            UserModel model = new UserModel();  
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Select * From Utilizatori where TipCont=1";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (rdr.Read())
                            {
                                model.Nume = rdr.GetString(1);
                                model.Prenume = rdr.GetString(2);
                                model.Email= rdr.GetString(3);
                                model.Parola=rdr.GetString(4);
                                model.TipCont = Int32.Parse(rdr.GetValue(5).ToString());
                                model.id = Int32.Parse(rdr.GetValue(0).ToString());
                                userModels.Add(model);
                            }
                        }
                        catch
                        {
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
        public static void InsertRezervari(RezervariModel model)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Insert into Rezervari values(@Iv,@Iu,@Di,@Ds,@P)";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("Iv", model.IdVacanta);
                    cmd.Parameters.AddWithValue("Iu", model.IdUser);
                    cmd.Parameters.AddWithValue("Di", model.DataSt);
                    cmd.Parameters.AddWithValue("Ds", model.Datasf);
                    cmd.Parameters.AddWithValue("P", model.Pret);
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public static void RezervariUser(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Select * From Rezervari where IdUser=@I";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("I", model.id);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            rezervaris.Add(new RezervariModel { Id=Int32.Parse(rdr.GetValue(0).ToString()),IdVacanta= Int32.Parse(rdr.GetValue(1).ToString()),IdUser= Int32.Parse(rdr.GetValue(2).ToString()),DataSt=rdr.GetDateTime(3),Datasf=rdr.GetDateTime(4),Pret= Int32.Parse(rdr.GetValue(5).ToString()) });
                        }

                      
                    }
                }
            }
        }
        public static void DeleteFromRezervari(int IdVa)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Delete From Rezervari where IdVacanta=@I";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("I", IdVa);
                    cmd.ExecuteNonQuery();
                    
                }
            }
        }
        public static void ResetVacante()
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Delete From Vacante";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                   
                    cmd.ExecuteNonQuery();

                }
            }
        }
        public static void  AdminsUpdate(string email)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                string cmdText = "Update Utilizatori Set TipCont=0 WHERE email=@e";
                using (SqlCommand cmd = new SqlCommand(cmdText, con))
                {
                    cmd.Parameters.AddWithValue("e", email);
                    cmd.ExecuteNonQuery();

                }
            }
        }

    }

}
