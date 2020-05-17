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
using PROJET_2CP.Noyau;
using PROJET_2CP.Pages;

namespace PROJET_2CP.Niveau2
{
    /// <summary>
    /// Interaction logic for LessonContent.xaml
    /// </summary>
    public partial class LessonContent : Page
    {// recuperer le numero du panneau choisit
        public static int panneau;
        private NLeçon lesson;
        private int bi = 0;
        private int bs = 0;
        private int langue = MainWindow.langue;

        public LessonContent(int a, int b)
        {
            lesson = new NLeçon();
            InitializeComponent();
            bi = a;
            bs = b;
            creerPanneau();
            if (langue == 0)
                back.Text = "Retour";
            else
                back.Text = "عودة";
        }
        // creation dynamic des boutton contenant les panneaux
        private void creerPanneau()
        {
            //initialisation de titre
            if(langue == 0)
            {
                if (bi == 95 && bs == 128)
                    lessonName.Content = "Niveau 2 : Les indications diverses";
                if (bi == 129 && bs == 172)
                    lessonName.Content = "Niveau 2 : Les indications utiles";
                if (bi == 173 && bs == 184)
                    lessonName.Content = "Niveau 2 : La signalisation temporaire";
                if (bi == 185 && bs == 212)
                    lessonName.Content = "Niveau 2 : Les panneaux directionnels";
                if (bi == 213 && bs == 228)
                    lessonName.Content = "Niveau 2 : Les balises";
                if (bi == 229 && bs == 254)
                    lessonName.Content = "Niveau 2 : Le marquage au sol";
            }
            else if(langue == 1)
            {
                if (bi == 95 && bs == 128)
                    lessonName.Content = "المستوى 2 : مؤشرات مختلفة";
                if (bi == 129 && bs == 172)
                    lessonName.Content = "المستوى 2 : مؤشرات مفيدة ";
                if (bi == 173 && bs == 184)
                    lessonName.Content = "المستوى 2 : لافتات مؤقتة";
                if (bi == 185 && bs == 212)
                    lessonName.Content = "المستوى 2 : علامات الاتجاه"; 
                if (bi == 213 && bs == 228)
                    lessonName.Content = "المستوى 2 : العلامات"; 
                if (bi == 229 && bs == 254)
                    lessonName.Content = "المستوى 2 :التأشير الأرضي";
            }

            // ceation d'un dynamic button
            Button b = new Button();
            // creation d'un stack panel qui va contenir 9 bouttons horizontallement
            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            // declaration d'une variable qui va compter jusqu'à 9 (le nombre de bouttons)
            int k = 1;

            for (int i = bi; i <= bs; i++)
            {
                // definition des proprietes des buttons
                b.HorizontalAlignment = HorizontalAlignment.Left;
                b.FontSize = 24;
                b.Margin = new Thickness(10, 10, 10, 10);
                b.Width = 140;
                b.Height = 120;
                b.Background = null;
                b.BorderBrush = null;
                // ajouter un handler quand le curseur quitte ce button
                b.MouseLeave += button_MouseLeave;
                //ajouter un handler quand on clicke sur le boutton
                b.Click += Button_Click;
                //ajouter un tag pour le boutton
                b.Tag = i;
                // ajout du panneau au boutton
                b.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + Convert.ToString(i) + "_off");

                // ajout du boutton au stack panel
                s.Children.Add(b);
                // si nous avons ajouté 9 boutton horizontal  on ajoute le stack panel s au stack panel vertical sp
                if (k == 5)
                {
                    // on remet le compteur à 0
                    k = 0;
                    sp.Children.Add(s);
                    // creation d'un nouveau stack panel horizontal
                    s = new StackPanel();
                    s.Orientation = Orientation.Horizontal;
                }
                // creation d'un nouveau boutton
                b = new Button();
                // incrementaion du compteur
                k++;
            }
            // ajouter le dernier stack panel horizontal s restant
            sp.Children.Add(s);
        }
        // lorsqu'on click sur un panneau
        private void Button_Click(object sender, RoutedEventArgs e) //Event which will be triggerd on click of ya button
        {
            panneau = (int)((Button)sender).Tag;
            Home.mainFrame.Content = new DecrirePanneau(bi, bs, "signalisation2");
        }
        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var index = (int)button.Tag;
            button.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + Convert.ToString((int)button.Tag) + "_on");

        }
        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var index = (int)button.Tag;
            button.Content = lesson.ajouterImage(AppDomain.CurrentDomain.BaseDirectory + "/Images/" + Convert.ToString((int)button.Tag) + "_off");

        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Lesson();
        }
    }
}
