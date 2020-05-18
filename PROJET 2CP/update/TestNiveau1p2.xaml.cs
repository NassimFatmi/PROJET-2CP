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
    /// Logique d'interaction pour TestNiveau1p2.xaml
    /// </summary>
    public partial class TestNiveau1p2 : Page
    {
        public static int langue { get; set; } =MainWindow.langue;
        private string quest;
        private string quest2;
        private string propA;
        private string propB;
        private string propC;
        private string propD;
        private string bonnRep;
        private string bonnRep2;
        private string correct;
        private int tag=0;
        private int tagMax;
        private int[] tab;
        private int increment = 20;
        private DispatcherTimer dt;
       // private int nbBonneReponse = 0;
        private int questionRep = 1;
        private int hasImage;
        private int idImage;
        private bool tempEcoulé = true;
        private bool CanGoToNextQuestion = true;


        private int _codeQst;

        private string propAar;
        private string propBar;
        private string propCar;
        private string propDar;

        private string propAfr;
        private string propBfr;
        private string propCfr;
        private string propDfr;

        public TestNiveau1p2(int a,int b)
        {
            InitializeComponent();
            tagMax = b - a;
            b1.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Visible;
            b3.Visibility = Visibility.Collapsed;
            b4.Visibility = Visibility.Collapsed;
            q2.Text = "";
            q2.Visibility = Visibility.Collapsed;
            q1.Visibility = Visibility.Visible;

            tab = Tests1.reorder(a, b);
            creerQuestion();
            afficherQuestion();
            if(MainWindow.langue==0)
            {
                ex1.Text = "Voir explication";
                rmq.Text = "Répondre à la deuxième partie de cette question <<la prochaine question>> pour voir l'explication";
            }
            if(MainWindow.langue == 1)
            {
                ex1.Text = "أنظر للتفسير";
                rmq.Text = "أجب على الجزء الثاني من  هذ السؤال <<السؤال القادم>> كي ترى التفسير";
            }
            Distimer();
        }
        public void creerQuestion()
        {
           
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =  {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [QuizTheme2] where Id='" + Convert.ToString(tab[tag]) + "'", connection);
            SqlDataReader dr;
            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _codeQst = Int32.Parse(dr["Id"].ToString());

                    hasImage = Convert.ToInt32(dr["hasImg"].ToString());
                        if (hasImage == 1)
                        {
                            idImage = Convert.ToInt32(dr["idImg"].ToString());
                        }

                    if (MainWindow.langue == 0)
                    {
                         next.Content = "suivant";
                        //switch_lang.Content = "changer la langue en arabe";
                        quest = dr["qst1Fr"].ToString();
                        quest2 = dr["qst2Fr"].ToString();
                        propA = dr["propA1Fr"].ToString();
                        propB = dr["propB1Fr"].ToString();
                        propC = dr["propA2Fr"].ToString();
                        propD = dr["propB2Fr"].ToString();
                        correct = dr["CorrectionFr"].ToString();
                        bonnRep = dr["bonneRep1Fr"].ToString();
                        bonnRep2 = dr["bonneRep2Fr"].ToString();
                       
                    }
                    if (MainWindow.langue == 1)
                    {
                        next.Content = "التالي";
                        //  switch_lang.Content = "تغيير اللغة الى الفرنسية";
                        quest = dr["qst1Ar"].ToString();
                        quest2 = dr["qst2Ar"].ToString();
                        propA = dr["propA1Ar"].ToString();
                        propB = dr["propB1Ar"].ToString();
                        propC = dr["propA2Ar"].ToString();
                        propD = dr["propB2Ar"].ToString();
                        correct = dr["CorrectionAr"].ToString();
                        bonnRep = dr["bonneRep1Ar"].ToString();
                        bonnRep2 = dr["bonneRep2Ar"].ToString();
                    }

                    propAfr = "propA1Fr";
                    propBfr = "propB1Fr";
                    propCfr = "propA2Fr";
                    propDfr = "propB2Fr";

                    propAar = "propA1Ar";
                    propBar = "propB1Ar";
                    propCar = "propA2Ar";
                    propDar = "propB2Ar";
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
            if (hasImage == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + Convert.ToString(idImage) + "_2.png", UriKind.RelativeOrAbsolute));
                img.Source = btm;
            }
            // definir la question
                q1.Text = quest;
                b1.Content = propA;
                b2.Content = propB;

            }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Page1Tests.total++;
            if (MainWindow.langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string) ((Button)sender).Content==bonnRep)
            {
                saveAnswer(true, 0, 0, _codeQst, propAfr, propAar);
                Page1Tests.nbBonneReponse++;
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;
            }
            else
            {
                saveAnswer(false, 0, 0, _codeQst, propAfr, propAar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + (string)((Button)sender).Content;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + (string)((Button)sender).Content;
                }
                votre_reponse.Foreground = Brushes.Red;
            }
            b1.IsEnabled = false;
            b2.IsEnabled = false;
            
            if (quest2 != "")
            {
                q2.Text = quest2;
                q2.Visibility = Visibility.Visible;
                q1.Visibility = Visibility.Collapsed;

                b1.Visibility = Visibility.Collapsed;

                b1.Visibility = Visibility.Collapsed;
                b2.Visibility = Visibility.Collapsed;
                e1.Visibility = Visibility.Collapsed;
                rmq.Visibility = Visibility.Visible;
                b3.Visibility = Visibility.Visible;
                b4.Visibility = Visibility.Visible;
                q2.Text = quest2;
                b3.Content = propC;
                b4.Content = propD;
                CanGoToNextQuestion = false;
            }
            else
            {
                tempEcoulé = false;
                if (tag == tagMax)
                {
                    if (MainWindow.langue == 0)
                    {
                        next.Content = "Voir le Bilan";
                    }
                    else
                    {
                        next.Content = "تفحص النتائج";
                    }
                }
              //  next_grid.Visibility = Visibility.Visible;
                timer.Visibility = Visibility.Collapsed;
              //  correctionborder.Visibility = Visibility.Visible;
                tb1.Text = correct;
                CanGoToNextQuestion = true;
            }
            next_grid.Visibility = Visibility.Visible;
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            Page1Tests.total++;
            if ((string)((Button)sender).Content == bonnRep)
            {
                saveAnswer(true, 0, 0, _codeQst, propBfr, propBar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;
                Page1Tests.nbBonneReponse++;
            }
            else
            {
                saveAnswer(false, 0, 0, _codeQst, propBfr, propBar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + (string)((Button)sender).Content;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + (string)((Button)sender).Content;
                }
                votre_reponse.Foreground = Brushes.Red;
            }
            b1.IsEnabled = false;
            b2.IsEnabled = false;
            if (quest2 != "")
            {
                b1.Visibility = Visibility.Collapsed;
                b2.Visibility = Visibility.Collapsed;
                b3.Visibility = Visibility.Visible;
                b4.Visibility = Visibility.Visible;
                q2.Text = quest2;
                q2.Visibility = Visibility.Visible;
                q1.Visibility = Visibility.Collapsed;
                b3.Content = propC;
                b4.Content = propD;
                CanGoToNextQuestion = false;
                e1.Visibility = Visibility.Collapsed;
                rmq.Visibility = Visibility.Visible;
            }
            else
            {
                tempEcoulé = false;
                if (tag == tagMax)
                {
                    if (MainWindow.langue == 0)
                    {
                        next.Content = "Voir le Bilan";
                    }
                    else
                    {
                        next.Content = "تفحص النتائج";
                    }
                }
              //  next_grid.Visibility = Visibility.Visible;
                //correctionborder.Visibility = Visibility.Visible;
                tb1.Text = correct;
                CanGoToNextQuestion = true;
                timer.Visibility = Visibility.Collapsed;
            }
            next_grid.Visibility = Visibility.Visible;
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            CanGoToNextQuestion = true;
            Page1Tests.total++;
            if (MainWindow.langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string)((Button)sender).Content == bonnRep2)
            {
                saveAnswer(true, 0, 0, _codeQst, propCfr, propCar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;
                Page1Tests.nbBonneReponse++;
            }
            else
            {
                saveAnswer(false, 0, 0, _codeQst, propCfr, propCar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + (string)((Button)sender).Content;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + (string)((Button)sender).Content;
                }
                votre_reponse.Foreground = Brushes.Red;
            }
            b3.IsEnabled = false;
            b4.IsEnabled = false;
            //correctionborder.Visibility = Visibility.Visible;
            tb1.Text = correct;
            next_grid.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
            if (tag == tagMax)
            {
                if (MainWindow.langue == 0)
                {
                    next.Content = "Voir le Bilan";
                }
                else
                {
                    next.Content = "تفحص النتائج";
                }
            }
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            CanGoToNextQuestion = true;
            Page1Tests.total++;
          
            if (MainWindow.langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if ((string)((Button)sender).Content == bonnRep2)
            {
                saveAnswer(true, 0, 0, _codeQst, propDfr, propDar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/happy.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + bonnRep;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + bonnRep;
                }
                votre_reponse.Foreground = Brushes.Green;
                Page1Tests.nbBonneReponse++;
            }
            else
            {
                saveAnswer(false, 0, 0, _codeQst, propDfr, propDar);

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Icons/sad.png", UriKind.RelativeOrAbsolute));
                reaction.Source = btm;
                reaction.Stretch = Stretch.Fill;
                if (MainWindow.langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + (string)((Button)sender).Content;
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + (string)((Button)sender).Content;
                }
                votre_reponse.Foreground = Brushes.Red;
            }
            b3.IsEnabled = false;
            b4.IsEnabled = false;
           // correctionborder.Visibility = Visibility.Visible;
            tb1.Text = correct;
            next_grid.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
            if (tag == tagMax)
            {
                if (MainWindow.langue == 0)
                {
                    next.Content = "Voir le Bilan";
                }
                else
                {
                    next.Content = "تفحص النتائج";
                }
            }
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            e1.Visibility = Visibility.Visible;
            rmq.Visibility = Visibility.Collapsed;
            if (CanGoToNextQuestion)
            {
                tag++;
                tempEcoulé = true;
                // correctionborder.Visibility = Visibility.Collapsed;
                tb1.Text = "";
                // reaction.Visibility = Visibility.Collapsed;
                if (tag <= tagMax)
                {
                    questionRep++;
                    b1.IsEnabled = true;
                    b2.IsEnabled = true;
                    b3.IsEnabled = true;
                    b4.IsEnabled = true;
                    b1.Foreground = Brushes.White;
                    b2.Foreground = Brushes.White;
                    b3.Foreground = Brushes.White;
                    b4.Foreground = Brushes.White;

                    b1.Visibility = Visibility.Visible;
                    b2.Visibility = Visibility.Visible;

                    q1.Visibility = Visibility.Visible;
                    q2.Visibility = Visibility.Collapsed;

                    b3.Visibility = Visibility.Collapsed;
                    b4.Visibility = Visibility.Collapsed;
                    creerQuestion();
                    afficherQuestion();
                    q2.Text = "";
                    timer.Visibility = Visibility.Visible;
                    increment = 20;
                }
                else
                {
                    Home.mainFrame.Content = new Bilan();
                }
            }
            else
            {
                if (tag <= tagMax)
                {
                    questionRep++;
                    b1.IsEnabled = true;
                    b2.IsEnabled = true;
                    b3.IsEnabled = true;
                    b4.IsEnabled = true;
                    b1.Foreground = Brushes.White;
                    b2.Foreground = Brushes.White;
                    b3.Foreground = Brushes.White;
                    b4.Foreground = Brushes.White;
                    q2.Visibility = Visibility.Visible;
                    timer.Visibility = Visibility.Visible;
                    increment = 20;
                }
                else
                {
                    Home.mainFrame.Content = new Bilan();
                }
            }
            

            next_grid.Visibility = Visibility.Collapsed;
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
                if(propA==bonnRep)
                {
                    b1.Foreground = Brushes.Green;
                }
                else
                {
                    b2.Foreground = Brushes.Green;
                }
               if(q2.Text !="")
                {
                    b3.Visibility = Visibility.Visible;
                    b4.Visibility = Visibility.Visible;
                    q2.Text = quest2;
                    q2.Visibility = Visibility.Visible;
                    q1.Visibility = Visibility.Collapsed;

                    b3.Content = propC;
                    b4.Content = propD;
                    if (propC==bonnRep2)
                    {
                        b3.Foreground = Brushes.Green;
                    }
                    else
                    {
                        b4.Foreground = Brushes.Green;
                    }
                }
                //Calcul de stats
                b1.IsEnabled = false;
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                b4.IsEnabled = false;
                next_grid.Visibility = Visibility.Visible;
                timer.Visibility = Visibility.Collapsed;
               // correctionborder.Visibility = Visibility.Visible;
                tb1.Text = correct;
                tempEcoulé = false;
                if (tag == tagMax)
                {
                    if (MainWindow.langue == 0)
                    {
                        next.Content = "Voir le Bilan";
                    }
                    else
                    {
                        next.Content = "تفحص النتائج";
                    }
                }
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
                    query = "UPDATE " + LogIN.LoggedUser.UtilisateurID + "Trace SET Reponse='" + reponse + "' , ReponseText = '" + reponseText + "' , ReponseTextAr ='" + reponseTextAr + "'  WHERE niveau = '" + niveau.ToString() + "' AND Code = '" + code.ToString() + "'";
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
