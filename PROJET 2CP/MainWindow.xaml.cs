using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Media;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame quizFrame;
        public static Rectangle backgroundImage;
        public static int langue { get; set; }
        public static Grid _backGrid { get; set; }
        public static Grid apArb { get; set; }
        public static Grid apfran { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            langue = 0;
            initialize();
            apArb = AproposArab;
            apfran = aproposFranc;
            _backGrid = backGrid;
        }
        private void initialize()
        {
          

               backgroundImage = new Rectangle();
            backgroundImage.HorizontalAlignment = HorizontalAlignment.Center;
            backgroundImage.VerticalAlignment = VerticalAlignment.Bottom;
            backgroundImage.Width = 970;
            backgroundImage.Height = 640;
            backgroundImage.Opacity = 0.7;

            backgroundImage.Fill = new ImageBrush(new BitmapImage(new Uri($@"{System.IO.Directory.GetCurrentDirectory()}\icons\Background.png")));
            grid.Children.Add(backgroundImage);

            quizFrame = new Frame();
            quizFrame.Height = 660;
            quizFrame.Width = 1000;
            quizFrame.VerticalAlignment = VerticalAlignment.Center;
            quizFrame.HorizontalAlignment = HorizontalAlignment.Center;
            quizFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            quizFrame.Content = new LogIN();

            quizFrame.Navigating += new NavigatingCancelEventHandler(this.frame_Navigating);
            grid.Children.Add(quizFrame);

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;
        private Duration _duration = new Duration(TimeSpan.FromSeconds(0.1));
        private double _oldWidth = 0;

        // <<Frame animation>>
        private void frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;

                _navArgs = e;
                _oldWidth = quizFrame.ActualWidth;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = 1000;
                animation0.To = 0;
                animation0.Duration = _duration;
                animation0.Completed += SlideCompleted;
                quizFrame.BeginAnimation(WidthProperty, animation0);
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
                        quizFrame.Navigate(_navArgs.Content);
                    else
                        quizFrame.Navigate(_navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    quizFrame.GoBack();
                    break;
                case NavigationMode.Forward:
                    quizFrame.GoForward();
                    break;
                case NavigationMode.Refresh:
                    quizFrame.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = 0;
                    animation0.To = 1000;
                    animation0.Duration = _duration;
                    quizFrame.BeginAnimation(WidthProperty, animation0);
                });
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            {

            }
        }
        public static Grid getInstance()
        {
            return _backGrid;
        }
        public static Grid getAproposArab()
        {
            return apArb;
        }
        public static Grid getAproposfrançais()
        {
            return apfran;
        }

        private void retour_Click(object sender, RoutedEventArgs e)
        {
            getAproposfrançais().Visibility = Visibility.Collapsed;
        }

        private void retourAR_Click(object sender, RoutedEventArgs e)
        {
            getAproposArab().Visibility = Visibility.Collapsed;
        }

        private void APRP_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.langue == 0)
            {
                MainWindow.getAproposfrançais().Visibility = Visibility.Visible;
            }
            else
            {
                MainWindow.getAproposArab().Visibility = Visibility.Visible;
            }
        }
    }
}
