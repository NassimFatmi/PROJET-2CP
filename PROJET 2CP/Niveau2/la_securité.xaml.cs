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
                BitmapImage btm = new BitmapImage(new Uri(";component/img/sec1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/sec2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/sec3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/sec4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/sec5.png", UriKind.Relative));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/sec6.png", UriKind.Relative));
                img6.Source = btm;

            }
            if (MainWindow.langue == 1)
            {

                BitmapImage btm = new BitmapImage(new Uri(";component/img/secAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/secAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/secAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/secAR4.png", UriKind.Relative));
                img4.Source = btm;
            }
        }
    }
}
