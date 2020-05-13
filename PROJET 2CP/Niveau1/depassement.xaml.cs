﻿using PROJET_2CP.Noyau;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROJET_2CP
{
    /// <summary>
    /// Logique d'interaction pour depassement.xaml
    /// </summary>
    public partial class depassement : Page
    {
        public static int num;
        private NLeçon lesson;
        public depassement()
        {
            lesson = new NLeçon();
            InitializeComponent();
            creerPanneau();
            num = 1;
            img.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Img/1_8");
            Explication.Content = "La voiture verte peut dépasser la voiture rouge, tandis que la voiture jaune ne peut pas dépasser à la troisième piste et ne peut pas suivre la voiture verte, elle doit tourner à droite.";
        }
        private void creerPanneau()
        {
            int k = 1;
            Button btn = new Button();
            StackPanel sp = new StackPanel();
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Center;
            for (int i = 21; i < 31; i++)
            {
                SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [IntersectionsLçn] where Id='" + Convert.ToString(i) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        btn.HorizontalAlignment = HorizontalAlignment.Left;
                        btn.FontSize = 24;
                        btn.Width = 90;
                        btn.Height = 90;
                        btn.Margin = new Thickness(20, 20, 20, 20);
                        btn.Background = Brushes.Transparent;
                        //btn.MouseEnter += button_MouseEnter;
                        //btn.MouseLeave += button_MouseLeave;
                        btn.Click += button_Click;
                        btn.Tag = i;
                        btn.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Img/" + dr["nom"].ToString());

                        sp.Children.Add(btn);
                        if (k == 5)
                        {
                            k = 0;
                            stk.Children.Add(sp);
                            sp = new StackPanel();
                            sp.Orientation = Orientation.Horizontal;
                            sp.HorizontalAlignment = HorizontalAlignment.Center;


                        }

                        btn = new Button();
                        k++;
                    }
                    dr.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            stk.Children.Add(sp);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var index = (int)button.Tag;
            num = (int)button.Tag;
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [IntersectionsLçn] where Id='" + Convert.ToString((int)button.Tag) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Explication.Content = dr["Explication"].ToString();
                    img.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Img/" + dr["nom"].ToString());


                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void suivant_Click(object sender, RoutedEventArgs e)
        {


            num++;
            if (num == 26)
                num = 21;
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [IntersectionsLçn] where Id='" + Convert.ToString(num) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Explication.Content = dr["Explication"].ToString();
                    img.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Img/" + dr["nom"].ToString());


                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void precedent_Click(object sender, RoutedEventArgs e)
        {


            num--;
            if (num == 20)
                num = 25;
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [IntersectionsLçn] where Id='" + Convert.ToString(num) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Explication.Content = dr["Explication"].ToString();
                    img.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Img/" + dr["nom"].ToString());


                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void son_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(Convert.ToString(num) + ".wav");
            sp.Play();
        }
        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Leçons();
        }
    }
}