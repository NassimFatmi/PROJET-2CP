using PROJET_2CP.Pages;
using System;
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
        private int nbBonneReponse = 0;
        private int questionRep = 1;
        private int hasImage;
        private int idImage;
        private bool tempEcoulé = true;

        public TestNiveau1p2(int a,int b)
        {
            InitializeComponent();
            tagMax = b - a;
            b1.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Visible;
            b3.Visibility = Visibility.Collapsed;
            b4.Visibility = Visibility.Collapsed;
            next.Visibility = Visibility.Collapsed;
            q2.Text = "";
            q2.Visibility = Visibility.Collapsed;
            q1.Visibility = Visibility.Visible;

            tab = Tests1.reorder(a, b);
            creerQuestion();
            afficherQuestion();
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
                        hasImage = Convert.ToInt32(dr["hasImg"].ToString());
                        if (hasImage == 1)
                        {
                            idImage = Convert.ToInt32(dr["idImg"].ToString());
                        }

                    if (langue == 0)
                    {
                         next.Content = "Passer à la question suivante";
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
                        next.Content = "Passer à la question suivante";
                    }
                    if (langue == 1)
                    {
                        next.Content = "السؤال التالي";
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
            if ((string) ((Button)sender).Content==bonnRep)
            {
                b1.Foreground = Brushes.Green;
                nbBonneReponse++;
            }
            else
            {

                b1.Foreground = Brushes.Red;
                b2.Foreground = Brushes.Green;
            }
            b1.IsEnabled = false;
            b2.IsEnabled = false;

            if (quest2 != "")
            {
                nbBonneReponse--;
                q2.Text = quest2;
                q2.Visibility = Visibility.Visible;
                q1.Visibility = Visibility.Collapsed;

                b1.Visibility = Visibility.Collapsed;

                b1.Visibility = Visibility.Collapsed;
                b2.Visibility = Visibility.Collapsed;

                b3.Visibility = Visibility.Visible;
                b4.Visibility = Visibility.Visible;
                q2.Text = quest2;
                b3.Content = propC;
                b4.Content = propD;
            }
            else
            {
                tempEcoulé = false;
                if (tag == tagMax)
                {
                    if (langue == 0)
                    {
                        next.Content = "Voir le Bilan";
                    }
                    else
                    {
                        next.Content = "تفحص النتائج";
                    }
                }
                next.Visibility = Visibility.Visible;
                timer.Visibility = Visibility.Collapsed;
                correctionborder.Visibility = Visibility.Visible;
                correction.Text = correct;
            }
          
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {

            if ((string)((Button)sender).Content == bonnRep)
            {
                b2.Foreground = Brushes.Green;
                nbBonneReponse++;
            }
            else
            {
                b2.Foreground = Brushes.Red;
                b1.Foreground = Brushes.Green;
            }
            b1.IsEnabled = false;
            b2.IsEnabled = false;
            if (quest2 != "")
            {
                nbBonneReponse--;
                b1.Visibility = Visibility.Collapsed;
                b2.Visibility = Visibility.Collapsed;
                b3.Visibility = Visibility.Visible;
                b4.Visibility = Visibility.Visible;
                q2.Text = quest2;
                q2.Visibility = Visibility.Visible;
                q1.Visibility = Visibility.Collapsed;
                b3.Content = propC;
                b4.Content = propD;
            }
            else
            {
                tempEcoulé = false;
                if (tag == tagMax)
                {
                    if (langue == 0)
                    {
                        next.Content = "Voir le Bilan";
                    }
                    else
                    {
                        next.Content = "تفحص النتائج";
                    }
                }
                next.Visibility = Visibility.Visible;
                correctionborder.Visibility = Visibility.Visible;
                correction.Text = correct;
                timer.Visibility = Visibility.Collapsed;
            }
           
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {

            if ((string)((Button)sender).Content == bonnRep2)
            {
                b3.Foreground = Brushes.Green;
                nbBonneReponse++;
            }
            else
            {
                b3.Foreground = Brushes.Red;
                b4.Foreground = Brushes.Green;
            }
            b3.IsEnabled = false;
            b4.IsEnabled = false;
            correctionborder.Visibility = Visibility.Visible;
            correction.Text = correct;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
            if (tag == tagMax)
            {
                if (langue == 0)
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

            if ((string)((Button)sender).Content == bonnRep2)
            {
                b4.Foreground = Brushes.Green;
                nbBonneReponse++;
            }
            else
            {
                b4.Foreground = Brushes.Red;
                b3.Foreground = Brushes.Green;
            }
            b3.IsEnabled = false;
            b4.IsEnabled = false;
            correctionborder.Visibility = Visibility.Visible;
            correction.Text = correct;
            next.Visibility = Visibility.Visible;
            timer.Visibility = Visibility.Collapsed;
            tempEcoulé = false;
            if (tag == tagMax)
            {
                if (langue == 0)
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
            tag++;
            tempEcoulé = true;
            correctionborder.Visibility = Visibility.Collapsed;
            correction.Text = "";
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
                Home.mainFrame.Content = new Bilan(this.nbBonneReponse,this.tagMax + 1 - this.nbBonneReponse);
            }
            next.Visibility = Visibility.Collapsed;
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
                next.Visibility = Visibility.Visible;
                timer.Visibility = Visibility.Collapsed;
                correctionborder.Visibility = Visibility.Visible;
                correction.Text = correct;
                tempEcoulé = false;
                if (tag == tagMax)
                {
                    if (langue == 0)
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
    }
}
