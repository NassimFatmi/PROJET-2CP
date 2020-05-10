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
        private string _themeQst;
        private int _niveau = 1;

        public Quiz(int bi,int bs)
        {
            InitializeComponent();
            tag = bi;
            tagMax = bs;
            next.Visibility = Visibility.Collapsed;
            creerQuestion();
            afficherQuestion();
            Distimer();
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
                    _themeQst = dr["leçon"].ToString();
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
                        propB = dr["propBFr"].ToString();
                        propC = dr["propCFr"].ToString();
                        propD = dr["propDFr"].ToString();
                        quest = dr["questionFr"].ToString();
                        bonnRep= dr["bonneRepFr"].ToString();
                    }
                    if (langue == 1)
                    {
                        //  repInteractive = " أحسنت";
                        switch_lang.Content = "تغيير اللغة الى الفرنسية";
                        next.Content = "السؤال التالي";
                       propA = dr["propAAr"].ToString();
                        propB = dr["propBAr"].ToString();
                        propC = dr["propCAr"].ToString();
                        propD = dr["propDAr"].ToString();
                        quest = dr["questionAr"].ToString();
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
                Label lbl = new Label();
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                lbl.VerticalAlignment = VerticalAlignment.Center;
                lbl.Foreground = Brushes.White;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                lbl.FontSize = 60;
                lbl.Content = nbBonneReponse.ToString()+" / 6";
                gd.Children.Add(lbl);
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
            //Calcul de stats
                //sauvgarder la question et la bonne reponse et la reponse de l'utilisateur
            if (propA==bonnRep)
            {
                saveAnswer(true,_niveau,_codeQst,_themeQst);
                repA.Foreground = Brushes.Green;
                nbBonneReponse++;
                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));
            }
            else
            {
                saveAnswer(false, _niveau, _codeQst, _themeQst);

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

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
            sp.Stop();
            if (propB == bonnRep)
            {
                saveAnswer(true, _niveau, _codeQst, _themeQst);

                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;

                nbBonneReponse++;
                repB.Foreground = Brushes.Green;

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));

                //Calcul de stats
            }
            else
            {
                saveAnswer(false, _niveau, _codeQst, _themeQst);

                reponseNext.Text = "Mauvaise reponse";
                reponseNext.Foreground = Brushes.Red;
                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

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
            sp.Stop();
            if (propC == bonnRep)
            {
                saveAnswer(true, _niveau, _codeQst, _themeQst);

                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;

                nbBonneReponse++;
                repC.Foreground = Brushes.Green;

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));

                //Calcul de stats
            }
            else
            {
                saveAnswer(false, _niveau, _codeQst, _themeQst);

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));

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
            sp.Stop();
            if (propD == bonnRep)
            {
                saveAnswer(true, _niveau, _codeQst, _themeQst);

                reponseNext.Text = "Bonne reponse";
                reponseNext.Foreground = Brushes.GreenYellow;
                nbBonneReponse++;
                repD.Foreground = Brushes.Green;

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\happy.png"));

                //Calcul de stats
            }
            else
            {
                saveAnswer(false, _niveau, _codeQst, _themeQst);

                nextimage.Source = new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\sad.png"));
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
            Home.mainFrame.Content = new Pages.Leçons();
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
                MessageBox.Show("error save Db quiz Testniveau 1 ");
            }
        }
    }
}
