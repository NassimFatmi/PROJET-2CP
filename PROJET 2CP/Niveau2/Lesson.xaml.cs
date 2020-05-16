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

namespace PROJET_2CP.Niveau2
{
    /// <summary>
    /// Interaction logic for Lesson.xaml
    /// </summary>
    public partial class Lesson : Page
    {
        private int langue = MainWindow.langue;
        public static int niveau2ThemeSelected;
        public Lesson()
        {
            InitializeComponent();
            quiz0.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz2.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz3.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz4.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz5.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz6.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz7.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz8.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz9.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz10.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz11.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            quiz12.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/quizicon.png", UriKind.RelativeOrAbsolute));
            configurerLaLangue();
        }
        private void configurerLaLangue()
        {
            if (langue == 0)
            {
                txt1.Text = "Le Conducteur";
                txt2.Text = "Le Véhicule";
                txt3.Text = "Les situations particulieres de conduite";

                l1.Content = "Les indications diverses";
                l2.Content = "Les indications utiles";
                l3.Content = "La signalisation temporaire";
                l4.Content = "Les panneaux directionnels";
                l5.Content = "Les balises";
                l6.Content = "Le marquage au sol";

                q1txt.Text = "Questions";
                q2txt.Text = "Questions";
                q3txt.Text = "Questions";
                q4txt.Text = "Questions";
                q5txt.Text = "Questions";
                q6txt.Text = "Questions";
                def.Content = "Les défaillances du conducteur";
                vit.Content = "Vitesse et Distance";
                dist.Content = "Distance en conduite";
                quiz_defailance.Content = "Quiz1";
                quiz_distance.Content = "Quiz2";
                quiz_vitesse.Content = "Quiz3";
                les_feux.Content = "Les feux du vehicule";
                securite.Content = "La securité en voiture";
                quiz_les_feux.Content = "Quiz1";
                quiz_securite.Content = "Quiz2";
                utilisation.Content = "Utilisation des feux";
                risque.Content = "Risques de la conduite sous \n        les intempéries";
                quiz_utilisation.Content = "Quiz1";
                quiz_risque.Content = "Quiz2";

                thm1.Content = "Désignation";
                thm2.Content = "Désignation";
                thm3.Content = "Désignation";
                thm4.Content = "Désignation";

            }
            if (langue == 1)
            {
                txt1.Text = "السائق";
                txt2.Text = "المركبة";
                txt3.Text = "حالات القيادة الخاصة";

                l1.Content = "مؤشرات مختلفة";
                l2.Content = "مؤشرات مفيدة";
                l3.Content = "لافتات مؤقتة";
                l4.Content = "علامات الاتجاه";
                l5.Content = "العلامات";
                l6.Content = "التأشير الأرضي";

                q1txt.Text = "الأسئلة";
                q2txt.Text = "الأسئلة";
                q3txt.Text = "الأسئلة";
                q4txt.Text = "الأسئلة";
                q5txt.Text = "الأسئلة";
                q6txt.Text = "الأسئلة";
                txt1.Text = "الموضوع 2";
                txt2.Text = "الموضوع 4";
                txt3.Text = "الموضوع 5";
                lbl1.Content = lbl.Content = lbl2.Content = ": الدروس";
                lbl.HorizontalAlignment = lbl1.HorizontalAlignment = lbl2.HorizontalAlignment = HorizontalAlignment.Right;
                def.Content = "فشل السائق";
                vit.Content = "السرعات والمسافات";
                dist.Content = "مسافة القيادة";
                quiz_defailance.Content = "إمتحان 1";
                quiz_distance.Content = "إمتحان 2";
                quiz_vitesse.Content = "إمتحان 3";
                les_feux.Content = "أضواء السيارة";
                securite.Content = "السلامة في السيارة";
                quiz_les_feux.Content = "إمتحان 1";
                quiz_securite.Content = "إمتحان 2";
                utilisation.Content = "استخدام الأضواء";
                risque.Content = "مخاطر القيادة في الأحوال  \n      الجوية السيئة";
                quiz_utilisation.Content = "إمتحان 1";
                quiz_risque.Content = "إمتحان 2";

                thm1.Content = "حول الدرس";
                thm2.Content = "حول الدرس";
                thm3.Content = "حول الدرس";
                thm4.Content = "حول الدرس";
            }
        }
        private void l1_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new LessonContent(95, 128);
        }

