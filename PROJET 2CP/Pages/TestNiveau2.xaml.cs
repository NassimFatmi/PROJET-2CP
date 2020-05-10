using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Interaction logic for TestNiveau2.xaml
    /// </summary>
   
        public partial class TestNiveau2 : Page
        {
            public static int langue { get; set; } = PROJET_2CP.MainWindow.langue;
            private string quest;
            private string propA;
            private string propB;
            private string propC;
            private string propD;
            private string bonnRep;
            private int tmp = 0;
            private int[] tab = new int[6];
            private int increment = 20;
            private DispatcherTimer dt;
            public static int nbBonneReponse = 0;          
            private bool tempEcoulé = true;
            

            // partie pour sauvegarde 
            private int _codeQst;
            private string _themeQst;

            private SoundPlayer _soundEffect;

            public TestNiveau2(int b1, int b2, int b3)
            {

            tab[0] = b1;
            tab[1] = b1 + 1;
            tab[2] = b2;
            tab[3] = b2 + 1;
            tab[4] = b3;
            tab[5] = b3 + 1;
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
                SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\questionBasset.mdf; Integrated Security = True");
                SqlCommand cmd = new SqlCommand("select * from [question] where Id='" + Convert.ToString(tab[tmp]) + "'", connection);
                SqlDataReader dr;
                try
                {
                    connection.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        _codeQst = Int32.Parse(dr["Id"].ToString());
                        _themeQst = dr["leçon"].ToString();
                        
                        if (langue == 0)
                        {
                            //  switch_lang.Content = "changer la langue en arabe";
                            quest = dr["contenu"].ToString();
                            propA = dr["reponse1"].ToString();
                            propB = dr["reponse2"].ToString();
                            propC = dr["reponse3"].ToString();
                            propD = dr["preponse4"].ToString();
                            bonnRep = dr["reponse1"].ToString();
                        }
                        if (langue == 1)
                        {
                            //switch_lang.Content = "تغيير اللغة الى الفرنسية";
                            quest = dr["contenuar"].ToString();
                            propA = dr["reponse1ar"].ToString();
                            propB = dr["reponse2ar"].ToString();
                            propC = dr["reponse3ar"].ToString();
                            propD = dr["reponse4ar"].ToString();
                            bonnRep = dr["reponse1ar"].ToString();
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

                //Afficher l'image du panneau
                
                
                    panneau.Visibility = Visibility.Collapsed;
                    propWrap.Width = 700;
                    propWrap.Height = 320;
                
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
    
            private void p1_Click(object sender, RoutedEventArgs e)
            {
                if ((string)((Button)sender).Tag == bonnRep)
                {
                    reaction.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png")));
                    p1.Foreground = Brushes.Green;
                    p1.BorderBrush = Brushes.Green;
                    nbBonneReponse++;
                    saveAnswer(true, 1, _codeQst, _themeQst);
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
                
                tempEcoulé = false;
            }

            private void p4_Click(object sender, RoutedEventArgs e)
            {
                if ((string)((Button)sender).Tag == bonnRep)
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
               
                tempEcoulé = false;
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                tmp++;
                tempEcoulé = true;
                if (tmp <= tab.Length)
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
                Home.mainFrame.Content = new Bilan(nbBonneReponse,6-  nbBonneReponse);

                }
                next.Visibility = Visibility.Collapsed;

            }

            private void quitter(object sender, RoutedEventArgs e)
            {
                Tests2.testActuel++;
                Home.mainFrame.Navigate(new Uri(";component/Pages/Tests2.xaml", UriKind.Relative));
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
            private void saveAnswer(bool reponse, int niveau, int code, string theme)
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
                        query = "INSERT INTO " + LogIN.LoggedUser.UtilisateurID + "Trace(Niveau,Theme,Test,Code,Reponse) VALUES('" + niveau.ToString() + "','" + theme + "', '' ,'" + code.ToString() + "','" + reponse + "')";
                        cmd = new SqlCommand(query, conn);

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    conn.Close();
                    MessageBox.Show("error save Db quiz Testniveau 2 ");
                }
            }
        }
}
