using System;
using System.Data;
using System.Data.SqlClient;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PROJET_2CP.Niveau3
{
    /// <summary>
    /// Logique d'interaction pour Quiz.xaml
    /// </summary> 
    public partial class Quiz : Page
    {
        private SoundPlayer sp;
        private string quest;
        private string propA;
        private string propB;
        private string propC;
        private string propD="";
        private string bonnRep;
        private int[] tab = new int[6];

        private int tmp;
        private int tag;
        private int tagMax;
        private int increment = 20;
        private DispatcherTimer dt;
        private int nbBonneReponse = 0;
        private bool tempEcoulé = true;
        //private  string repInteractive;

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

        public Quiz(int bi, int bs)
        {
            InitializeComponent();
            
            if(MainWindow.langue == 0)
                Back.Text = "Retour";
            else
                Back.Text = "عودة";

            tmp = 0;
            tag = bi;
            tagMax = bs;
            tab = initArray(6,7);
            next.Visibility = Visibility.Collapsed;
            creerQuestion();
            afficherQuestion();
            Distimer();
        }
        private static int[] initArray(int j ,int k)
        {
            int[] arr = new int[j];
            Random rnd = new Random();
            int tmp;
            for(int i=0;i<arr.Length;i++)
            {
                tmp = rnd.Next(k);
                while(IsDup(tmp,arr))
                {
                    tmp = rnd.Next(k);
                }
                arr[i] = tmp;
            }
            return arr;
        }
        private static bool IsDup(int tmp,int[] arr)
        {
            foreach(var item in arr)
                if(item == tmp)
                {
                    return true;
                }
            return false;
        }
        public void creerQuestion()
        {
            //int tag = aleaInt();
            //recuperation de la description du panneau à partir de la base de donnée 
            SqlConnection connection = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("select * from [Question] where Id='" + Convert.ToString(tab[tmp]+tag-1) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    int[] arr = new int[4];

                    _Code = Int32.Parse(dr["Id"].ToString());

                    // repInteractive =" bravo";
                    Random aleatoire = new Random();
                    arr = initArray(4,5);
                    if (MainWindow.langue == 0)
                    {
                        suivant.Text = "Passer à la question suivante";
                        propA = dr["reponse" + Convert.ToString(arr[0])].ToString();

                        propB = dr["reponse" + Convert.ToString(arr[1])].ToString();

                        propC = dr["reponse" + Convert.ToString(arr[2])].ToString();

                        propD = dr["reponse" + Convert.ToString(arr[3])].ToString();
                        quest = dr["contenu"].ToString();
                        bonnRep = dr["reponse1"].ToString();
                    }
                    if(MainWindow.langue ==1)
                    {
                        suivant.Text = "المرور الى السؤال الموالي";
                        propA = dr["reponse" + Convert.ToString(arr[0])+"ar"].ToString();

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
        public static int aleaInt()
        {
            Random aleatoire = new Random();
            return aleatoire.Next(1, 6);
        }
        private void afficherQuestion()
        {
            if (MainWindow.langue == 0)
            {
                SoundPlayer sp = new SoundPlayer(@"Soundsbst\"+Convert.ToString(tab[tmp] + tag - 1) + ".wav");
                try { sp.Play(); } catch (Exception) { }
            }
            if(MainWindow.langue ==1)
            {
                SoundPlayer sp = new SoundPlayer(@"Soundsbst\"+Convert.ToString(tab[tmp] + tag - 1) + "AR.wav");
                try { sp.Play(); } catch (Exception) { }
            }

            // definir la question
            question.Text = quest;
            // Definier les proposition
            if (propA == "")
            {
                repA.Visibility = Visibility.Collapsed;
            }
            else
            {
                repA.Visibility = Visibility.Visible;
                repA.Content = propA;
            }
            if (propB == "")
            {
                repB.Visibility = Visibility.Collapsed;
            }
            else
            {
                repB.Visibility = Visibility.Visible;
                repB.Content = propB;
            }
            if (propC == "")
            {
                repC.Visibility = Visibility.Collapsed;
            }
            else
            {
                repC.Visibility = Visibility.Visible;
                repC.Content = propC;
            }
            if (propD == "")
            {
                repD.Visibility = Visibility.Collapsed;
            }
            else
            {
                repD.Visibility = Visibility.Visible;
                repD.Content = propD;
            }

        }
        
        private void next_Click(object sender, RoutedEventArgs e)
        {
            tmp++;
            SoundPlayer soundPlayer;
            //tag++;
            tempEcoulé = true;
            //if (tag >= tagMax)
            if(tmp>=tab.Length)
            {
                repA.Visibility = Visibility.Collapsed;
                repB.Visibility = Visibility.Collapsed;
                repC.Visibility = Visibility.Collapsed;
                repD.Visibility = Visibility.Collapsed;
                question.Visibility = Visibility.Collapsed;
                bilanQuiz.Visibility = Visibility.Visible;
                lbl.Content = nbBonneReponse.ToString() + " / 6";

                if (MainWindow.langue == 0)
                    remarque.Content = "Remarques";
                else
                    remarque.Content = "ملاحظات ";

                if (nbBonneReponse < 3)
                {
                    if (MainWindow.langue == 0)
                        quizMessage.Content = "Revoir le cour";
                    else
                        quizMessage.Content = "اعد مراجعة الدرس ";
                    try
                    {
                        reactionBilan.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/bad.png", UriKind.RelativeOrAbsolute));
                        soundPlayer = new SoundPlayer(@"SoundsEffects\disappointment.wav");
                        soundPlayer.Play();
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    if (MainWindow.langue == 0)
                        quizMessage.Content = "Bravo !";
                    else
                        quizMessage.Content = "احسنت";
                    Random random = new Random();
                    int rnd = random.Next(2);
                    try
                    {
                        if (rnd == 0)
                            reactionBilan.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/200iq.jpg", UriKind.RelativeOrAbsolute));
                        else
                            reactionBilan.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/brain.png", UriKind.RelativeOrAbsolute));

                        soundPlayer = new SoundPlayer(@"SoundsEffects\clap.wav");
                        soundPlayer.Play();
                    }
                    catch (Exception)
                    {

                    }
                }
                dt.Stop();
                timer.Visibility = Visibility.Collapsed;
            }
            else
            {
                creerQuestion();
                afficherQuestion();
                repA.IsEnabled = true;
                repB.IsEnabled = true;
                repC.IsEnabled = true;
                repD.IsEnabled = true;
                repA.Foreground = Brushes.White;
                repB.Foreground = Brushes.White;
                repC.Foreground = Brushes.White;
                repD.Foreground = Brushes.White;
                timer.Visibility = Visibility.Visible;
                increment = 20;
            }
            next.Visibility = Visibility.Collapsed;
        }

        private void repA_Click(object sender, RoutedEventArgs e)
        {
            //Calcul de stats
            //sauvgarder la question et la bonne reponse et la reponse de l'utilisateur
            if (propA == bonnRep)
            {
                saveAnswer(true, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse1, _Reponse1AR);

                repA.Foreground = Brushes.Green;
                nbBonneReponse++;
                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;
                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/happy.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                saveAnswer(false, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse1, _Reponse1AR);

                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/sad.png", UriKind.RelativeOrAbsolute));

                reponseNext.Text = "Mauvaise reponse";
                reponseNext.Foreground = Brushes.Red;
                repA.Foreground = Brushes.Red;
                if (propB == bonnRep)
                {
                    repB.Foreground = Brushes.Green;
                }
                if (propC == bonnRep)
                {
                    repC.Foreground = Brushes.Green;
                }
                if (propD == bonnRep)
                {
                    repD.Foreground = Brushes.Green;
                }

            }
            repB.IsEnabled = false;
            repC.IsEnabled = false;
            repD.IsEnabled = false;
            repA.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
        }

        private void repB_Click(object sender, RoutedEventArgs e)
        {
            if (propB == bonnRep)
            {
                saveAnswer(true, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse2, _Reponse2AR);

                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;

                nbBonneReponse++;
                repB.Foreground = Brushes.Green;

                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/happy.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                saveAnswer(false, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse2, _Reponse2AR);

                reponseNext.Text = "Mauvaise reponse";
                reponseNext.Foreground = Brushes.Red;
                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/sad.png", UriKind.RelativeOrAbsolute));
                repB.Foreground = Brushes.Red;
                if (propA == bonnRep)
                {
                    repA.Foreground = Brushes.Green;
                }
                if (propC == bonnRep)
                {
                    repC.Foreground = Brushes.Green;
                }
                if (propD == bonnRep)
                {
                    repD.Foreground = Brushes.Green;
                }
                //Calcul de stats
            }
            repA.IsEnabled = false;
            repC.IsEnabled = false;
            repB.IsEnabled = false;
            repD.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
        }

        private void repC_Click(object sender, RoutedEventArgs e)
        {
            if (propC == bonnRep)
            {
                saveAnswer(true, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse3, _Reponse3AR);

                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;

                nbBonneReponse++;
                repC.Foreground = Brushes.Green;

                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/happy.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                saveAnswer(false, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse3, _Reponse3AR);

                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/sad.png", UriKind.RelativeOrAbsolute));

                reponseNext.Text = "Mauvaise reponse";
                reponseNext.Foreground = Brushes.Red;

                repC.Foreground = Brushes.Red;
                if (propA == bonnRep)
                {
                    repA.Foreground = Brushes.Green;
                }
                if (propB == bonnRep)
                {
                    repB.Foreground = Brushes.Green;
                }
                if (propD == bonnRep)
                {
                    repD.Foreground = Brushes.Green;
                }
                //Calcul de stats
            }
            repB.IsEnabled = false;
            repA.IsEnabled = false;
            repD.IsEnabled = false;
            repC.IsEnabled = false;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
        }

        private void repD_Click(object sender, RoutedEventArgs e)
        {
            if (propD == bonnRep)
            {
                saveAnswer(true, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse4, _Reponse4AR);

                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;
                nbBonneReponse++;
                repD.Foreground = Brushes.Green;

                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/happy.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                saveAnswer(false, 3, Niveau3.niveau3ThemeSelected, _Code, _Reponse4, _Reponse4AR);

                nextimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/sad.png", UriKind.RelativeOrAbsolute));
                reponseNext.Text = "Mauvaise reponse";
                reponseNext.Foreground = Brushes.Red;

                repD.Foreground = Brushes.Red;
                if (propB == bonnRep)
                {
                    repB.Foreground = Brushes.Green;
                }
                if (propC == bonnRep)
                {
                    repC.Foreground = Brushes.Green;
                }
                if (propA == bonnRep)
                {
                    repA.Foreground = Brushes.Green;
                }
                //Calcul de stats
            }
            repB.IsEnabled = false;
            repC.IsEnabled = false;
            repA.IsEnabled = false;
            repD.IsEnabled = false;
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
                    repB.Foreground = Brushes.Green;
                }
                if (propC == bonnRep)
                {
                    repC.Foreground = Brushes.Green;
                }
                if (propA == bonnRep)
                {
                    repA.Foreground = Brushes.Green;
                }
                if (propD == bonnRep)
                {
                    repD.Foreground = Brushes.Green;
                }
                //Calcul de stats
                repB.IsEnabled = false;
                repC.IsEnabled = false;
                repA.IsEnabled = false;
                repD.IsEnabled = false;
                next.Visibility = Visibility.Visible;
                timer.Visibility = Visibility.Collapsed;
                tempEcoulé = false;
            }
            increment--;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau3();
        }
        private void lbl_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau3();
        }
        private void langue()
        {
            if (MainWindow.langue == 0)
            {
                Back.Text = "Retour";
                dhmsg.Text = " est ce que vous etes sur de quitter le quiz ?";
                dhoui.Content = "Oui";
                dhnon.Content = "Non";
            }
            else
            {

                Back.Text = "عودة";
                dhmsg.Text = " هل انت متأكد من الخروج ؟";
                dhoui.Content = "نعم";
                dhnon.Content = "لا";
            }
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

            string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "Trace WHERE niveau = '" + niveau.ToString() + "' AND ID = '" + code.ToString() + "' AND Test = '" + theme.ToString() + "'";

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

