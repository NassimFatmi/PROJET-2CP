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
using PROJET_2CP.Pages;

namespace PROJET_2CP.Niveau2
{
    /// <summary>
    /// Interaction logic for Niv2Main.xaml
    /// </summary>
    public partial class Niv2Main : Page
    {
        public Niv2Main()
        {
            InitializeComponent();
            initialiserLangue();
            coursimg.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Booksicon.png", UriKind.RelativeOrAbsolute));
            testimg.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/StopSignicon.png", UriKind.RelativeOrAbsolute));
        }

        private void niv2cours(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau2.Lesson();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveaux();
        }

        private void mouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 340;
            button.Width = 240;
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 300;
            button.Width = 200;
        }
        private void test_niv2(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Tests2();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                l0.Text = "Cours";
                l1.Text = "Test de niveau";
                backtxt.Text = "Retour";
            }
            else
            {
                //la langue arabe
                l0.Text = "الدروس";
                l1.Text = "امتحان المستوى";
                backtxt.Text = "عودة";
            }
        }
    }
}
