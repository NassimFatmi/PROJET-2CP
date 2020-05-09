using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PROJET_2CP.Noyau;
using PROJET_2CP.Pages;

namespace PROJET_2CP
{
    /// <summary>
    /// Logique d'interaction pour Panneaux.xaml
    /// </summary>
    public partial class Panneaux : Page
    {
        // recuperer le numero du panneau choisit
        public static int panneau;
        private NLeçon lesson;

        public Panneaux()
        {
            lesson = new NLeçon();
            InitializeComponent();
            initialiserLangue();
            creerPanneau();
            creerPanneau();
            DecrirePanneau.lastPage = 1;
        }
        // creation dynamic des boutton contenant les panneaux
        private void creerPanneau()
        {
            // creation d'un dynamic button
            Button b = new Button();
            // creation d'un stack panel qui va contenir 9 bouttons horizontallement
            StackPanel s = new StackPanel();
            s.Orientation = Orientation.Horizontal;
            // declaration d'une variable qui va compter jusqu'à 9 (le nombre de bouttons)
            int k = 1;

            for (int i = 1; i <=30; i++)
            {
                // definition des proprietes des buttons
                b.HorizontalAlignment = HorizontalAlignment.Left;
                b.FontSize = 24;
                b.Margin = new Thickness(10, 10, 10, 10);
                b.Width = 140;
                b.Height = 120;
                b.Background = null;
                b.BorderBrush = null;
                // ajouter un handler quand le curseur est sur ce boutton
                b.MouseEnter += button_MouseEnter;
                // ajouter un handler quand le curseur quitte ce button
                b.MouseLeave += button_MouseLeave;
                //ajouter un handler quand on clicke sur le boutton
                b.Click += Button_Click;
                //ajouter un tag pour le boutton
                b.Tag = i;
                // ajout du panneau au boutton
                b.Content = lesson.ajouterImage("Images/" + Convert.ToString(i) + "_off");
                // ajout du boutton au stack panel
                s.Children.Add(b);
                // si nous avons ajouté 9 boutton horizontal  on ajoute le stack panel s au stack panel vertical sp
                if (k ==5)
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
            Home.mainFrame.Content= new DecrirePanneau(1,30,"danger");
            //des.ShowDialog();
        }
        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var index = (int)button.Tag;
            button.Content = lesson.ajouterImage("Images/" + Convert.ToString((int)button.Tag) + "_on");
        }
        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            var index = (int)button.Tag;
            button.Content = lesson.ajouterImage("Images/" + Convert.ToString((int)button.Tag) + "_off");
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Leçons();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                h0.Content = "Niveau 1 : Panneaux de danger";
                h1.Content = "Clicke sur le panneau pour la description ";
                back.Text = "Retoure";
            }
            else
            {
                //la langue arabe
                h0.Content = "المستوى 1 :لافتات الخطر";
                h1.Content = "انقر على الافتة للشرح ";
                back.Text = "عودة";
            }
        }
    }
}
