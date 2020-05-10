using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PROJET_2CP.Noyau
{
    class NLeçon
    {

        // ajouter un panneaux ou une intersection ...
     public Image ajouterImage(string url)
        {   
            BitmapImage btm = new BitmapImage(new Uri(url+".png", UriKind.RelativeOrAbsolute));
            Image img = new Image();
            img.Source = btm;
            img.Stretch = Stretch.Fill;
            return img;
        }
     //recuperer la designation de la leçon
     public string designation(string IdLesson)
        {
            string designation="";
            //recuperation de la description du panneau à partir de la base de donnée 
            System.Data.SqlClient.SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [TypePanneaux] where Id='" + IdLesson + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    designation = dr["explication"].ToString();
                }
                dr.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                connection.Close();
            }
            return designation;
        }
        // recuperer le contenu de la leçon

    }
}