        private void l2_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new LessonContent(129, 172);
        }

        private void l3_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new LessonContent(173, 184);
        }

        private void l4_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new LessonContent(185, 212);
        }

        private void l5_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new LessonContent(213, 228);
        }

        private void l6_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new MarquageAuSol(229, 254);
        }

        private void q1_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Quiz(50, 56);
        }

        private void q2_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Quiz(56, 62);
        }

        private void q3_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Quiz(62, 68);
        }

        private void q4_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Quiz(68, 74);

        }

        private void q5_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Quiz(74, 80);
        }

        private void q6_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Pages.Quiz(80, 86);
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niv2Main();
        }
        private void defailance_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new defailance();
        }
        private void distance_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new distance();
        }
        private void vitesse_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new vitesse();
        }
        private void quiz_defailance_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 2;

            Home.mainFrame.Content = new Quiz(7, 13);
        }

        private void quiz_distance_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 2;

            Home.mainFrame.Content = new Quiz(19, 25);
        }

        private void quiz_vitesse_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 2;

            Home.mainFrame.Content = new Quiz(1, 7);
        }
        private void les_feux_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new les_feux();
        }
        private void securité_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new la_securité();
        }

        private void quiz_les_feux_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 3;

            Home.mainFrame.Content = new Quiz(43, 49);
        }

        private void quiz_securite_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 3;

            Home.mainFrame.Content = new Quiz(49, 55);
        }
        private void utilisation_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new utilisation();
        }
        private void risque_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new risque();
        }

        private void quiz_utilisation_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 4;

            Home.mainFrame.Content = new Quiz(79, 85);
        }

        private void quiz_risque_Click(object sender, RoutedEventArgs e)
        {
            niveau2ThemeSelected = 4;

            Home.mainFrame.Content = new Quiz(73, 79);
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
                designatiotxt.Text = "      La circulation routière est la thématique la plus dense du code de la route\n" +
                                     "      La signalisation routière :\n\n" +
                                     "          *inclut les marquages au sol, les panneaux directionnels, les indications \n" +
                                     "           diverses et les indications utiles";
            }
            else
            {
                designatiotxt.TextAlignment = TextAlignment.Right;
                designatiotxt.Text = "      حركة المرور على الطرق تمثل الموضوع الأكثر أهمية في قانون المرور\n" +
                                     "          *يشمل العلامات الموجودة على الأرض، إشارات الاتجاه، وإشارات الإرشاد.";

            }
        }

        private void thm2_Click(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Visible;
            if (MainWindow.langue == 0)
            {
                designatiotxt.TextAlignment = TextAlignment.Left;
                designatiotxt.Text = "          Le thème du conducteur explore les bonnes pratiques pour conduire en toute sécurité et les facteurs de\n" +
                    "        risques.Cette thématique comprend donc les notions liées à comment conduire, au fait de savoir si un usager\n" +
                    "        est ou non dans un bon état pour conduire et à comment adapter sa conduite à certains comportements. Elle\n" +
                    "        regroupe les différents sous - thèmes :\n" +
                    "           * Utilisation des rétroviseurs.\n" +
                    "           *Vitesses et distances.\n" +
                    "           *Distances en conduite.\n" +
                    "           *Défaillances du conducteur.\n" +
                    "           *Principes d’une conduite sure.";
            }
            else
            {
                designatiotxt.TextAlignment = TextAlignment.Right;
                designatiotxt.Text = "      يعرض هذا الموضوع عوامل الخطر وأفضل السلوكيات للقيادة الآمنة. وبالتالي، فإنه يتضمن المفاهيم المتعلقة بكيفية القيادة، ما إذا كان\n" +
                                     "          المستخدم في حالة جيدة للقيادة أم لا وكيفية تكييف سلوكه مع مختلف الوضعيات والظروف من خلال دروس التالية:\n" +
                                     "          *استخدام المرايا.\n" +
                                     "          *السرعات والمسافات.\n" +
                                     "          *مسافات القيادة.\n" +
                                     "          *فشل السائق.\n" +
                                     "          *مبادئ القيادة الآمنة.";

            }
        }

        private void thm3_Click(object sender, RoutedEventArgs e)
        {
            designationGrid.Visibility = Visibility.Visible;
            if (MainWindow.langue == 0)
            {
                designatiotxt.TextAlignment = TextAlignment.Left;
                designatiotxt.Text = "          Ce thème regroupe l’ensemble des notions liées à l’administration et aux règles administratives, comme :\n" +
                                     "          *Les documents administratifs, et ceci qu’ils soient obligatoires ou facultatifs.\n" +
                                     "          *Le chargement des véhicules en fonctions de leurs PTAC.\n" +
                                     "          *Le permis de conduire à points, et la façon dont les points peuvent être retirées ou récupérées.\n" +
                                     "          *Les infractions et les délits commises à l’encontre du code de la route et les sanctions associées";
            }
            else
            {
                designatiotxt.TextAlignment = TextAlignment.Right;
                designatiotxt.Text = "      يغطي هذا الموضوع كل ما يتعلق بميكانيك المركبات وتشغيل معداتها. يتضمن المواضيع الفرعية التالية:\n" +
                    "           أضواء السيارة واستخداماتها.\n" +
                    "           السلامة في السيارة.\n" +
                    "           التعرف على كيفية استخدام علبة السرعة.\n" +
                    "           التعرف على كيفية الانطلاق والتوقف.\n" +
                    "           التعرف على المركبة.";

            }
        }
    }
}
