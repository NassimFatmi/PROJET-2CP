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
        private int _LevelsPoints = 60;

        public Niveaux()
        {
            InitializeComponent();
            lvl1icon.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Lvl1icon.png", UriKind.RelativeOrAbsolute));
            lvl2icon.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Lvl2icon.png", UriKind.RelativeOrAbsolute));
            lvl3icon.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Lvl3icon.png", UriKind.RelativeOrAbsolute));
            if (!LogIN.LoggedUser.UtilisateurID.Equals("Guest"))
                progressLevelsFinal();
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
            Home.mainFrame.Content = new Niveau3.Niv3Main();
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

        private void mouseEnterLockedLevel2(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 340;
            button.Width = 240;
            if (MainWindow.langue == 0)
                lockmessage.Content = "Pour débloquer ce niveau il faut obtenir 60 points ";
            else
                lockmessage.Content = "يجب عليك تجميع 60 نقطة لفتح هدا المستوى";
        }

        private void mouseLeaveLocked(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 300;
            button.Width = 200;
            lockmessage.Content = "";
        }
        private void mouseEnterLockedLevel3(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Height = 340;
            button.Width = 240;
            if (MainWindow.langue == 0)
                lockmessage.Content = "Pour débloquer ce niveau il faut obtenir 90 points ";
            else
                lockmessage.Content = "يجب عليك تجميع 90 نقطة لفتح هدا المستوى";
        }
       
        private void progressLevelsFinal()
        {
            string connStringToSave = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ={ System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf; Integrated Security = True";
            SqlConnection saveConn = new SqlConnection(connStringToSave);
            SqlCommand cmd;
            SqlDataAdapter adapter;
            DataTable temp = new DataTable();
            string querySelect = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID.ToString() + "Trace WHERE ( Test = '0' AND Reponse = 'True')";

            try
            {
                if (saveConn.State == ConnectionState.Closed)
                    saveConn.Open();

                cmd = new SqlCommand(querySelect, saveConn);
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(temp);
                adapter.Dispose();

                 _LevelsPoints = temp.Rows.Count;

                if (saveConn.State == ConnectionState.Open)
                    saveConn.Close();
            }
            catch (Exception)
            {
                if (saveConn.State == ConnectionState.Open)
                    saveConn.Close();
                MessageBox.Show("Error Niveaux progressFinal");
            }

            pointsLbl.Content = _LevelsPoints.ToString();
            if (MainWindow.langue == 0)
                pointsMsg.Content = "Vos points";
            else
                pointsMsg.Content = "نقاطك";

            if(_LevelsPoints < 60)
            {
                lvl2lock.Visibility = Visibility.Visible;
                lvl3lock.Visibility = Visibility.Visible;
                niv2.MouseEnter += new MouseEventHandler(this.mouseEnterLockedLevel2);
                niv2.MouseLeave += new MouseEventHandler(this.mouseLeaveLocked);
                niv3.MouseEnter += new MouseEventHandler(this.mouseEnterLockedLevel3);
                niv3.MouseLeave += new MouseEventHandler(this.mouseLeaveLocked);
            }
            if(_LevelsPoints >= 60)
            {
                lvl2lock.Visibility = Visibility.Collapsed;
                niv2.Click += new RoutedEventHandler(this.niv2_Click);
                niv2.MouseEnter += new MouseEventHandler(this.mouseEnter);
                niv2.MouseLeave += new MouseEventHandler(this.mouseLeave);
                niv3.MouseEnter += new MouseEventHandler(this.mouseEnterLockedLevel3);
                niv3.MouseLeave += new MouseEventHandler(this.mouseLeaveLocked);
            }
            if(_LevelsPoints >= 90)
            {
                lvl3lock.Visibility = Visibility.Collapsed;
                niv3.Click += new RoutedEventHandler(this.niv3_Click);
                niv3.MouseEnter += new MouseEventHandler(this.mouseEnter);
                niv3.MouseLeave += new MouseEventHandler(this.mouseLeave);
            }
        }
    }
}
