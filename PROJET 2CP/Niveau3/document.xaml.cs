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
    /// Logique d'interaction pour document.xaml
    /// </summary>
    public partial class document : Page
    {
        public document()
        {
            InitializeComponent();
            language();
        }
        private void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(";component/img/doc1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/doc2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/doc3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/doc4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/doc5.png", UriKind.Relative));
                img5.Source = btm;

            }
            if (MainWindow.langue == 1)
            {

                BitmapImage btm = new BitmapImage(new Uri(";component/img/docAR1.png", UriKind.Relative));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/docAR2.png", UriKind.Relative));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/docAR3.png", UriKind.Relative));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/docAR4.png", UriKind.Relative));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(";component/img/docAR5.png", UriKind.Relative));
                img5.Source = btm;
            }
        }
    }
}
