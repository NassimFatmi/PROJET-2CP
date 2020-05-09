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
    /// Logique d'interaction pour connaitre.xaml
    /// </summary>
    public partial class connaitre : Page
    {
        public connaitre()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/con1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/con2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/con3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/con4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/con5.png", UriKind.Relative));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/con6.png", UriKind.Relative));
                img6.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/con7.png", UriKind.Relative));
                img7.Source = btm;

            }
            if (MainWindow.langue == 1)
            {

                BitmapImage btm = new BitmapImage(new Uri(";component/img/conAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/conAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/conAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/conAR4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/conAR5.png", UriKind.Relative));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/conAR6.png", UriKind.Relative));
                img6.Source = btm;
               
            }
        }
    }
}
