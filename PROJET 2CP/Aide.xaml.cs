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
            Expander ex = new Expander();
            ex.Header = new TextBlock();
            for(int i=1;i<=7;i++)
            {
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
                           // ex.Text = dr["questionAr"].ToString();
                            ex.Header= dr["repAr"].ToString();
                        }
                        if (MainWindow.langue == 0)
                        {
                            //ex.Text = dr["questionFr"].ToString();
                            ex.Header = dr["repFr"].ToString();
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
            }
        }
    }
}
