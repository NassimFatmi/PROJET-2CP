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
    /// Interaction logic for PagePrincipale.xaml
    /// </summary>
    public partial class PagePrincipale : Page
    {
        public PagePrincipale()
        {
            InitializeComponent();
            back2image.Fill = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/back2.png", UriKind.RelativeOrAbsolute)));
            guestMode();
            initialiserLangue();

            if (SignIN._Commencer == 1)
            {
                commencertxt.Visibility = Visibility.Visible;
                Reprendretxt.Visibility = Visibility.Collapsed;
            }
            else
            {
                commencertxt.Visibility = Visibility.Collapsed;
                Reprendretxt.Visibility = Visibility.Visible;
            }
            
            
        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveaux();
        }

        private void stateClick(object sender, RoutedEventArgs e)
        {
            //Click sur btn statistique
            //Show la page des statistiques
            Home.mainFrame.Content = new Statistiques();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                commencertxt.Text = "Commencer";
                Reprendretxt.Text = "Reprendre";
                statestxt.Text = "Statistiques";
                parametretxt.Text = "Paramètres";
            }
            else
            {
                //la langue arabe
                commencertxt.Text = "ابدأ";
                statestxt.Text = "احصائيات";
                parametretxt.Text = "اعدادات";
                Reprendretxt.Text = "تابع التعلم";
            }
        }
        /*<<
            Decrease advantages of the guest user
        >>*/
        private void guestMode()
        {
            if (LogIN.LoggedUser.ID == 0 && LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
            {
                settingsHomebtn.IsEnabled = false;
                stateHomebtn.IsEnabled = false;
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

        private void settingsHomebtn_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Settings();
        }
    }
}
