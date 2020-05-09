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
    /// Logique d'interaction pour risque.xaml
    /// </summary>
    public partial class risque : Page
    {
        public risque()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/risq1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risq2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risq3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risq4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risq5.png", UriKind.Relative));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risq6.png", UriKind.Relative));
                img6.Source = btm;

            }
            if (MainWindow.langue == 1)
            {

                BitmapImage btm = new BitmapImage(new Uri(";component/img/risqAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risqAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risqAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/risqAR4.png", UriKind.Relative));
                img4.Source = btm;
            }
        }
    }
}
