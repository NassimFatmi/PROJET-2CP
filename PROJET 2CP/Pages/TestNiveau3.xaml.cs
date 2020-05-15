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
    /// Interaction logic for TestNiveau3.xaml
    /// </summary>
    public partial class TestNiveau3 : Page
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
        public static int nbReponse;

        // partie pour sauvegarde 
        private int _codeQst;
        private string _themeQst;

        private int _Code;//ID
        private string _Reponse;
        private string _ReponseAR;

        private string _Reponse1;
        private string _Reponse2;
        private string _Reponse3;
        private string _Reponse4;

        private string _Reponse1AR;
        private string _Reponse2AR;
        private string _Reponse3AR;
        private string _Reponse4AR;

        private SoundPlayer _soundEffect;

        public TestNiveau3(int b1, int b2, int b3)
        {

            tab[0] = b1;
            tab[1] = b1 + 1;
            tab[2] = b2;
            tab[3] = b2 + 1;
            tab[4] = b3;
            tab[5] = b3 + 1;
            langue = 0;
            shufflearray(tab);
            InitializeComponent();

            next.Visibility = Visibility.Collapsed;
            // configurerLaLangue();
            creerQuestion();
            afficherQuestion();
            Distimer();
        }
        public static void shufflearray(int[] tab)
        {
            int n = tab.Length;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                swap(tab, i, i + rand.Next(n - i));
            }
        }
        public static void swap(int[] tab, int a, int b)
        {
            int temp = tab[a];
            tab[a] = tab[b];
            tab[b] = temp;
        }
        private static int[] initArray(int j, int k)
        {
            int[] arr = new int[j];
            Random rnd = new Random();
            int tmp;
            for (int i = 0; i < arr.Length; i++)
            {
                tmp = rnd.Next(k);
                while (IsDup(tmp, arr))
                {
                    tmp = rnd.Next(k);
                }
                arr[i] = tmp;
            }
            return arr;
        }
        private static bool IsDup(int tmp, int[] arr)
        {
            foreach (var item in arr)
                if (item == tmp)
                {
                    return true;
                }
            return false;
        }
        public void creerQuestion()
        {
            SqlConnection connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("select * from [Question] where Id='" + Convert.ToString(tab[tmp]) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _codeQst = Int32.Parse(dr["Id"].ToString());
                    _themeQst = "";

                    _Code = Int32.Parse(dr["Id"].ToString());

                    int[] arr = new int[4];
                    Random aleatoire = new Random();
                    arr = initArray(4, 5);

                    if (MainWindow.langue == 0)
                    {

                        propA = dr["reponse" + Convert.ToString(arr[0])].ToString();

                        propB = dr["reponse" + Convert.ToString(arr[1])].ToString();

                        propC = dr["reponse" + Convert.ToString(arr[2])].ToString();

                        propD = dr["reponse" + Convert.ToString(arr[3])].ToString();
                        quest = dr["contenu"].ToString();
                        bonnRep = dr["reponse1"].ToString();
                    }
                    if (MainWindow.langue == 1)
                    {

                        propA = dr["reponse" + Convert.ToString(arr[0]) + "ar"].ToString();

                        propB = dr["reponse" + Convert.ToString(arr[1]) + "ar"].ToString();

                        propC = dr["reponse" + Convert.ToString(arr[2]) + "ar"].ToString();

                        propD = dr["reponse" + Convert.ToString(arr[3]) + "ar"].ToString();
                        quest = dr["contenuar"].ToString();
                        bonnRep = dr["reponse1ar"].ToString();
                    }

                    _Reponse1 = "reponse" + Convert.ToString(arr[0]);
                    _Reponse2 = "reponse" + Convert.ToString(arr[1]);
                    _Reponse3 = "reponse" + Convert.ToString(arr[2]);
                    _Reponse4 = "reponse" + Convert.ToString(arr[3]);

                    _Reponse1AR = "reponse" + Convert.ToString(arr[0]) + "ar";
                    _Reponse2AR = "reponse" + Convert.ToString(arr[1]) + "ar";
                    _Reponse3AR = "reponse" + Convert.ToString(arr[2]) + "ar";
                    _Reponse4AR = "reponse" + Convert.ToString(arr[3]) + "ar";
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
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 3, 0, _Code, _Reponse1, _Reponse1AR);

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));
                nbBonneReponse++;

                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            else
            {
                saveAnswer(false, 3, 0, _Code, _Reponse1, _Reponse1AR);

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + p1.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + p1.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;
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
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 3, 0, _Code, _Reponse2, _Reponse2AR);

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));
                nbBonneReponse++;
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            else
            {
                saveAnswer(false, 3, 0, _Code, _Reponse2, _Reponse2AR);

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + p2.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + p2.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;
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
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 3, 0, _Code, _Reponse4, _Reponse4AR);

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));
                nbBonneReponse++;
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();

            }
            else
            {
                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + p4.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + p4.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;
                saveAnswer(false, 3, 0, _Code, _Reponse4, _Reponse4AR);
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
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string)((Button)sender).Tag == bonnRep)
            {
                saveAnswer(true, 3, 0, _Code, _Reponse3, _Reponse3AR);

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));
                nbBonneReponse++;
                _soundEffect = new SoundPlayer($@"{System.IO.Directory.GetCurrentDirectory()}\SoundsEffects\correct_effect.wav");
                _soundEffect.Play();
            }
            else
            {
                saveAnswer(false, 3, 0, _Code, _Reponse3, _Reponse3AR);

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + p3.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + p3.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;
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
            if (tmp < tab.Length)
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

                Home.mainFrame.Content = new Bilan3(nbBonneReponse, 6 - nbBonneReponse);

            }
            next.Visibility = Visibility.Collapsed;

        }

        private void quitter(object sender, RoutedEventArgs e)
        {
            Tests3.testActuel++;
            Home.mainFrame.Navigate(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Pages/Tests3.xaml", UriKind.RelativeOrAbsolute));
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
