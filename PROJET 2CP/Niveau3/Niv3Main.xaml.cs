using PROJET_2CP.Pages;
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
    /// Interaction logic for Niv3Main.xaml
    /// </summary>
    public partial class Niv3Main : Page
    {
        public Niv3Main()
        {
            InitializeComponent();
            coursimg.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Booksicon.png", UriKind.RelativeOrAbsolute));
            testimg.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/StopSignicon.png", UriKind.RelativeOrAbsolute));
            language();
        }

        private void niv2cours(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau3();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new PROJET_2CP.Niveaux();
        }
        private void language()
        {
            if(MainWindow.langue == 0)
            {//langue francais
                back.Text = "Retour";
                niveauLbl.Content = "NIVEAU III";
                coursbtn.Text = "Cours";
                testnv.Text = "Test Niveau";
            }
            else
            {//langue arabe
                back.Text = "عودة";
                niveauLbl.Content = "المستوى 3";
                coursbtn.Text = "الدروس";
                testnv.Text = "امتحان المستوى";
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Tests3();
        }
    }
}
