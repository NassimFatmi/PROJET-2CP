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
using System.Data;
using System.Data.SqlClient;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for Niveaux.xaml
    /// </summary>
    public partial class Niveaux : Page
    {
        private int _test1;
        private int _test2;
        private int _test3;

        public Niveaux()
        {
            InitializeComponent();
            progressLevels();
            initialiserLangue();
            guestMode();
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new PagePrincipale();
        }

        private void niveau1Click(object sender, RoutedEventArgs e)
        {
            //Selection du niveau 1
            //Show niv 1 main page
            Home.mainFrame.Content = new Niveau1.Niv1Main();
            
        }

        private void niv2_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niveau2.Niv2Main();
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                l0.Text = "Niveau 1";
                l1.Text = "Niveau 2";
                l2.Text = "Niveau 3";
                back.Text = "Retour";
            }
            else
            {
                //la langue arabe
                l0.Text = "المستوى 1";
                l1.Text = "المستوى 2";
                l2.Text = "المستوى 3";
                back.Text = "عودة";
            }
        }
        /*<<
            Decrease advantages of the guest user
        >>*/
        private void guestMode()
        {
            if (LogIN.LoggedUser.ID == 0 && LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
            {
                niv2.IsEnabled = false;
                niv3.IsEnabled = false;

                if(MainWindow.langue == 0)
                {   //la langue français
                    niv2.ToolTip = "Créer compte pour vous aurez le droit d'accée a le NIVEAU 2";
                    niv3.ToolTip = "Créer compte pour vous aurez le droit d'accée a le NIVEAU 3";
                }
                else
                {
                    //la langue arabe
                    niv2.ToolTip = "يجب عليك فتح حساب لتستطيع الدخول للمستوى 2";
                    niv3.ToolTip = "يجب عليك فتح حساب لتستطيع الدخول للمستوى 3";
                }
            }
        }

        private void niv3_Click(object sender, RoutedEventArgs e)
        {
            //Home.mainFrame.Content = new Niveau3.Niv3Main();
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

        private void mouseEnterLocked(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 340;
            button.Width = 240;
            if (MainWindow.langue == 0)
                lockmessage.Content = "Pour débloquer se niveau il faut compléter les tests de niveau précedent";
            else
                lockmessage.Content = "يجب عليك انهاء امتحانات المستوى السابق لفتح هدا المستوى";
        }

        private void mouseLeaveLocked(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 300;
            button.Width = 200;
            lockmessage.Content = "";
        }
        private void progressLevels()
        {
            // Code == ID //
            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
            DataTable savedData = new DataTable();
            SqlConnection connectToUtilisateur = new SqlConnection(connString);

            SqlCommand cmd;

            string query = "SELECT * FROM Utilisateur WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

            if (connectToUtilisateur.State == ConnectionState.Closed)
                connectToUtilisateur.Open();

            using (connectToUtilisateur)
            {
                try
                {
                    cmd = new SqlCommand(query, connectToUtilisateur);

                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(savedData);
                    da.Dispose();

                    if (savedData.Rows[0]["Test1"].ToString().Equals(""))
                    {
                        lvl2lock.Visibility = Visibility.Visible;
                        lvl3lock.Visibility = Visibility.Visible;
                        niv2.MouseEnter += new MouseEventHandler(this.mouseEnterLocked);
                        niv2.MouseLeave += new MouseEventHandler(this.mouseLeaveLocked);
                        niv3.MouseEnter += new MouseEventHandler(this.mouseEnterLocked);
                        niv3.MouseLeave += new MouseEventHandler(this.mouseLeaveLocked);
                    }
                    else
                    {
                        if (Int32.Parse(savedData.Rows[0]["Test1"].ToString()) == 10)
                        {
                            lvl2lock.Visibility = Visibility.Collapsed;
                            niv2.Click += new RoutedEventHandler(this.niv2_Click);
                            niv2.MouseEnter += new MouseEventHandler(this.mouseEnter);
                            niv2.MouseLeave += new MouseEventHandler(this.mouseLeave);
                            niv3.MouseEnter += new MouseEventHandler(this.mouseEnterLocked);
                            niv3.MouseLeave += new MouseEventHandler(this.mouseLeaveLocked);
                        }
                    }
                    if (!savedData.Rows[0]["Test2"].ToString().Equals(""))
                    {
                        if (Int32.Parse(savedData.Rows[0]["Test2"].ToString()) == 10)
                        {
                            lvl3lock.Visibility = Visibility.Collapsed;
                            niv3.Click += new RoutedEventHandler(this.niv3_Click);
                            niv3.MouseEnter += new MouseEventHandler(this.mouseEnter);
                            niv3.MouseLeave += new MouseEventHandler(this.mouseLeave);
                        }
                    }
                    if (connectToUtilisateur.State == ConnectionState.Open)
                        connectToUtilisateur.Close();
                }
                catch (Exception)
                {
                    if (connectToUtilisateur.State == ConnectionState.Open)
                        connectToUtilisateur.Close();

                    MessageBox.Show("error Get LastTest Niveaux ");
                }
            }
        }
    }
}
