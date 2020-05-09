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

namespace PROJET_2CP.Niveau3
{
    /// <summary>
    /// Logique d'interaction pour les_ponints.xaml
    /// </summary>
    public partial class les_ponints : Page
    {
        public les_ponints()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/per1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/per2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/per3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/per4.png", UriKind.Relative));
                img4.Source = btm;

            }
            if (MainWindow.langue == 1)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/perAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/perAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/perAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/perAR4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/perAR5.png", UriKind.Relative));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/perAR6.png", UriKind.Relative));
                img6.Source = btm;
            }
        }
    }
}
