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

namespace PROJET_2CP.Niveau2
{
    /// <summary>
    /// Logique d'interaction pour la_securité.xaml
    /// </summary>
    public partial class la_securité : Page
    {
        public la_securité()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sec1.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sec2.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sec3.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sec4.png", UriKind.RelativeOrAbsolute));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sec5.png", UriKind.RelativeOrAbsolute));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sec6.png", UriKind.RelativeOrAbsolute));
                img6.Source = btm;

            }
            if (MainWindow.langue == 1)
            {

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/secAR1.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/secAR2.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/secAR3.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/secAR4.png", UriKind.RelativeOrAbsolute));
                img4.Source = btm;
              
            }
        }
        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Lesson();
        }
    }
}
