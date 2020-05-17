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

namespace PROJET_2CP.Niveau1
{
    /// <summary>
    /// Interaction logic for signalisation_gen.xaml
    /// </summary>
    public partial class signalisation_gen : Page
    {
        public signalisation_gen()
        {
            InitializeComponent();
            language();
        }
        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Leçons();
        }
        public void language()
        {
            if (MainWindow.langue == 0)
            {

                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sig1.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sig2.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sig3.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sig4.png", UriKind.RelativeOrAbsolute));
                img4.Source = btm;
                

            }
            if (MainWindow.langue == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sigar1.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sigar2.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/sigar3.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
               
                back.Text = "رجوع";
            }
        }
    }
}
