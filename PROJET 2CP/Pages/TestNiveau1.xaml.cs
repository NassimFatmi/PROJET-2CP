using System;
using System.Data.SqlClient;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Data;
using System.Data.SqlClient;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour TestNiveau1.xaml
    /// </summary>
    public partial class TestNiveau1 : Page
    {
        public static int langue { get; set; } = PROJET_2CP.MainWindow.langue;
        private string quest;
        private string propA;
        private string propB;
        private string propC;
        private string propD;
        private string bonnRep;
        private int tag;
        private int tagMax;
        private int[] tab;
        private int[] tab2;
        private int increment = 20;
        private DispatcherTimer dt;
        public static int nbBonneReponse = 0;
        private int hasImage;
        private int idImage;
        private SoundPlayer sp;
        private bool tempEcoulé = true;
        private int a;

        // partie pour sauvegarde 
        private int _codeQst;
        private string _themeQst;

        private SoundPlayer _soundEffect;

        public TestNiveau1(int bi,int bs,int[]t,int [] t2) 
        {
            tag = bi;
            a = bi;
            tagMax = bs;
            tab = t;
            tab2=t2;
            langue = 0;
            InitializeComponent();
            next.Visibility = Visibility.Collapsed;
           // configurerLaLangue();
            creerQuestion();
            afficherQuestion();
            Distimer();
       }
        public void creerQuestion()
        {
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [Quiz] where Id='" + Convert.ToString(tab[tag]) + "'", connection);
            SqlDataReader dr;
            try
            { connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _codeQst = Int32.Parse(dr["Id"].ToString());
                    _themeQst = dr["leçon"].ToString();
                    if (tab[tag] < 25)
                    {
                        hasImage = Convert.ToInt32(dr["hasImage"].ToString());
                        if (hasImage == 1)
                        {
                            idImage = Convert.ToInt32(dr["idImage"].ToString());
                        }

                    }
                        if (langue == 0)
                        {
                          //  switch_lang.Content = "changer la langue en arabe";
                            quest = dr["questionFr"].ToString();
                            propA = dr["propAFr"].ToString();
                            propB = dr["propBFr"].ToString();
                            propC = dr["propCFr"].ToString();
                            propD = dr["propDFr"].ToString();
                            bonnRep = dr["bonneRepFr"].ToString();
                        }
                        if (langue == 1)
                        {
                            //switch_lang.Content = "تغيير اللغة الى الفرنسية";
                            quest = dr["questionAr"].ToString();
                            propA = dr["propAAr"].ToString();
                            propB = dr["propBAr"].ToString();
                            propC = dr["propCAr"].ToString();
                            propD = dr["propDAr"].ToString();
                            bonnRep = dr["bonneRepAr"].ToString();
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
        private void afficherQuestion()
        {
            if (tab[tag] >= 25)
            {
                propWrap.Height = 320;
                propWrap.Width = 320;

                // definir la question
                panneau.Visibility = Visibility.Collapsed;
                qst.Text = quest;

                // Definier les proposition
                p1.Width = 140;
                p1.Height = 120;

                p2.Width = 140;
                p2.Height = 120;
                
                p3.Width = 140;
                p3.Height = 120;
                
                p4.Width = 140;
                p4.Height = 120;

                p1.Foreground = Brushes.White;
                p2.Foreground = Brushes.White;
                p3.Foreground = Brushes.White;
                p4.Foreground = Brushes.White;

                if (propA == "")
                {
                    p1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    p1.Visibility = Visibility.Visible;
                    BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + propA + "_off.png", UriKind.RelativeOrAbsolute));
                    Image p = new Image();
                    p.Source = btm;
                    p.Stretch = Stretch.Fill;
                    p1.Content = p;
                    p1.Margin = new Thickness(5);
                    p1.Tag = propA;
                }

                if (propB == "")
                {
                    p2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    p2.Visibility = Visibility.Visible;
                    BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + propB + "_off.png", UriKind.RelativeOrAbsolute));
                    Image p = new Image();
                    p.Source = btm;
                    p.Stretch = Stretch.Fill;
                    p2.Content = p;
                    p2.Margin = new Thickness(5);
                    p2.Tag = propB;

                }
                if (propC == "")
                {
                    p3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    p3.Visibility = Visibility.Visible;
                    BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + propC + "_off.png", UriKind.RelativeOrAbsolute));
                    Image p = new Image();
                    p.Source = btm;
                    p.Stretch = Stretch.Fill;
                    p3.Content = p;
                    p3.Margin = new Thickness(5);
                    p3.Tag = propC;
                }
                if (propD == "")
                {
                    p4.Visibility = Visibility.Collapsed;
                }
                else
                {
                    p4.Visibility = Visibility.Visible;
                    BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + propD + "_off.png", UriKind.RelativeOrAbsolute));
                    Image p = new Image();
                    p.Source = btm;
                    p.Stretch = Stretch.Fill;
                    p4.Content = p;
                    p4.Margin = new Thickness(5);
                    p4.Tag = propD;
                }
            }
            else
            {
                afficherQuestion2();
            }
        }
        private void afficherQuestion2()
        {

            //Afficher l'image du panneau
            if (hasImage == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + Convert.ToString(idImage) + "_off.png", UriKind.RelativeOrAbsolute));             
                panneau.Source = btm;
                panneau.Stretch = Stretch.Fill;
                panneau.Visibility = Visibility.Visible;
                propWrap.Height = 250;
                propWrap.Width = 700;
            }
            else
            {
                panneau.Visibility = Visibility.Collapsed;
                propWrap.Width = 700;
                propWrap.Height = 320;
            }
            // definir la question
            qst.Text = quest;
            // Definier les proposition
            p1.Width = 650;
            p1.Height = 50;

            p2.Width = 650;
            p2.Height = 50;
            
            p3.Width = 650;
            p3.Height = 50;
            
            p4.Width = 650;
            p4.Height = 50;

            p1.Foreground = Brushes.White;
            p2.Foreground = Brushes.White;
            p3.Foreground = Brushes.White;
            p4.Foreground = Brushes.White;

            if (propA == "")
            {
                p1.Visibility = Visibility.Collapsed;
            }
            else
            {
                p1.Visibility = Visibility.Visible;
                p1.Content = propA;
                p1.Tag = propA;
            }
            if (propB == "")
            {
                p2.Visibility = Visibility.Collapsed;
            }
            else
            {
                p2.Visibility = Visibility.Visible;
                p2.Content = propB;
                p2.Tag = propB;
                p2.Margin = new Thickness(5);
            }
            if (propC == "")
            {
                p3.Visibility = Visibility.Collapsed;
            }
            else
            {
                p3.Visibility = Visibility.Visible;
                p3.Content = propC;
                p3.Tag = propC;
                p3.Margin = new Thickness(5);
            }
            if (propD == "")
            {
                p4.Visibility = Visibility.Collapsed;
            }
            else
            {
                p4.Visibility = Visibility.Visible;
                p4.Content = propD;
                p4.Tag = propD;
                p4.Margin = new Thickness(5);
            }

        }
        /*
        private void configurerLaLangue()
        {
            if (langue == 0)
            {
                next.Content = "Passer à la question suivante";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                switch_lang.Content = "changer la langue en arabe";
            }
            if (langue == 1)
            {
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
               
            }
        }
        private void switch_lang_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (langue == 0 && !changed)
            {
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                langue = 1;
                HomePage.langue = 1;
                changed = true;
            }
            if (langue == 1 && !changed)
            {
                next.Content = "Passer à la question suivante";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                langue = 0;
                HomePage.langue = 0;
                changed = true;
            }
        }
        */
        private void p1_Click(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Tag == bonnRep)
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png")));
                p1.Foreground = Brushes.Green;
                p1.BorderBrush = Brushes.Green;
                nbBonneReponse++;
                saveAnswer(true,1,_codeQst,_themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            else
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png")));
                p1.Foreground = Brushes.Red;
                p1.BorderBrush = Brushes.Red;
                saveAnswer(false, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            p1.IsEnabled = false;
            p2.IsEnabled = false;
            p3.IsEnabled = false;
            p4.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            /* if(tag==tagMax)
           {
               Button b = new Button();
               if (langue == 0)
               {
                   b.Content = "Quitter";
               }
               b.HorizontalAlignment = HorizontalAlignment.Left;
               b.Margin = new Thickness(878, 543, 0, 0);
               b.VerticalAlignment = VerticalAlignment.Top;
               b.Width = 112;
               b.Height = 40;
               b.Click += quitter;
               gd.Children.Add(b);
           }*/
            tempEcoulé = false;
        }

        private void p2_Click(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Tag == bonnRep)
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png")));
                p2.Foreground = Brushes.Green;
                p2.BorderBrush = Brushes.Green;
                nbBonneReponse++;
                saveAnswer(true, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            else
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png")));
                p2.Foreground = Brushes.Red;
                p2.BorderBrush = Brushes.Red;
                saveAnswer(false, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            p1.IsEnabled = false;
            p2.IsEnabled = false;
            p3.IsEnabled = false;
            p4.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            /* if(tag==tagMax)
              {
                  Button b = new Button();
                  if (langue == 0)
                  {
                      b.Content = "Quitter";
                  }x
                  b.HorizontalAlignment = HorizontalAlignment.Left;
                  b.Margin = new Thickness(878, 543, 0, 0);
                  b.VerticalAlignment = VerticalAlignment.Top;
                  b.Width = 112;
                  b.Height = 40;
                  b.Click += quitter;
                  gd.Children.Add(b);
              }*/
            tempEcoulé = false;
        }

        private void p4_Click(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Tag==bonnRep)
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png")));
                p4.Foreground = Brushes.Green;
                p4.BorderBrush = Brushes.Green;
                nbBonneReponse++;
                saveAnswer(true, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();

            }
            else
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png")));
                p4.Foreground = Brushes.Red;
                p4.BorderBrush = Brushes.Red;
                saveAnswer(false, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            p1.IsEnabled = false;
            p2.IsEnabled = false;
            p3.IsEnabled = false;
            p4.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            /* if(tag==tagMax)
               {
                   Button b = new Button();
                   if (langue == 0)
                   {
                       b.Content = "Quitter";
                   }
                   b.HorizontalAlignment = HorizontalAlignment.Left;
                   b.Margin = new Thickness(878, 543, 0, 0);
                   b.VerticalAlignment = VerticalAlignment.Top;
                   b.Width = 112;
                   b.Height = 40;
                   b.Click += quitter;
                   gd.Children.Add(b);
               }*/
            tempEcoulé = false;
           
        }

        private void p3_Click(object sender, RoutedEventArgs e)
        {
            if ((string)((Button)sender).Tag == bonnRep)
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png")));
                p3.Foreground = Brushes.Green;
                p3.BorderBrush = Brushes.Green;
                nbBonneReponse++;
                saveAnswer(true, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            else
            {
                reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png")));
                p3.Foreground = Brushes.Red;
                p3.BorderBrush = Brushes.Red;
                saveAnswer(false, 1, _codeQst, _themeQst);
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            p1.IsEnabled = false;
            p2.IsEnabled = false;
            p3.IsEnabled = false;
            p4.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
           /* if(tag==tagMax)
            {
                Button b = new Button();
                if (langue == 0)
                {
                    b.Content = "Quitter";
                }
                b.HorizontalAlignment = HorizontalAlignment.Left;
                b.Margin = new Thickness(878, 543, 0, 0);
                b.VerticalAlignment = VerticalAlignment.Top;
                b.Width = 112;
                b.Height = 40;
                b.Click += quitter;
                gd.Children.Add(b);
            }*/
            tempEcoulé = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tag++;
            tempEcoulé = true;
            if (tag <= tagMax)
            {
                p1.IsEnabled = true;
                p2.IsEnabled = true;
                p3.IsEnabled = true;
                p4.IsEnabled = true;
                p1.BorderBrush = Brushes.Transparent;
                p2.BorderBrush = Brushes.Transparent;
                p3.BorderBrush = Brushes.Transparent;
                p4.BorderBrush = Brushes.Transparent;
                p1.Foreground = Brushes.Black;
                p2.Foreground = Brushes.Black;
                p3.Foreground = Brushes.Black;
                p4.Foreground = Brushes.Black;
                creerQuestion();
                afficherQuestion();
                increment = 20;
                timer.Visibility = Visibility.Visible;
                increment = 20;
            }
            else
            {
                Home.mainFrame.Content = new TestNiveau1p2(a, tagMax, tab2);
            
            }
            next.Visibility = Visibility.Collapsed;
           
        }

        private void quitter(object sender, RoutedEventArgs e)
        {
            Tests1.testActuel++;
            Home.mainFrame.Navigate(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Pages/Tests1.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Distimer()
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += thick;
            dt.Start();

        }
        private void thick(object sender, EventArgs e)
        {
            if (increment == 20)
            {
                timer.Foreground = Brushes.Green;
            }
            if (increment == 10)
            {
                timer.Foreground = Brushes.Orange;
            }
            if (increment == 5)
            {
                timer.Foreground = Brushes.Red;
            }
            if (!tempEcoulé)
            {
                timer.Content = "";
            }
            else
            {
                timer.Content = increment.ToString();
            }
            if (increment == 0 && tempEcoulé)
            {
                if (propB == bonnRep)
                {
                    p2.Foreground = Brushes.Green;
                }
                if (propC == bonnRep)
                {
                    p3.Foreground = Brushes.Green;
                }
                if (propA == bonnRep)
                {
                    p1.Foreground = Brushes.Green;
                }
                if (propD == bonnRep)
                {
                    p4.Foreground = Brushes.Green;
                }
                //Calcul de stats
                p1.IsEnabled = false;
                p2.IsEnabled = false;
                p3.IsEnabled = false;
                p4.IsEnabled = false;
                next.Visibility = Visibility.Visible;
                timer.Visibility = Visibility.Collapsed;
                tempEcoulé = false;
            }
            increment--;
        }

        /// <summary>
        /// partie pour le sauvegrade des reponses pour construire les statistiques 
        /// </summary>
        /// <param name="reponse"></param> la reponse de l'apprenant
        /// <param name="niveau"></param> niveau de la qst
        /// <param name="code"></param> id ou le code pour la referance
        /// <param name="theme"></param> le theme que la qst apartient
        private void saveAnswer(bool reponse , int niveau , int code , string theme)
        {
            // Code == ID //
            string connString = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf; Integrated Security = True";

            DataTable savedData = new DataTable();

            string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "Trace WHERE niveau = '" + niveau.ToString() + "' AND ID = '" + code.ToString() + "'";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(savedData);
                da.Dispose();

                if (savedData.Rows.Count == 1)
                {
                    // Si l'apprenant a répondu a cette question on fait la maj dans sa Table dans Save BDD
                    query = "UPDATE " + LogIN.LoggedUser.UtilisateurID + "Trace SET Reponse='" + reponse + "' WHERE niveau = '" + niveau.ToString() + "' AND ID = '" + code.ToString() + "'";
                    cmd = new SqlCommand(query, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {

                    //Si l'apprenant n'a pas répondu a cette question on l'insert sa réponse
                    query = "INSERT INTO " + LogIN.LoggedUser.UtilisateurID + "Trace(Niveau,Theme,Test,Code,Reponse) VALUES('" + niveau.ToString() + "','" + theme + "', '' ,'"+ code.ToString() + "','" + reponse +"')";
                    cmd = new SqlCommand(query, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                MessageBox.Show("error save Db quiz Testniveau 1 ");
            }
        }
    }
}
