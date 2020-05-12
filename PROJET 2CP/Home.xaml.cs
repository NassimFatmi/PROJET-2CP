using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public static Frame mainFrame;
        public static Grid _tutogrid;
        public Home()
        {
            InitializeComponent();
            logoimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/EDautoEcole.png", UriKind.RelativeOrAbsolute));
            _tutogrid = tutogrid;
            initialiserLangue();
            guestMode();
            mainFrame = new Frame();
            mainFrame.Height = 610;
            mainFrame.Width = 950;
            mainFrame.VerticalAlignment = VerticalAlignment.Center;
            mainFrame.HorizontalAlignment = HorizontalAlignment.Center;
            mainFrame.Margin = new Thickness(0,10,0,0);
            mainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            mainFrame.Navigating += new NavigatingCancelEventHandler(this.frame_Navigating);

            initializeHomePage();
            mainFrame.Content = new PagePrincipale();
            mainGrid.Children.Add(mainFrame);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void initializeHomePage()
        {
            try
            {
                if (LogIN.LoggedUser.Image.ToString().Equals(""))
                    avatarFrame.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\Default.png")));
                else
                    avatarFrame.Fill = new ImageBrush(new BitmapImage(new Uri(LogIN.LoggedUser.Image.ToString())));
            }
            catch(Exception)
            {
                MessageBox.Show("Can't find avatare image");
            }
            userIDHome.Text = LogIN.LoggedUser.UtilisateurID;

            if(LogIN.LoggedUser.Theme == true || LogIN.LoggedUser.Theme.ToString().Equals(""))
            {
                GradientStop color1 = new GradientStop();
                GradientStop color2 = new GradientStop();

                LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                linearGradientBrush.StartPoint = new Point(0, 0);
                linearGradientBrush.EndPoint = new Point(1, 1);

                color1.Offset = 0;
                color1.Color = Color.FromRgb(14, 115, 134);
                linearGradientBrush.GradientStops.Add(color1);

                color2.Offset = 1;
                color2.Color = Color.FromRgb(0, 200, 238);
                linearGradientBrush.GradientStops.Add(color2);

                MainWindow.getInstance().Background = linearGradientBrush;
            }
            else
            {
                GradientStop color1 = new GradientStop();
                GradientStop color2 = new GradientStop();

                LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                linearGradientBrush.StartPoint = new Point(0, 0);
                linearGradientBrush.EndPoint = new Point(1, 1);

                color1.Offset = 0;
                color1.Color = Color.FromRgb(134, 14, 14);
                linearGradientBrush.GradientStops.Add(color1);

                color2.Offset = 1;
                color2.Color = Color.FromRgb(223, 119, 119);
                linearGradientBrush.GradientStops.Add(color2);

                MainWindow.getInstance().Background = linearGradientBrush;
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            GradientStop color1 = new GradientStop();
            GradientStop color2 = new GradientStop();

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            linearGradientBrush.StartPoint = new Point(0, 0);
            linearGradientBrush.EndPoint = new Point(1, 1);

            color1.Offset = 0;
            color1.Color = Color.FromRgb(14, 115, 134);
            linearGradientBrush.GradientStops.Add(color1);

            color2.Offset = 1;
            color2.Color = Color.FromRgb(0, 200, 238);
            linearGradientBrush.GradientStops.Add(color2);

            MainWindow.getInstance().Background = linearGradientBrush;
            MainWindow.quizFrame.Content = new LogIN();
        }

        private void MenuList_SelectionChanged(object sender, RoutedEventArgs e)
        {
           int index = MenuList.SelectedIndex;
            
            switch (index)
            {
                case 0:
                    mainFrame.Content = new PagePrincipale();
                    MenuList.SelectedIndex = -1;
                    statesLV.Background = null;
                    settingsLV.Background = null;
                    helpLV.Background = null;
                    break;

                case 1:
                    mainFrame.Content = new Statistiques();
                    statesLV.Background = Brushes.Red;
                    settingsLV.Background = null;
                    helpLV.Background = null;
                    bonusLV.Background = null;
                    break;

                case 2:
                    mainFrame.Content = new Settings();
                    statesLV.Background = null;
                    settingsLV.Background = Brushes.MediumPurple;
                    helpLV.Background = null;
                    bonusLV.Background = null;
                    break;

                case 3:
                    mainFrame.Content = new QuRe();
                    statesLV.Background = null;
                    settingsLV.Background = null;
                    helpLV.Background = null;
                    bonusLV.Background = Brushes.DeepSkyBlue;
                    break;
                case 4:
                    mainFrame.Content = new Aide();
                    statesLV.Background = null;
                    settingsLV.Background = null;
                    helpLV.Background = Brushes.LightGreen;
                    bonusLV.Background = null;
                    break;
            }
        }

        // partie pour l'animation ! >>

        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;
        private Duration _duration = new Duration(TimeSpan.FromSeconds(0.3));
        private double _oldWidth = 0;

        // <<Frame animation>>
        private void frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;

                _navArgs = e;
                _oldWidth = mainFrame.ActualWidth;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = 950;
                animation0.To = 0;
                animation0.Duration = _duration;
                animation0.Completed += SlideCompleted;
                mainFrame.BeginAnimation(WidthProperty, animation0);
            }
            _allowDirectNavigation = false;
        }

        private void SlideCompleted(object sender, EventArgs e)
        {
            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                        mainFrame.Navigate(_navArgs.Content);
                    else
                        mainFrame.Navigate(_navArgs.Uri);
                    break;

                /*case NavigationMode.Back:
                    mainFrame.GoBack();
                    break;
                case NavigationMode.Forward:
                    mainFrame.GoForward();
                    break;
                case NavigationMode.Refresh:
                    mainFrame.Refresh();
                    break;*/
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = 0;
                    animation0.To = 950;
                    animation0.Duration = _duration;
                    mainFrame.BeginAnimation(WidthProperty, animation0);
                });
        }
        /*<<
            Decrease advantages of the guest user
        >>*/
        private void guestMode()
        {
            if (LogIN.LoggedUser.ID == 0 && LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
            {
                settingsLV.IsEnabled = false;
                statesLV.IsEnabled = false;
                popup1.IsEnabled = false;
                popup2.IsEnabled = false;
            }
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                lvitem0.Text = "Page principale";
                lvitem1.Text = "Statistiques";
                lvitem2.Text = "Paramètre";
                lvitem3.Text = "Aide";
                lvitem4.Text = "Questions réponses";

                popup1.Content = "Paramètre";
                popup2.Content = "Aide";
                popup3.Content = "Déconnecter";
                profilebtn.ToolTip = "Voir profile";

                //tutorial partie
                moreInfo.Content = "Pour plus d'information voir le guide d'utilisation !";
                greating.Content = "Bienvenu dans EDautoEcole tutoriel";
                hometuto.Text = "Home , raccourci vers le menu principale";
                viewstates.Text = "Voir votre statistiques";
                settingstuto.Text = "Régler vos information et changer les themes";
                qstreptuto.Text = "tester vos connaissances";
                helptuto.Text = "Ouvrire le guide d'utilisation";
                moreshortcuts.Text = "Plus de raccourci";
                commencertuto.Text = "Commencer a apprendre";
            }
            else
            {
                //la langue arabe
                lvitem0.Text = "الصفحة الرئيسية";
                lvitem1.Text = "احصائيات";
                lvitem2.Text = "اعدادات";
                lvitem3.Text = "مساعدة";


                popup1.Content = "اعدادات";
                popup2.Content = "مساعدة";
                popup3.Content = "تسجيل الخروج";
                profilebtn.ToolTip = "حسابي";

                //tutorial partie
                //tutorial partie
                moreInfo.Content = "لمزيد من المعلومات انظر دليل الاستعمال";
                greating.Content = "مرحبا بك في دليل استعمال السريع";
                hometuto.Text = "المنزل , اختصار للعودة للصفحة الرئيسية";
                viewstates.Text = "راقب احصائياتك";
                settingstuto.Text = "عدل في معلوماتك الشخصية";
                qstreptuto.Text = "اختبر معلوماتك";
                helptuto.Text = "افتح دليل الاستعمال";
                moreshortcuts.Text = "المزيد من الاختصارات";
                commencertuto.Text = "ابدأ التعلم الان";
            }
        }

        private void profile(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new Settings();
        }


        private void b1_Click(object sender, RoutedEventArgs e)
        {
            b1.Visibility = Visibility.Collapsed;
            b2.Visibility = Visibility.Visible;
            b2a.Visibility = Visibility.Visible;
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            b3.Visibility = Visibility.Visible;
            b3a.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Collapsed;
            b2a.Visibility = Visibility.Collapsed;
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            b3.Visibility = Visibility.Collapsed;
            b3a.Visibility = Visibility.Collapsed;
            b4.Visibility = Visibility.Visible;
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            b4.Visibility = Visibility.Collapsed;
            b4_Copy.Visibility = Visibility.Visible;
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            b6.Visibility = Visibility.Visible;
            b5.Visibility = Visibility.Collapsed;
        }
        public static Grid getinstanceTuto()
        {
            return _tutogrid;
        }

        private void b6_Click(object sender, RoutedEventArgs e)
        {
            tutogrid.Visibility = Visibility.Collapsed;
        }

        private void b4_Copy_Click(object sender, RoutedEventArgs e)
        {
            b4_Copy.Visibility = Visibility.Collapsed;
            b5.Visibility = Visibility.Visible;
        }

        private void Aide_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Aide();
        }
    }
}
