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
    /// Logique d'interaction pour les_feux.xaml
    /// </summary>
    public partial class les_feux : Page
    {
        public les_feux()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux1.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux2.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux3.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux4.png", UriKind.RelativeOrAbsolute));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux5.png", UriKind.RelativeOrAbsolute));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux6.png", UriKind.RelativeOrAbsolute));
                img6.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feux7.png", UriKind.RelativeOrAbsolute));
                img7.Source = btm;

            }
            if (MainWindow.langue == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feuxAR1.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feuxAR2.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feuxAR3.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feuxAR4.png", UriKind.RelativeOrAbsolute));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/feuxAR5.png", UriKind.RelativeOrAbsolute));
                img5.Source = btm;
               
                
            }
        }
        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Lesson();
        }
    }
}
