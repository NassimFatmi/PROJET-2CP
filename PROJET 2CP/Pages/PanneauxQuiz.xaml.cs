
using System.Windows;
using System.Windows.Controls;


namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour PanneauxQuiz.xaml
    /// </summary>
    public partial class PanneauxQuiz : Page
    {
        public static int langue { get; set; } = PROJET_2CP.MainWindow.langue;

        public PanneauxQuiz()
        {
            InitializeComponent();
            configurerLaLangue();
        }
        private void configurerLaLangue()
        {
            if(langue== 0)
            {
                btn.Content = "signalisation";
                btn2.Content = "Tests Niveau 1";
              switch_lang.Content = "changer la langue en arabe";
            }
            if (langue== 1)
            {
               switch_lang.Content = "تغيير اللغة الى الفرنسية";
                btn.Content = "إشارات المرور";
                btn2.Content = "إمتحانات المستوى الأول";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Leçons();
        }
       private void switch_lang_Click(object sender, RoutedEventArgs e)
        {
            bool changed = false;
            if (langue == 0 && !changed)
            {
                btn.Content = "إشارات المرور";
                btn2.Content = "إمتحانات المستوى الأول";
                switch_lang.Content = "تغيير اللغة الى الفرنسية";
                switch_lang.Margin = new Thickness(692, 82, 26, 558);
                langue = 1;
                MainWindow.langue = 1;
                changed = true;
            }
            if (langue == 1 && !changed)
            {
                btn.Content = "signalisation";
                btn2.Content = "Tests Niveau 1";
                switch_lang.Content = "changer la langue en arabe";
                switch_lang.Margin = new Thickness(26, 82, 692, 558);
                langue = 0;
                MainWindow.langue = 0;
                changed = true;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Tests1();
        }
    }
}
