using System;
using System.Data.SqlClient;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PROJET_2CP.Niveau2;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour DecrirePanneau.xaml
    /// </summary>
    public partial class DecrirePanneau : Page
    {
        private int i = 0;
        private int langue = MainWindow.langue;
        private SoundPlayer sp = null;
        /*
         Si lastPage == 1 alors lastPage est une Panneaux
         Si == 2 est une PanneauxInterdiction
         Si == 3 est une PanneauxObligation
             */
        public static int lastPage;

        private int _bi = 0;
        private int _bs = 0;
        private string _panneau;
        public DecrirePanneau(int a,int b ,string panneau )
        {
            InitializeComponent();

            _bi = a;
            _bs = b;
            _panneau = panneau;

            AfficherPanneauEtDescription();
            buttonNextEtBack();
        }
        public void AfficherPanneauEtDescription()
        {
            if ( _panneau == "danger" )
            {
                i = Panneaux.panneau;
            }
            if (_panneau == "interdiction")
            {
                i = PanneauxInterdiction.panneau;
            }
            if (_panneau == "obligation")
            {
                i = PanneauxObligation.panneau;
            }
            if(_panneau == "signalisation2")
            {
                i = LessonContent.panneau;
            }

            //Afficher l'image du panneau
            BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + Convert.ToString(i) + "_off.png", UriKind.RelativeOrAbsolute));
            pano.Source = btm;
            pano.Stretch = Stretch.Fill;
            //recuperation de la description du panneau à partir de la base de donnée 
            System.Data.SqlClient.SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ={System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(i) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (langue == 1)
                    {
                        describe.Text = dr["descriptionArab"].ToString();
                      //  describe.HorizontalContentAlignment = HorizontalAlignment.Right;
                        audio.ToolTip = "تشغيل";
                        stop.ToolTip = "إيقاف";
                        switch_lang.Content = "تغيير اللغة الى الفرنسية";
                    }
                    if (langue == 0)
                    {
                        switch_lang.Content = "changer la langue en arabe";
                        describe.Text = dr["description"].ToString();
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
        public void buttonNextEtBack()
        {
            // back button
            back.Click += back_Click;
            //next button
            next.Click += next_Click;
        }
        // run audio
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (langue == 0)
            {
               sp= new SoundPlayer(@"sounds\" + Convert.ToString(i)+".wav");
            }

            if (langue == 1)
            {
             sp = new SoundPlayer(@"sounds\" + Convert.ToString(i) + "ar.wav");
            }

            try
            {
                sp.Play();
            }
            catch (Exception)
            {

            }
        }
        private void switch_lang_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (langue == 0 && !changed)
            {
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                langue = 1;
                MainWindow.langue = 1;
                changed = true;
            }
            if (langue == 1 && !changed)
            {
                MainWindow.langue = 0;
                switch_lang.Content = "changer la langue en arabe";
                langue = 0;
                changed = true;
            }
            AfficherPanneauEtDescription();
        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (i >= (_bi+1))
            {
                i--;
                //Afficher l'image du panneau
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + Convert.ToString(i) + "_off.png", UriKind.RelativeOrAbsolute));
                pano.Source = btm;
                pano.Stretch = Stretch.Fill;
                //recuperation de la description du panneau à partir de la base de donnée 
                SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(i) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (langue == 1)
                        {
                            describe.Text = dr["descriptionArab"].ToString();
                           // describe.HorizontalContentAlignment = HorizontalAlignment.Right;
                            audio.ToolTip = "تشغيل";
                            stop.ToolTip = "إيقاف";
                        }
                        if (langue == 0)
                        {
                            stop.ToolTip = "Stop";
                            audio.ToolTip = "Play";
                            describe.Text = dr["description"].ToString();
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
            if (i <= (_bs-1))
            {
                i++;
                //Afficher l'image du panneau
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + Convert.ToString(i) + "_off.png", UriKind.RelativeOrAbsolute));
                pano.Source = btm;
                pano.Stretch = Stretch.Fill;
                //recuperation de la description du panneau à partir de la base de donnée 
                SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [Table] where Id='" + Convert.ToString(i) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (langue == 1)
                        {
                            describe.Text = dr["descriptionArab"].ToString();
                            //describe.HorizontalContentAlignment = HorizontalAlignment.Right;
                            audio.ToolTip = "تشغيل";
                            stop.ToolTip = "إيقاف";
                        }
                        if (langue == 0)
                        {
                            stop.ToolTip = "Stop";
                            audio.ToolTip = "Play";
                            describe.Text = dr["description"].ToString();
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
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            sp.Stop();
        }

        private void backClickTopanneau(object sender, RoutedEventArgs e)
        {
            if (lastPage == 1)
                Home.mainFrame.Content = new Panneaux();
            if (lastPage == 2)
                Home.mainFrame.Content = new PanneauxInterdiction();
            if (lastPage == 3)
                Home.mainFrame.Content = new PanneauxObligation();
        }
    }
}
