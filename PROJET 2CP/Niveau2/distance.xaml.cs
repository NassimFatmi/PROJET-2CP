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
    /// Logique d'interaction pour distance.xaml
    /// </summary>
    public partial class distance : Page
    {
        public distance()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/dist1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/dist2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/dist3.png", UriKind.Relative));
                img3.Source = btm;

            }
            if (MainWindow.langue == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/distAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/distAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/distAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/distAR4.png", UriKind.Relative));
                img4.Source = btm;
            }
        }

    }
}
