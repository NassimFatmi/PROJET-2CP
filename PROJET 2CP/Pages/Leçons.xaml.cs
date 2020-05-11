
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PROJET_2CP.Noyau;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour Leçons.xaml
    /// </summary>
    public partial class Leçons : Page
    {
        public static int langue { get; set; } = PROJET_2CP.MainWindow.langue;
        public Leçons()
        {
            NLeçon lesson = new NLeçon();
            InitializeComponent();
            quiz1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz4.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            lecon1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/1_off.png", UriKind.RelativeOrAbsolute));
            lecon2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/Images/40_off.png", UriKind.RelativeOrAbsolute));
            lecon3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/theme1quiz14.png", UriKind.RelativeOrAbsolute));

            initialiserLangue();
            configurerLaLangue();
            guestMode();
        }
        private void configurerLaLangue()
        {

            if (langue == 0)
            {
                b0.Content = "regles generales";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                qst0.Text = "Questions";
                qst1.Text = "Questions";
                qst2.Text = "Questions";
                qst3.Text = "Questions";

                thm1.Content = "Désignation";
                thm2.Content = "Désignation";
            }
            if (langue == 1)
            {
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                b0.Content = "قواعد عامة"; 
                qst0.Text = "أسئلة";
                qst1.Text = "أسئلة";
                qst2.Text = "أسئلة";
                qst3.Text = "أسئلة";

                thm1.Content = "حول الدرس";
                thm2.Content = "حول الدرس";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (langue == 0)
            {
                //Home.mainFrame.Content = new Signalisation();
            }
            if (langue == 1)
            {
                //Home.mainFrame.Content = new SignalisationAr();
            }
            Panneaux.panneau = 0;
            PanneauxInterdiction.panneau = 0;
            PanneauxObligation.panneau = 0;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            PanneauxInterdiction.panneau = 0;
            PanneauxObligation.panneau = 0;
            Home.mainFrame.Content = new Panneaux();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Panneaux.panneau = 0;
            PanneauxObligation.panneau = 0;
            Home.mainFrame.Content = new PanneauxInterdiction();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Panneaux.panneau = 0;
            PanneauxInterdiction.panneau = 0;
            Home.mainFrame.Content = new PanneauxObligation();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Quiz(1, 7);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Quiz(7, 13);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Quiz(13, 19);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Quiz(19, 25);
        }
        private void switch_lang_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (langue == 0 && !changed)
            {
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                b0.Content = "قواعد عامة";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                langue = 1;
                MainWindow.langue = 1;
                changed = true;
            }
            if (langue == 1 && !changed)
            {
                MainWindow.langue = 0;
                b0.Content = "Règle générale";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                langue = 0;
                changed = true;
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau1.Niv1Main();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                h0.Content = "Niveau 1 : themes";
                singtxt.Text = "Signalisation";
                intertxt.Text = "Intersection et priorité";
                back.Text = "Retoure";
            }
            else
            {
                //la langue arabe
                h0.Content = "المستوى 1 : المواضيع";
                singtxt.Text = "الاشارات";
                intertxt.Text = "التقاطعات والأولوية";
                back.Text = "عودة";
            }
        }
        /*<<
            Decrease advantages of the guest user
        >>*/
        private void guestMode()
        {
            if (LogIN.LoggedUser.ID == 0 && LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
            {
                themeinterspriot.IsEnabled = false;
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                q0.IsEnabled = false;
                q1.IsEnabled = false;
                q2.IsEnabled = false;
                q3.IsEnabled = false;

                if (MainWindow.langue == 0)
                {   //la langue français
                }
                else
                {
                    //la langue arabe
                }
            }
        }
    }
}
