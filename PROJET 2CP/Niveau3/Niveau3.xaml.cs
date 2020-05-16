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
    /// Logique d'interaction pour Niveau2.xaml
    /// </summary>
    public partial class Niveau3 : Page
    {
        public static int niveau3ThemeSelected;

        public Niveau3()
        {
            InitializeComponent();
            quiz0.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz4.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz5.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            language();
        }

        private void principe_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new principe();
        }


        private void quiz_principe_Click(object sender, RoutedEventArgs e)
        {
            niveau3ThemeSelected = 1;
            Home.mainFrame.Content = new Quiz(13, 19);
        }


        private void language()
        {
            if (MainWindow.langue == 0)
            {
                principe.Content = "principe d une conduite sûre";
                quiz_principe.Content = "Quiz1";
                document.Content = "document et équipement";
                les_points.Content = "les points et les permis";
                quiz_document.Content = "Quiz1";
                quiz_les_points.Content = "Quiz2";
                connaitre.Content = "connaître son véhicule";
                demarrer.Content = "démarrer et s'arrêter";
                monter.Content = "monter ses vitesses et rétrograder";
                quiz_connaitre.Content = "Quiz1";
                quiz_demarrer.Content = "Quiz2";
                quiz_monter.Content = "Quiz3";

                thm1.Content = "Désignation";
                thm2.Content = "Désignation";
                thm3.Content = "Désignation";
            }
            if (MainWindow.langue == 1)
            {
                txt1.Text = "الموضوع 2";
                txt2.Text = "الموضوع 3";
                txt3.Text = "الموضوع 4";
                lbl1.Content = lbl.Content = lbl2.Content = "الدروس";
                lbl.HorizontalAlignment =lbl1.HorizontalAlignment= lbl2.HorizontalAlignment= HorizontalAlignment.Right;
                principe.Content = "مبدأ القيادة الآمنة";
                quiz_principe.Content = "إمتحان 1";
                document.Content = "الوثائق والمعدات";
                les_points.Content = "نقاط وتصاريح";
                quiz_document.Content = "إمتحان 1";
                quiz_les_points.Content = "إمتحان 2";
                connaitre.Content = "تعرف سيارتك";
                demarrer.Content = "الانطلاق و التوقف";
                monter.Content = "كيفية استعمال علبة السياقة";
                quiz_connaitre.Content = "إمتحان 1";
                quiz_demarrer.Content = "إمتحان 2";
                quiz_monter.Content = "إمتحان 3";

                thm1.Content = "حول الدرس";
                thm2.Content = "حول الدرس";
                thm3.Content = "حول الدرس";
            }
        }
        private void document_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new document();
        }
        private void les_points_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new les_ponints();
        }

        private void quiz_document_Click(object sender, RoutedEventArgs e)
        {
            niveau3ThemeSelected = 2;

            Home.mainFrame.Content = new Quiz(31, 37);
        }

        private void quiz_les_points_Click(object sender, RoutedEventArgs e)
        {
            niveau3ThemeSelected = 2;

            Home.mainFrame.Content = new Quiz(37, 43);
        }
        private void connaitre_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new connaitre();
        }
        private void demarrer_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new demarrer();
        }
        private void monter_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new monter();
        }
        private void quiz_connaitre_Click(object sender, RoutedEventArgs e)
        {
            niveau3ThemeSelected = 3;

            Home.mainFrame.Content = new Quiz(55, 61);
        }

        private void quiz_demarrer_Click(object sender, RoutedEventArgs e)
        {
            niveau3ThemeSelected = 3;

            Home.mainFrame.Content = new Quiz(67, 73);
        }

        private void quiz_monter_Click(object sender, RoutedEventArgs e)
        {
            niveau3ThemeSelected = 3;

            Home.mainFrame.Content = new Quiz(1, 7);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niv3Main();
        }

        private void quitdesignationClick(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Collapsed;
        }

        private void thm1_Click(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Visible;
            if (MainWindow.langue == 0)
            {
                designatiotxt.TextAlignment = TextAlignment.Left;
                designatiotxt.Text = "      Ce thème fait le point sur les différents types de route, les zones et conditions de conduite difficiles.\n" +
                                     "        C’est donc dans cette section que les candidats peuvent retrouver l’ensemble des règles liées :\n" +
                                     "              *A l’usage des feux.\n" +
                                     "              *Aux risques de la conduite sous les intempéries et comportements.\n" +
                                     "              *Aux tunnels et passage à niveau.\n" +
                                     "              *Aux règles de circulation sur l’autoroute.";
            }
            else
            {
                designatiotxt.TextAlignment = TextAlignment.Right;
                designatiotxt.Text = "      يستعرض هذا الموضوع الأنواع المختلفة من الطرق، المناطق الصعبة وظروف القيادة. لذلك في هذا القسم يمكن للمرشحين العثور على\n" +
                    "       جميع القواعد ذات صلة:\n" +
                    "       *باستخدام الأضواء.\n" +
                    "       *خطر القيادة في الطقس السيئ والسلوكيات الواجب اعتمادها.\n" +
                    "       *• الأنفاق ومعابر المستوى.\n" +
                    "       *قواعد المرور على الطريق السريع.";

            }
        }
    }
}
