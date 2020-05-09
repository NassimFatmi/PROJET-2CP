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
using PROJET_2CP.Noyau;

namespace PROJET_2CP.Niveau2
{
    /// <summary>
    /// Logique d'interaction pour MarquageAuSol.xaml
    /// </summary>
    public partial class MarquageAuSol : Page
    {
        private int langue = MainWindow.langue;
        private int bi;
        private int bs;
        private NLeçon lesson;
        private SoundPlayer splayer = null;
        private int index=229;

        public MarquageAuSol(int a,int b)
        {
            lesson = new NLeçon();
            bi = a;
            bs = b;
            InitializeComponent();
            configurerLangue();
            creerLesson();
        }
        private void configurerLangue()
        {
            BitmapImage btm = new BitmapImage(new Uri(";component/Images/229_off.png", UriKind.Relative));
            marquage.Source = btm;
            if (langue==1)
            {
                play.ToolTip = "تشغيل";
                stop.ToolTip = "إيقاف";
                backtxt.Text = "قبل";
                nexttxt.Text = "بعد";
                description.Text = "الخط الذي يحدد الحافة اليمنى للطريق هو خط الحافة. يبلغ طول الخط 3 أمتار وفاصل 3.5 متر. يمكنني عبور خط الشاطئ هذا لإيقاف السيارة أو التوقف على الجانب. يؤكد خط الحافة المستمر على يسار الممر أننا نسير في اتجاه واحد.";
            }

            if (langue == 0)
            {
                stop.ToolTip = "Stop";
                play.ToolTip = "Play";
                backtxt.Text = "precedent";
                nexttxt.Text = "suivant";
                description.Text = "La ligne délimitant le bord droit de la chaussée est une ligne de rive. La ligne a une distance de 3 mètres et un intervalle de 3.5 mètres. Je peux franchir cette ligne de rive pour me stationner ou m'arrêter sur le bas côté. La ligne de rive continue  sur la gauche de la voie confirme que nous circulons en sens unique.";
            }
        }
        private void creerLesson()
        {
            Button b = new Button();
            for (int i=bi;i<=bs;i++)
            {
                b.Height = 200;
                b.Width=160;
                b.Margin = new Thickness(10,0,10,0);
                b.Background = Brushes.Transparent;
                b.BorderBrush = null;
                b.Tag = i;
                b.Click += Button_Click;
                b.Content = lesson.ajouterImage(";component/Images/" + Convert.ToString(i) + "_off");
                sp.Children.Add(b);
                b = new Button();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            index = (int)((Button)sender).Tag;
            BitmapImage btm = new BitmapImage( new Uri(";component/Images/" + Convert.ToString(index)+ "_off.png", UriKind.Relative));
            marquage.Source = btm;

            System.Data.SqlClient.SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(index) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (langue == 1)
                    {
                        description.Text= dr["descriptionArab"].ToString();
                    }
                    if (langue == 0)
                    {
                        description.Text = dr["description"].ToString();
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

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (langue == 0)
            {
                splayer = new SoundPlayer(Convert.ToString(index) + "fr.wav");
            }

            if (langue == 1)
            {
                splayer = new SoundPlayer(Convert.ToString(index) + "ar.wav");
            }
            try
            {
                splayer.Play();
            }
            catch (Exception)
            {

            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            splayer.Stop();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (index >= 230)
            {
                index--;
                BitmapImage btm = new BitmapImage(new Uri(";component/Images/" + Convert.ToString(index) + "_off.png", UriKind.Relative));
                marquage.Source = btm;

                System.Data.SqlClient.SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(index) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (langue == 1)
                        {
                            description.Text = dr["descriptionArab"].ToString();
                        }
                        if (langue == 0)
                        {
                            description.Text = dr["description"].ToString();
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

        private void next_Click(object sender, RoutedEventArgs e)
        {
            if(index < 254)
            {
                index++;
                BitmapImage btm = new BitmapImage(new Uri(";component/Images/" + Convert.ToString(index) + "_off.png", UriKind.Relative));
                marquage.Source = btm;

                System.Data.SqlClient.SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(index) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (langue == 1)
                        {
                            description.Text = dr["descriptionArab"].ToString();
                        }
                        if (langue == 0)
                        {
                            description.Text = dr["description"].ToString();
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
