using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media;
using PROJET_2CP.Niveau1;
using PROJET_2CP.update;
using System.Windows.Media.Imaging;

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour Tests1.xaml
    /// </summary>
    public partial class Tests1 : Page
    {
        private int[] tab1;
        private int[] tab2;
        // lire cette donnée à partir de la base de donnée de l'utilisateur
        public static int testActuel { get; set; } = 1;

        public static int _testChoisi { get; set; }
        public Tests1()
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
            b8.Tag = 8;
            b9.Tag = 9;
            b10.Tag = 10;
            getLastTest();
            langue();
        }
        private void langue()
        {
            if (MainWindow.langue == 0)
            {
                back.Text = "Retour";
                b1.Content = "Test 1";
                b2.Content = "Test 2";
                b3.Content = "Test 3";
                b4.Content = "Test 4";
                b5.Content = "Test 5";
                b6.Content = "Test 6";
                b7.Content = "Test 7";
                b8.Content = "Test 8";
                b9.Content = "Test 9";
                b10.Content ="Test 10";
                labelTest.Content = "Test Niveau 1";
            }
            else
            {
                b1.Content = "اختبار 1";
                b2.Content = "اختبار 2";
                b3.Content = "اختبار 3";
                b4.Content = "اختبار 4";
                b5.Content = "اختبار 5";
                b6.Content = "اختبار 6";
                b7.Content = "اختبار 7";
                b8.Content = "اختبار 8";
                b9.Content = "اختبار 9";
                b10.Content = "اختبار 10";
                labelTest.Content = "امتحان المستوى 1";
                back.Text = "عودة";
            }
        }
        public static int[] reorder(int a, int b)
        {
            int[] tab = new int[b - a + 1];
            int size = 0, i;
            Random rd = new Random();
            while (size < b - a + 1)
            {
                i = rd.Next(a, b + 1);
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
            Home.mainFrame.Content = new Page1Tests(1, 3, 25, 28, 1, 3);
            _testChoisi = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(4, 6, 29, 32, 4, 6);
            _testChoisi =2;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(7, 9, 33, 36, 7, 9);
            _testChoisi = 3;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(10, 12, 37, 40, 10, 12);
            _testChoisi = 4;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(13, 15, 41, 44, 13, 15);
            _testChoisi = 5;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(16, 18, 45, 47, 16, 19);
            _testChoisi = 6;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(19, 22, 39, 41, 20, 22);
            _testChoisi = 7;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(23, 24, 32, 34, 3, 7);
            _testChoisi = 8;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(14, 17, 27, 30, 13, 14);
            _testChoisi = 9;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Page1Tests(8, 11, 42, 44, 18, 20);
            _testChoisi = 10;
        }
        
        private void choix_Test(object sender, RoutedEventArgs e)
        {
            if (getnote(1, 1) > 6)
                b2.Background = Brushes.GreenYellow;
            else if (getnote(1, 1) == 6)
                b2.Background = Brushes.Orange;
            else if (getnote(1, 1) == -1)
                b2.Background = Brushes.BlueViolet;
            else if (getnote(1, 1) <6)
                b2.Background = Brushes.Red;

            if (getnote(1, 2) > 6)
                b2.Background = Brushes.GreenYellow;
            else if (getnote(1, 2) == 6)
                b2.Background = Brushes.Orange;
            else if (getnote(1, 2) == -1)
                b2.Background = Brushes.BlueViolet;
            else if (getnote(1, 2) < 6)
                b2.Background = Brushes.Red;


            if (getnote(1, 3) > 6)
                b3.Background = Brushes.GreenYellow;
            else if (getnote(1, 3) == 6)
                b3.Background = Brushes.Orange;
            else if (getnote(1, 3) == -1)
                b3.Background = Brushes.BlueViolet;
            else if (getnote(1, 3) < 6)
                b3.Background = Brushes.Red;


            if (getnote(1, 1) > 6)
                b1.Background = Brushes.GreenYellow;
            else if (getnote(1, 1) == 6)
                b1.Background = Brushes.Orange;
            else if (getnote(1, 1) == -1)
                b1.Background = Brushes.BlueViolet;
            else if (getnote(1, 1) < 6)
                b1.Background = Brushes.Red;


            if (getnote(1, 4) > 6)
                b4.Background = Brushes.GreenYellow;
            else if (getnote(1, 4) == 6)
                b4.Background = Brushes.Orange;
            else if (getnote(1, 4) == -1)
                b4.Background = Brushes.BlueViolet;
            else if (getnote(1, 4) < 6)
                b4.Background = Brushes.Red;


            if (getnote(1, 5) > 6)
                b5.Background = Brushes.GreenYellow;
            else if (getnote(1, 5) == 6)
                b5.Background = Brushes.Orange;
            else if (getnote(1, 5) == -1)
                b5.Background = Brushes.BlueViolet;
            else if (getnote(1, 5) < 6)
                b5.Background = Brushes.Red;


            if (getnote(1, 6) > 6)
                b6.Background = Brushes.GreenYellow;
            else if (getnote(1, 6) == 6)
                b6.Background = Brushes.Orange;
            else if (getnote(1, 6) == -1)
                b6.Background = Brushes.BlueViolet;
            else if (getnote(1, 6) < 6)
                b6.Background = Brushes.Red;


            if (getnote(1, 7) > 6)
                b7.Background = Brushes.GreenYellow;
            else if (getnote(1, 7) == 6)
                b7.Background = Brushes.Orange;
            else if (getnote(1, 7) == -1)
                b7.Background = Brushes.BlueViolet;
            else if (getnote(1, 7) < 6)
                b7.Background = Brushes.Red;


            if (getnote(1, 8) > 6)
                b8.Background = Brushes.GreenYellow;
            else if (getnote(1, 8) == 6)
                b8.Background = Brushes.Orange;
            else if (getnote(1, 8) == -1)
                b8.Background = Brushes.BlueViolet;
            else if (getnote(1, 8) < 6)
                b8.Background = Brushes.Red;


            if (getnote(1, 9) > 6)
                b9.Background = Brushes.GreenYellow;
            else if (getnote(1, 9) == 6)
                b9.Background = Brushes.Orange;
            else if (getnote(1, 9) == -1)
                b9.Background = Brushes.BlueViolet;
            else if (getnote(1, 9) < 6)
                b9.Background = Brushes.Red;


            if (getnote(1, 10) > 6)
                b10.Background = Brushes.GreenYellow;
            else if (getnote(1, 10) == 6)
                b10.Background = Brushes.Orange;
            else if (getnote(1, 10) == -1)
                b10.Background = Brushes.BlueViolet;
            else if (getnote(1, 10) < 6)
                b10.Background = Brushes.Red;

            if (testActuel == 1)
            {
                b2.IsEnabled = false;
                b3.IsEnabled = false;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
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
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(2);
            }
            if (testActuel == 3)
            {
                b3.IsEnabled = true;
                b4.IsEnabled = false;
                b5.IsEnabled = false;
                b6.IsEnabled = false;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(3);
            }
            if (testActuel == 4)
            {
                b4.IsEnabled = true;
                b5.IsEnabled = false;
                b6.IsEnabled = false;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(4);
            }
            if (testActuel == 5)
            {
                b5.IsEnabled = true;
                b6.IsEnabled = false;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(5);
            }
            if (testActuel == 6)
            {
                b6.IsEnabled = true;
                b7.IsEnabled = false;
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(6);
            }
            if (testActuel == 7)
            {
                b7.IsEnabled = true;
                b8.IsEnabled = false;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(7);
            }
            if (testActuel == 8)
            {
                b8.IsEnabled = true;
                b9.IsEnabled = false;
                b10.IsEnabled = false;
                saveLastTest(8);
            }
            if (testActuel == 9)
            {
                b9.IsEnabled = true;
                b10.IsEnabled = false;
                saveLastTest(9);
            }
            if (testActuel == 10)
            {
                b10.IsEnabled = true; 
                saveLastTest(10);
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

            if(connectToUtilisateur.State == ConnectionState.Closed)
                connectToUtilisateur.Open();

            try
            {
                cmd = new SqlCommand(query, connectToUtilisateur); 

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(savedData);
                da.Dispose();

                if (savedData.Rows[0]["Test1"].ToString().Equals(""))
                    testActuel = 1;
                else
                   testActuel = Int32.Parse(savedData.Rows[0]["Test1"].ToString());

                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();
            }
            catch (Exception)
            {
                if (connectToUtilisateur.State == ConnectionState.Open)
                    connectToUtilisateur.Close();

                MessageBox.Show("error Get LastTest Testniveau 1 ");
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
            string query = "UPDATE Utilisateur SET Test1='" + testCode.ToString() + "' WHERE UtilisateurID = '" + LogIN.LoggedUser.UtilisateurID + "'";

            SqlConnection connecttoUsers = new SqlConnection(connString);

            if (connecttoUsers.State == ConnectionState.Closed)
                connecttoUsers.Open();

            try
            {
                SqlCommand cmd = new SqlCommand(query, connecttoUsers);

                cmd = new SqlCommand(query, connecttoUsers);

                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if(connecttoUsers.State == ConnectionState.Open)
                    connecttoUsers.Close();
            }
            catch (Exception)
            {
                if (connecttoUsers.State == ConnectionState.Open)
                    connecttoUsers.Close();

                MessageBox.Show("error save last test Testniveau 1 ");
            }
        }
        private int getnote(int niveau , int test)
        {
            // Code == ID //
            string connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            DataTable savedData = new DataTable();
            SqlConnection connectToUtilisateur = new SqlConnection(connString);

            SqlCommand cmd;

            string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "NoteTest WHERE Niveau = '" + niveau.ToString() + "' AND Test = '" + test.ToString() +"'";

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

                MessageBox.Show("error Get note Testniveau 1 ");
                return -1;
            }
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Niv1Main();
        }

        private void b1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;
            button.BorderThickness = new Thickness(8);
            button.BorderBrush = Brushes.Azure;
            if (getnote(1, ((int)button.Tag)) == -1)
            {
                if (MainWindow.langue == 0)
                {
                    noteShow.Content = "Vous n'avez pas passé ce test !";
                }
                else
                {
                    noteShow.Content = "انت لم تجتز هدا الامتحان";
                }
            }
            else
            {
                if (MainWindow.langue == 0)
                {
                    noteShow.Content = "Test Note : " + getnote(1, ((int)button.Tag)).ToString();
                }
                else
                {
                    noteShow.Content = "نقطة الامتحان " + getnote(1, ((int)button.Tag)).ToString();
                }
            }
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
