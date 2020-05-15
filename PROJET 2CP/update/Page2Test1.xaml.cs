using PROJET_2CP.Pages;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PROJET_2CP.update
{
    /// <summary>
    /// Logique d'interaction pour Page2Test1.xaml
    /// </summary>
    public partial class Page2Test1 : Page
    {
        private int langue { get; set; } = MainWindow.langue;
        private string quest;
        private string propA;
        private string propB;
        private string propC;
        private string propD;
        private string bonnRep;
        private int tag = 0;
        public static int tagMax { get; set; }
        private int increment = 20;
        private DispatcherTimer dt;
       // public static int nbBonneReponse { get; set; } = 0;
        private bool tempEcoulé = true;
        private int bi3;
        private int bs3;
        private int[] tab;

        public Page2Test1(int bi2, int bs2, int bi3, int bs3)
        {
            tagMax = bs2 - bi2;
            this.bi3 = bi3;
            this.bs3 = bs3;
            InitializeComponent();
            tab = Tests1.reorder(bi2, bs2);
            creerQuestion();
            afficherQuestion();
            Distimer();
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : ";
                votre_reponse.Text = "Votre réponse : ";
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة  ";
                votre_reponse.Text = "إجابتك";
            }
        }
        public void creerQuestion()
        {

            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [Quiz] where Id='" + Convert.ToString(tab[tag]) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (langue == 0)
                    {
                        Suivant.Text = "Passer à la question suivante";
                        quest = dr["questionFr"].ToString();
                     
                    }
                    if (langue == 1)
                    {
                        Suivant.Text = "السؤال التالي";
                        quest = dr["questionAr"].ToString();
                       
                    }
                    propA = dr["propAFr"].ToString();
                    propB = dr["propBFr"].ToString();
                    propC = dr["propCFr"].ToString();
                    propD = dr["propDFr"].ToString();
                    bonnRep = dr["bonneRepFr"].ToString();
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
            qst.Text = quest;
            int[] table = Tests1.reorder(1, 4);
            string[] propositions = new string[5];
            propositions[0] = propA;
            propositions[1] = propB;
            propositions[2] = propC;
            propositions[3] = propD;
            if (propositions[table[0] - 1] == "")
            {
                p1.Visibility = Visibility.Collapsed;
            }
            else
            {
                p1.Visibility = Visibility.Visible;
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + propositions[table[0]-1] + "_off.png", UriKind.RelativeOrAbsolute));
                Image p = new Image();
                p.Source = btm;
                p.Stretch = Stretch.Fill;
                p1.Content = p;
                p1.Tag = propositions[table[0] - 1];
            }
            if (propositions[table[1] - 1] == "")
            {
                p2.Visibility = Visibility.Collapsed;
            }
            else
            {
                p2.Visibility = Visibility.Visible;
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + propositions[table[1] - 1] + "_off.png", UriKind.RelativeOrAbsolute));
                Image p = new Image();
                p.Source = btm;
                p.Stretch = Stretch.Fill;
                p2.Content = p;
                p2.Tag = propositions[table[1] - 1];

            }
            if (propositions[table[2] - 1] == "")
            {
                p3.Visibility = Visibility.Collapsed;
            }
            else
            {
                p3.Visibility = Visibility.Visible;
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + propositions[table[2] - 1] + "_off.png", UriKind.RelativeOrAbsolute));
                Image p = new Image();
                p.Source = btm;
                p.Stretch = Stretch.Fill;
                p3.Content = p;
                p3.Tag = propositions[table[2] - 1];
            }
            if (propositions[table[3] - 1] == "")
            {
                p4.Visibility = Visibility.Collapsed;
            }
            else
            {
                p4.Visibility = Visibility.Visible;
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + propositions[table[3] - 1] + "_off.png", UriKind.RelativeOrAbsolute));
                Image p = new Image();
                p.Source = btm;
                p.Stretch = Stretch.Fill;
                p4.Content = p;
                p4.Tag = propositions[table[3] - 1];
            }
        }
        private void p1_Click(object sender, RoutedEventArgs e)
        {
            bon_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
            Page1Tests.total++;
            reaction.Visibility = Visibility.Visible;
           
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p1.Foreground = Brushes.Green;
                p1.BorderBrush = Brushes.Green;
                Page1Tests.nbBonneReponse++;
                votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Green;
            }
            else
            {
                saveAnswer(false, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p1.Foreground = Brushes.Red;
                p1.BorderBrush = Brushes.Red;
                votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + (string)((Button)sender).Tag + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Red;
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
            bon_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
            Page1Tests.total++;
            reaction.Visibility = Visibility.Visible;
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p2.Foreground = Brushes.Green;
                p2.BorderBrush = Brushes.Green;
                Page1Tests.nbBonneReponse++;
              votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Green;
            }
            else
            {
                saveAnswer(false, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p2.Foreground = Brushes.Red;
                p2.BorderBrush = Brushes.Red;
                votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + (string)((Button)sender).Tag + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Red;
            }
            p1.IsEnabled = false;
            p2.IsEnabled = false;
            p3.IsEnabled = false;
            p4.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
        }
        private void p3_Click(object sender, RoutedEventArgs e)
        {
            bon_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
            Page1Tests.total++;
            reaction.Visibility = Visibility.Visible;
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p3.Foreground = Brushes.Green;
                p3.BorderBrush = Brushes.Green;
                Page1Tests.nbBonneReponse++;
              votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Green;
            }
            else
            {
                saveAnswer(false, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p3.Foreground = Brushes.Red;
                p3.BorderBrush = Brushes.Red;
               votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + (string)((Button)sender).Tag + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Red;
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
            bon_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
            Page1Tests.total++;
            reaction.Visibility = Visibility.Visible;
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p4.Foreground = Brushes.Green;
                p4.BorderBrush = Brushes.Green;
                Page1Tests.nbBonneReponse++;
              votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + bonnRep + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Green;
            }
            else
            {
                saveAnswer(false, 1, 0, 0, "TestNiv1", "");
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                p4.Foreground = Brushes.Red;
                p4.BorderBrush = Brushes.Red;
               votre_panneau.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + (string)((Button)sender).Tag + "_off.png", UriKind.RelativeOrAbsolute));
                votre_reponse.Foreground = Brushes.Red;
            }
            p1.IsEnabled = false;
            p2.IsEnabled = false;
            p3.IsEnabled = false;
            p4.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tag++;
            tempEcoulé = true;
            reaction.Visibility = Visibility.Collapsed;
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
               Home.mainFrame.Content = new TestNiveau1p2(bi3, bs3);
            }
            next.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Save the answer in the DATABASE Save.mdf
        /// </summary>
        /// <param name="reponse"></param> is the answer true ?
        /// <param name="niveau"></param> level 
        /// <param name="theme"></param> numero de theme dans la bdd theme == test
        /// <param name="code"></param> Code  == Id
        /// <param name="reponseText"></param> Answer in frensh
        /// <param name="reponseTextAr"></param> answer in arabic
        private void saveAnswer(bool reponse, int niveau, int theme, int code, string reponseText, string reponseTextAr)
        {
            //pour les question des leçons
            // Code == ID //
            string connString = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf; Integrated Security = True";

            DataTable savedData = new DataTable();

            string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "Trace WHERE niveau = '" + niveau.ToString() + "' AND Code = '" + code.ToString() + "' AND Test = '" + theme.ToString() + "'";

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
                    query = "UPDATE " + LogIN.LoggedUser.UtilisateurID + "Trace SET Reponse='" + reponse + "' , ReponseText = '" + reponseText + "' , ReponseTextAr ='" + reponseTextAr + "'  WHERE niveau = '" + niveau.ToString() + "' AND ID = '" + code.ToString() + "'";
                    cmd = new SqlCommand(query, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    //Si l'apprenant n'a pas répondu a cette question on l'insert sa réponse
                    query = "INSERT INTO " + LogIN.LoggedUser.UtilisateurID + "Trace(Niveau,Test,Code,ReponseText,ReponseTextAr,Reponse) VALUES('" + niveau.ToString() + "', '" + theme.ToString() + "' ,'" + code.ToString() + "','" + reponseText + "','" + reponseTextAr + "','" + reponse + "')";
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
