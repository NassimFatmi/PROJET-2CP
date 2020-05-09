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
    /// Logique d'interaction pour utilisation.xaml
    /// </summary>
    public partial class utilisation : Page
    {
        public utilisation()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/ut1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/ut2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/ut3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/ut4.png", UriKind.Relative));
                img4.Source = btm;

            }
            if (MainWindow.langue == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/utAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/utAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/utAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/utAR4.png", UriKind.Relative));
                img4.Source = btm;
            }
        }
    }
}
