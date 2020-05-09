
using System.Windows.Controls;
using System.Windows.Media;
using PROJET_2CP.Noyau;
namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour SignalisationAr.xaml
    /// </summary>
    public partial class SignalisationAr : Page
    {
        public SignalisationAr()
        {
            InitializeComponent();
        }

        private void backClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Leçons();
        }
    }
}
