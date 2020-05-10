using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using PROJET_2CP.Niveau2;
using PROJET_2CP.Niveau1;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Interaction logic for Tests2.xaml
    /// </summary>
   
        public partial class Tests2 : Page
        {
           
           
            // lire cette donnée à partir de la base de donnée de l'utilisateur
            public static int testActuel { get; set; } = 1;

            public static int _testChoisi { get; set; }
            public Tests2()
            {
                InitializeComponent();
                logoimg.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/EDautoEcole.png", UriKind.RelativeOrAbsolute));
                b1.Tag = 1;
                b2.Tag = 2;
                b3.Tag = 3;
                b4.Tag = 4;
                b5.Tag = 5;
                b6.Tag = 6;
                b7.Tag = 7;
                getLastTest();
                
                
            }
            static int[] reorder(int a)
            {
                int[] tab = new int[a];
                int size = 0, i;
                Random rd = new Random();
                while (size < a)
                {
                    i = rd.Next(1, a + 1);
                    if (!exist(i, tab))
                    {
                        tab[size] = i;
                        size++;
                    }
                }
                return tab;
            }
            static bool exist(int a, int[] t)
            {
                bool ext = false;

                foreach (int el in t)
                {
                    if (el == a)
                    {
                        ext = true;
                    }
                }
                return ext;
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(7, 1,19);
                _testChoisi = 1;
            }

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(9, 43, 49);
                _testChoisi = 2;

            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(11, 79, 73);
                _testChoisi = 3;
            }

            private void Button_Click_3(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(3, 21, 45);
                _testChoisi = 4;
            }

            private void Button_Click_4(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(5, 51, 81);
                _testChoisi = 5;
            }

            private void Button_Click_5(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(23, 47, 53);
                _testChoisi = 6;
            }

            private void Button_Click_6(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new TestNiveau2(75, 83, 77);
            _testChoisi = 7;
        }

           

            private void choix_Test(object sender, RoutedEventArgs e)
            {
                if (getnote(2, 1) > 5)
                b1.Background = Brushes.GreenYellow;
                else if (getnote(2, 1) == 5)
                    b1.Background = Brushes.Orange;
                else if (getnote(2, 1) == -1)
                    b1.Background = Brushes.BlueViolet;
                else if (getnote(2, 1) < 5)
                    b1.Background = Brushes.Red;

                if (getnote(2, 2) > 5)
                    b2.Background = Brushes.GreenYellow;
                else if (getnote(2, 2) == 5)
                    b2.Background = Brushes.Orange;
                else if (getnote(2, 2) == -1)
                    b2.Background = Brushes.BlueViolet;
                else if (getnote(2, 2) < 5)
                    b2.Background = Brushes.Red;


                if (getnote(2, 3) > 5)
                    b3.Background = Brushes.GreenYellow;
                else if (getnote(2, 3) == 5)
                    b3.Background = Brushes.Orange;
                else if (getnote(2, 3) == -1)
                    b3.Background = Brushes.BlueViolet;
                else if (getnote(2, 3) < 5)
                    b3.Background = Brushes.Red;

                if (getnote(2, 5) > 5)
                    b4.Background = Brushes.GreenYellow;
                else if (getnote(2, 5) == 5)
                    b4.Background = Brushes.Orange;
                else if (getnote(2, 6) == -1)
                    b4.Background = Brushes.BlueViolet;
                else if (getnote(2, 6) < 5)
                    b4.Background = Brushes.Red;


                if (getnote(2, 6) > 5)
                    b5.Background = Brushes.GreenYellow;
                else if (getnote(2, 6) == 5)
                    b5.Background = Brushes.Orange;
                else if (getnote(2, 6) == -1)
                    b5.Background = Brushes.BlueViolet;
                else if (getnote(2, 6) < 5)
                    b5.Background = Brushes.Red;

                if (getnote(2, 7) > 5)
                    b7.Background = Brushes.GreenYellow;
                else if (getnote(2, 7) == 5)
                    b7.Background = Brushes.Orange;
                else if (getnote(2, 7) == -1)
                    b7.Background = Brushes.BlueViolet;
                else if (getnote(2, 7) < 5)
                    b7.Background = Brushes.Red;


               

                if (testActuel == 1)
                {
                    b2.IsEnabled = false;
                    b3.IsEnabled = false;
                    b4.IsEnabled = false;
                    b5.IsEnabled = false;
                    b6.IsEnabled = false;
                    b7.IsEnabled = false;
                    
                    saveLastTest(1);
                }
                if (testActuel == 2)
                {
                    b2.IsEnabled = true;
                    b3.IsEnabled = false;
                    b4.IsEnabled = false;
                    b5.IsEnabled = false;
                    b6.IsEnabled = false;
                    b7.IsEnabled = false;
                   
                    saveLastTest(2);
                }
                if (testActuel == 3)
                {
                    b3.IsEnabled = true;
                    b4.IsEnabled = false;
                    b5.IsEnabled = false;
                    b6.IsEnabled = false;
                    b7.IsEnabled = false;
                    
                    saveLastTest(3);
                }
                if (testActuel == 4)
                {
                    b4.IsEnabled = true;
                    b5.IsEnabled = false;
                    b6.IsEnabled = false;
                    b7.IsEnabled = false;
                    
                    saveLastTest(4);
                }
                if (testActuel == 5)
                {
                    b5.IsEnabled = true;
                    b6.IsEnabled = false;
                    b7.IsEnabled = false;
                   
                    saveLastTest(5);
                }
                if (testActuel == 6)
                {
                    b6.IsEnabled = true;
                    b7.IsEnabled = false;
                   
                    saveLastTest(6);
                }
                if (testActuel == 7)
                {
                    b7.IsEnabled = true;
                   
                    saveLastTest(7);
                }
               
            }

            /// <summary>
            /// get l'état d'avancement 
            /// </summary>
            /// <param name="testCode"></param> le dérnier test
            private void getLastTest()
            {
                // Code == ID //
                string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
                DataTable savedData = new DataTable();
                SqlConnection connectToUtilisateur = new SqlConnection(connString);

                SqlCommand cmd;

                string query = "SELECT * FROM Utilisateur WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

                if (connectToUtilisateur.State == ConnectionState.Closed)
                    connectToUtilisateur.Open();

                try
                {
                    cmd = new SqlCommand(query, connectToUtilisateur);

                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(savedData);
                    da.Dispose();

                    if (savedData.Rows[0]["Test2"].ToString().Equals(""))
                        testActuel = 1;
                    else
                        testActuel = Int32.Parse(savedData.Rows[0]["Test2"].ToString());

                    if (connectToUtilisateur.State == ConnectionState.Open)
                        connectToUtilisateur.Close();
                }
                catch (Exception)
                {
                    if (connectToUtilisateur.State == ConnectionState.Open)
                        connectToUtilisateur.Close();

                    MessageBox.Show("error Get LastTest Testniveau 2 ");
                }
            }
            /// <summary>
            /// Save the last test
            /// </summary>
            /// <param name="testCode"></param> the code of the test
            private void saveLastTest(int testCode)
            {
                // Code == ID //
                string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security=True";
                string query = "UPDATE Utilisateur SET Test2='" + testCode.ToString() + "' WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

                SqlConnection connecttoUsers = new SqlConnection(connString);

                if (connecttoUsers.State == ConnectionState.Closed)
                    connecttoUsers.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, connecttoUsers);

                    cmd = new SqlCommand(query, connecttoUsers);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    if (connecttoUsers.State == ConnectionState.Open)
                        connecttoUsers.Close();
                }
                catch (Exception)
                {
                    if (connecttoUsers.State == ConnectionState.Open)
                        connecttoUsers.Close();

                    MessageBox.Show("error save last test Testniveau 2 ");
                }
            }
            private int getnote(int niveau, int test)
            {
                // Code == ID //
                string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
                DataTable savedData = new DataTable();
                SqlConnection connectToUtilisateur = new SqlConnection(connString);

                SqlCommand cmd;

                string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "NoteTest WHERE Niveau = '" + niveau.ToString() + "' AND Test = '" + test.ToString() + "'";

                if (connectToUtilisateur.State == ConnectionState.Closed)
                    connectToUtilisateur.Open();

            try
            {
                cmd = new SqlCommand(query, connectToUtilisateur);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(savedData);
                da.Dispose();
                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();

                if (savedData.Rows.Count == 1)
                {
                    if (savedData.Rows[0]["Note"].ToString().Equals(""))
                        return -1;
                    else
                        return Int32.Parse(savedData.Rows[0]["Note"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();

                MessageBox.Show("error Get note Testniveau 2 ");
                return -1;
            }
            }

            private void backClick(object sender, RoutedEventArgs e)
            {
                Home.mainFrame.Content = new Niv2Main();
            }

            private void b1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
            {
                var button = sender as Button;
                button.BorderThickness = new Thickness(8);
                button.BorderBrush = Brushes.Azure;
                noteShow.Content = "Test Note : " + getnote(1, ((int)button.Tag)).ToString();
            }

            private void b1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
            {
                var button = sender as Button;
                button.BorderThickness = new Thickness(0);
                button.BorderBrush = null;
                noteShow.Content = "";
            }
        }
}
