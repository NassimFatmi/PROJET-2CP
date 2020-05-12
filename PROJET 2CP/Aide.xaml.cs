using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
    /// Logique d'interaction pour Aide.xaml
    /// </summary>
    public partial class Aide : Page
    {
        public Aide()
        {
            InitializeComponent();
            CreerHelper();
        }
        void CreerHelper()
        {
            Expander ex;
            TextBlock t;
            Grid g;
            TextBlock t2 ;
            SolidColorBrush color = new SolidColorBrush();
            SolidColorBrush color2 = new SolidColorBrush();
            color.Color = Color.FromArgb(40, 0, 191, 255);
            color2.Color = Color.FromArgb(20, 135, 206, 235);
            for (int i=1;i<=10;i++)
            {
                 ex = new Expander();
                 t = new TextBlock();
                 g = new Grid();
                t2 = new TextBlock();
                System.Data.SqlClient.SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ={System.IO.Directory.GetCurrentDirectory()}\Aide.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(i) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (MainWindow.langue == 1)
                        {
                            t.Text = dr["questionAr"].ToString();
                            t2.Text = dr["repAr"].ToString();
                        }
                        if (MainWindow.langue == 0)
                        {
                           t.Text = dr["questionFr"].ToString();
                            t2.Text = dr["repFr"].ToString();
                        }

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
                g.Background = color2;
                t2.TextWrapping = TextWrapping.Wrap;
                t2.FontSize = 16;
                t2.Foreground = Brushes.White;
                t2.Margin = new Thickness(20);
                t2.HorizontalAlignment = HorizontalAlignment.Center;
                g.Children.Add(t2);
                ex.Width = 700;
                ex.FontSize = 20;
                ex.HorizontalAlignment = HorizontalAlignment.Center;
                ex.Margin = new Thickness(0, 10, 0, 10);
                ex.Background = color;

                t.TextWrapping = TextWrapping.Wrap;
                t.Foreground = Brushes.White;
                t.FontWeight = FontWeights.Bold;
                t.HorizontalAlignment = HorizontalAlignment.Center;
                t.Margin = new Thickness(10);
                ex.Content = g;
                ex.Header = t;
                sp.Children.Add(ex);
            }
        }
    }
}
