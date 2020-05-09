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

namespace PROJET_2CP.Niveau1
{
    /// <summary>
    /// Interaction logic for Niv1Main.xaml
    /// </summary>
    public partial class Niv1Main : Page
    {
        public Niv1Main()
        {
            InitializeComponent();
            initialiserLangue();
            guestMode();
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            //Retoure button click
            //Retoure vers la page précedente Niveaux
            Home.mainFrame.Content = new Niveaux();
        }

        private void startTest(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Tests1();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Leçons();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                l0.Text = "Cours";
                l1.Text = "Test de niveau";
            }
            else
            {
                //la langue arabe
                l0.Text = "الدروس";
                l1.Text = "امتحان المستوى";
            }
        }
        /*<<
            Decrease advantages of the guest user
        >>*/
        private void guestMode()
        {
            if (LogIN.LoggedUser.ID == 0 && LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
            {
                testnivbtn.IsEnabled = false;

                if (MainWindow.langue == 0)
                {   //la langue français
                    testnivbtn.ToolTip = "Créer compte pour vous aurez le droit d'accée a le Test";
                }
                else
                {
                    //la langue arabe
                    testnivbtn.ToolTip = "يجب عليك فتح حساب لتستطيع الدخول للامتحان";
                }
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
    }
}
