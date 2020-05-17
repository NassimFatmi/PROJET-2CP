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
    /// Logique d'interaction pour Quiz.xaml
    /// </summary> 
    public partial class Quiz : Page
    {
        private SoundPlayer sp;
        private int hasImage;
        private string quest;
        private int idImage;
        private string propA;
        private string propB;
        private string propC;
        private string propD;
        private string bonnRep;
        private int langue = PROJET_2CP.MainWindow.langue;
        private int tag;
        private int tagMax;
        private int increment = 20;
        private DispatcherTimer dt;
        private int nbBonneReponse = 0;
        private bool tempEcoulé=true;
        //private  string repInteractive;

        // partie pour sauvegarde 
        private int _codeQst;
        private int _niveau = 1;

        private string propAar;
        private string propBar;
        private string propCar;
        private string propDar;

        private string propAfr;
        private string propBfr;
        private string propCfr;
        private string propDfr;

        public static int _lastPageQuiz;

        public Quiz(int bi,int bs)
        {
            InitializeComponent();
            tag = bi;
            tagMax = bs;
            next.Visibility = Visibility.Collapsed;
            creerQuestion();
            afficherQuestion();
            Distimer();
            initialiserLangue();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                back.Text = "Retour";
            }
            else
            {
                //la langue arabe
                back.Text = "عودة";
            }
        }
        public void creerQuestion()
        {
            //int tag = aleaInt();
            //recuperation de la description du panneau à partir de la base de donnée 
            SqlConnection connection = new SqlConnection($@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Panneaux.mdf; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from [Quiz] where Id='" + Convert.ToString(tag) + "'", connection);
            SqlDataReader dr;
            try
            {  connection.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    _codeQst = Int32.Parse(dr["Id"].ToString());

                    //si bi < 50 niveau = 1
                    if (_codeQst <= 50)
                    {
                        _niveau = 1;
                    }
                    if (_codeQst > 50)
                    {
                        _niveau = 2;
                    }

                    hasImage = Convert.ToInt32( dr["hasImage"].ToString());
                    if (hasImage == 1)
                    {
                        idImage = Convert.ToInt32(dr["idImage"].ToString());
                    }
                    if (langue == 0)
                    {
                        // repInteractive =" bravo";
                        switch_lang.Content = "changer la langue en arabe";
                        propA = dr["propAFr"].ToString();
                        propAfr = propA;
                        propB = dr["propBFr"].ToString();
                        propBfr = propB;
                        propC = dr["propCFr"].ToString();
                        propCfr = propC;
                        propD = dr["propDFr"].ToString();
                        propDfr = propD;
                        quest = dr["questionFr"].ToString();
                        bonnRep= dr["bonneRepFr"].ToString();
                    }
                    if (langue == 1)
                    {
                        //  repInteractive = " أحسنت";
                        switch_lang.Content = "تغيير اللغة الى الفرنسية";
                        suivant.Text = "السؤال التالي";
                        propA = dr["propAAr"].ToString();
                        propAar = propA;
                        propB = dr["propBAr"].ToString();
                        propBar = propB;
                        propC = dr["propCAr"].ToString();
                        propCar = propC;
                        propD = dr["propDAr"].ToString();
                        propDar = propD;
                        quest = dr["questionAr"].ToString();
                        bonnRep = dr["bonneRepAr"].ToString();
                    }
                    propAfr = "propAFr";
                    propBfr = "propBFr";
                    propCfr = "propCFr";
                    propDfr = "propDFr";

                    propAar = "propAAr";
                    propBar = "propBAr";
                    propCar = "propCAr";
                    propDar = "propDAr";
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

            //Afficher l'image du panneau
            if (hasImage == 1)
            {
                panneau.Visibility = Visibility.Visible;
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + Convert.ToString(idImage) + "_off.png", UriKind.RelativeOrAbsolute));
                panneau.Source = btm;
                panneau.Stretch = Stretch.Fill; 
            }
            else
            {
                panneau.Visibility = Visibility.Collapsed;
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
               
            if ( langue == 0 )
            {
                sp = new SoundPlayer(@"QuizSounds\q" + Convert.ToString(tag) + "Fr.wav");
                try
                {
                    sp.Play();
                }
                catch(Exception)
                {

                }
            }

            if (langue == 1)
            {
                sp = new SoundPlayer(@"QuizSounds\q" + Convert.ToString(tag) + "Ar.wav");
                try
                {
                    sp.Play();
                }
                catch (Exception)
                {

                }
            }
        }
        private void switch_lang_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (langue == 0 && !changed)
            {
                next.Content = "لقد أكملت هذاالاختبار القصير";
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                switch_lang.Margin = new Thickness(683, 45, 35, 595);
                langue = 1;
                MainWindow.langue = 1;
                changed = true;
            }
            if (langue == 1 && !changed)
            {
                    MainWindow.langue = 0;
                next.Content = "vous avez terminé ce petit quiz";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(35, 45, 683, 595);
                langue = 0;
                changed = true;
            }
            if (tag >= tagMax)
            {
                repA.Visibility = Visibility.Collapsed;
                repB.Visibility = Visibility.Collapsed;
                repC.Visibility = Visibility.Collapsed;
                repD.Visibility = Visibility.Collapsed;
            }
            else
            {
                creerQuestion();
                afficherQuestion();
            }
        }
        private void next_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer soundPlayer;
            tag++;
            tempEcoulé = true;
            if (tag >= tagMax)
            {
                repA.Visibility = Visibility.Collapsed;
                repB.Visibility = Visibility.Collapsed;
                repC.Visibility = Visibility.Collapsed;
                repD.Visibility = Visibility.Collapsed;
                question.Visibility = Visibility.Collapsed;
                panneau.Visibility = Visibility.Collapsed;
                bilanQuiz.Visibility = Visibility.Visible;
                lbl.Content = nbBonneReponse.ToString()+" / 6";

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
                    catch(Exception)
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
            sp.Stop();
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            //Calcul de stats
            //sauvgarder la question et la bonne reponse et la reponse de l'utilisateur
            if (propA==bonnRep)
            {
                saveAnswer(true,_niveau,1,_codeQst, propAfr,propAar); // theme 1
                repA.Foreground = Brushes.Green;
                nbBonneReponse++;

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
            }
            else
            {
                saveAnswer(false, _niveau,1, _codeQst, propAfr, propAar);

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + repA.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + repA.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;

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
            sp.Stop();
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            if (propB == bonnRep)
            {
                saveAnswer(true, _niveau, 1, _codeQst, propBfr, propBar) ;


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
                //Calcul de stats
            }
            else
            {
                saveAnswer(false, _niveau,1, _codeQst, propBfr, propBar);
                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + repB.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + repB.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;
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
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            sp.Stop();
            if (propC == bonnRep)
            {
                saveAnswer(true, _niveau,1, _codeQst, propCfr, propCar);


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

                //Calcul de stats
            }
            else
            {
                saveAnswer(false, _niveau,1, _codeQst, propCfr, propCar);
                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + repC.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + repC.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;
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
            if (langue == 0)
            {
                bonne_reponse.Text = "Bonne réponse : " + bonnRep;
            }
            else
            {
                bonne_reponse.Text = " الإجابة الصحيحة : " + bonnRep;
            }
            sp.Stop();
            if (propD == bonnRep)
            {
                saveAnswer(true, _niveau,1, _codeQst, propDfr, propDar);

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

                //Calcul de stats
            }
            else
            {
                saveAnswer(false, _niveau,1, _codeQst, propDfr, propDar);

                reaction.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

                if (langue == 0)
                {
                    votre_reponse.Text = "Votre réponse : " + repD.Content.ToString();
                }
                else
                {
                    votre_reponse.Text = "إجابتك:" + repD.Content.ToString();
                }
                votre_reponse.Foreground = Brushes.Red;

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

            if(increment==20)
            {
                timer.Foreground = Brushes.Green;
            }
            if(increment==10)
            {
                timer.Foreground = Brushes.Orange;
            }
            if(increment==5)
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
            if (increment==0 && tempEcoulé)
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
            if (Pages.Quiz._lastPageQuiz == 0)
                Home.mainFrame.Content = new Pages.Leçons();
            else
                Home.mainFrame.Content = new Niveau2.Lesson();
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
        private void saveAnswer(bool reponse, int niveau ,int theme, int code, string reponseText ,string reponseTextAr)
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
                    query = "UPDATE " + LogIN.LoggedUser.UtilisateurID + "Trace SET Reponse='" + reponse + "' , ReponseText = '"+ reponseText + "' , ReponseTextAr ='" + reponseTextAr + "'  WHERE niveau = '" + niveau.ToString() + "' AND ID = '" + code.ToString() + "'";
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

        private void lbl_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Leçons();
        }
    }
}
