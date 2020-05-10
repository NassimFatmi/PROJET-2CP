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

namespace PROJET_2CP
{
    /// <summary>
    /// Logique d'interaction pour pdf.xaml
    /// </summary>
    public partial class pdf : Page
    {
        public pdf()
        {
            InitializeComponent();
        }
        public void language()
        {
            if (MainWindow.langue == 0)
            {
                BitmapImage btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/a.png", UriKind.RelativeOrAbsolute));
                img1.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/b.png", UriKind.RelativeOrAbsolute));
                img2.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/c.png", UriKind.RelativeOrAbsolute));
                img3.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/d.png", UriKind.RelativeOrAbsolute));
                img4.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/e.png", UriKind.RelativeOrAbsolute));
                img5.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/f.png", UriKind.RelativeOrAbsolute));
                img6.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/g.png", UriKind.RelativeOrAbsolute));
                img7.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/h.png", UriKind.RelativeOrAbsolute));
                img8.Source = btm;
                btm = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/img/i.png", UriKind.RelativeOrAbsolute));
                img9.Source = btm;

            }
            if (MainWindow.langue == 1)
            {
            }
        }
    }
}
