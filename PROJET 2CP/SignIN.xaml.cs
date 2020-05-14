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
using PROJET_2CP.Classes;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace PROJET_2CP
{
    /// <summary>
    /// Interaction logic for SignIN.xaml
    /// </summary>
    public partial class SignIN : Page
    {
        private DBAccess _connectBDD;
        public static int _Commencer;
        private string _connString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\UtilisateurBDD.mdf;Integrated Security = True";
        public SignIN()
        {
            InitializeComponent();
            _Commencer = 0;
            logoimage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/EDautoEcole.png", UriKind.RelativeOrAbsolute));
            franceicon.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Francais.png", UriKind.RelativeOrAbsolute));
            arabicon.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/Arabic.png", UriKind.RelativeOrAbsolute));
            initialiserLangue();
            _connectBDD = new DBAccess(_connString);
        }

        private void Creecpt_Click(object sender, RoutedEventArgs e)
        {
            string insert_query = "INSERT INTO Utilisateur(UtilisateurID,Nom,Prenom,Password,Image) VALUES(@userID,@nom,@prenom,@password,@Image)";

            if (userIDtxt.Text.Equals(""))
            {
                if(MainWindow.langue == 0)
                {
                    errLbl.Content = "Utilisateur id est obligatoire !";
                }
                else
                {
                    errLbl.Content = "اسم المستخدم اجباري";
                }
                return;
            }
            else if (nomtxt.Text.Equals(""))
            {
                if (MainWindow.langue == 0)
                {
                    errLbl.Content = "le Nom est obligatoire !";
                }
                else
                {
                    errLbl.Content = "اللقب اجباري";
                }
                return;

            } else if (prenomtxt.Text.Equals(""))
            {
                if (MainWindow.langue == 0)
                {
                    errLbl.Content = "le Prénom est obligatoire !";
                }
                else
                {
                    errLbl.Content = "الاسم اجباري";
                }
                return;
            }

            if (!passwordtxt.Password.Equals(""))
            {
                if(!passwordtxt.Password.Equals(confirmepasswordtxt.Password))
                {
                    if (MainWindow.langue == 0)
                    {
                        errLbl.Content = "Vérifier votre mot de passe !";
                    }
                    else
                    {
                        errLbl.Content = "تأكد من كلمة المرور";
                    }
                    return;
                }
            }

            Apprenant apprenant = new Apprenant(1, userIDtxt.Text, nomtxt.Text,prenomtxt.Text,passwordtxt.Password,imagepathtxt.Text);

            DataTable selectTable = new DataTable();

            try
            {
                string search_query = "SELECT * FROM Utilisateur WHERE ( UtilisateurID = '"+ apprenant.UtilisateurID +"')";
                _connectBDD.readDatathroughAdapter(search_query,selectTable);

                if (selectTable.Rows.Count == 1 )
                {
                    MessageBox.Show("Utilisateur est déja exister !");                    
                }
                else
                {
                    SqlCommand insert_Command = new SqlCommand(insert_query);

                    insert_Command.Parameters.AddWithValue("@userID", apprenant.UtilisateurID);
                    insert_Command.Parameters.AddWithValue("@nom", apprenant.Nom);
                    insert_Command.Parameters.AddWithValue("@prenom", apprenant.Prenom);
                    insert_Command.Parameters.AddWithValue("@Image",apprenant.Image);
                    insert_Command.Parameters.AddWithValue("@password", apprenant.Password);

                    _connectBDD.createConn();
                    int row = _connectBDD.executeQuery(insert_Command);
                    _connectBDD.closeConn();

                    int tblCreat = CreatTraceTable(apprenant.UtilisateurID);

                    if (row == 1 && tblCreat == 1)
                    {
                        LogIN.LoggedUser = apprenant;
                        LogIN.LoggedUser.Theme = true;
                        _Commencer = 1;
                        MainWindow.quizFrame.Content = new Home();
                        Home.getinstanceTuto().Visibility = Visibility.Visible;
                    }
                }
            }
            catch(Exception)
            {
                _connectBDD.closeConn();
                MessageBox.Show("Erreur lors l'acces a la BDD ! ");
            }
        }

        private void Browse_Image( object sender , RoutedEventArgs e )
        {
            OpenFileDialog parcourire = new OpenFileDialog();
            parcourire.Filter = "Image Files(*.jpg;*.jpeg)|*.jpg;*.jpeg|PNG Files(*png)|*.png";
            parcourire.Multiselect = false;
            parcourire.DefaultExt = ".jpg";

            if (parcourire.ShowDialog() == true)
            {
                imagepathtxt.Text = parcourire.FileName.ToString();
            }
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            MainWindow.quizFrame.Content = new LogIN();   
        }

        //Création d'une méthode qui construire une table dans la BDD pour sauvegarder la trace de l'apprenant
        private int CreatTraceTable(string userID)
        {
            //Si retourne 1 creation Réussi sinon 0 

            string saveConnection = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf;Integrated Security=True";
            string creatTableQuery = "CREATE TABLE "+userID+"Trace(" +
                                     "[Id]    INT NOT NULL PRIMARY KEY IDENTITY," +
                                     "[Niveau]    INT  NULL," +
                                     "[Test]    INT  NULL," +
                                     "[Code]    INT  NULL," +                // ID 
                                     "[ReponseText]   NVARCHAR(MAX) NULL," + // Answer content
                                     "[ReponseTextAr]   NVARCHAR(MAX) NULL," + // Answer content
                                     "[Reponse] bit NULL)";                  // V.V de la réponse 

            string testNoteCreatTableQuery = "CREATE TABLE " + userID + "NoteTest(" +
                                     "[Id]    INT NOT NULL PRIMARY KEY IDENTITY," +
                                     "[Niveau]    INT  NULL," +
                                     "[Theme]   VARCHAR(MAX) NULL," +
                                     "[Test]    INT  NULL," +
                                     "[Note]    INT  NULL,)";

            SqlConnection connection = new SqlConnection(saveConnection);
            
            if(connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand creatCMD = new SqlCommand(creatTableQuery, connection);
            SqlCommand CreatCMDTestNote = new SqlCommand(testNoteCreatTableQuery,connection);
            try
            {
                creatCMD.ExecuteNonQuery();
                CreatCMDTestNote.ExecuteNonQuery();
                connection.Close();
                return 1;
            }
            catch (Exception)
            {
                connection.Close();
                MessageBox.Show("Error has accured ! ");
                return 0;
            }
        }
        private void initialiserLangue()
        {
            if (MainWindow.langue == 0)
            {   //la langue français
                loginbtn.Content = "Se connecter";
                creercmpttxt.Content = "Créer comtpe";
                MaterialDesignThemes.Wpf.HintAssist.SetHint(userIDtxt, "Utilisateur ID");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(nomtxt, "Nom");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(prenomtxt, "Prénom");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordtxt, "Mot de passe");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(confirmepasswordtxt, "Confirmer le mot de passe");
                imagebtn.Text = "Image";
                creercompteconfirme.Content = "Créer comtpe";
                proverbe1.Content = "Respecte le code de la route";
                proverbe2.Content = "et le code de la route te respectera.";
                pub1.Content = "Créer votre compte gratuitement et obtenir";
                pub2.Content = "toutes les fonctionalités";
            }
            else
            {
                //la langue arabe
                loginbtn.Content = "تسجيل الدخول";
                creercmpttxt.Content = "انشئ حساب";
                MaterialDesignThemes.Wpf.HintAssist.SetHint(userIDtxt, "اسم الحساب");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(nomtxt, "اللقب");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(prenomtxt, "الاسم");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(passwordtxt, "كلمة المرور");
                MaterialDesignThemes.Wpf.HintAssist.SetHint(confirmepasswordtxt, "تأكيد كلمة المرور");
                imagebtn.Text = "صورة";
                creercompteconfirme.Content = "انشئ الحساب";
                proverbe1.Content = "احترم قانون المرور";
                proverbe2.Content = "و هو بدلك سيحترمك ";
                pub1.Content = "افنح حسابك مجانا";
                pub2.Content = "واحصل على جميع الميزات";
            }
        }
        private void langueCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (langueCmb.SelectedIndex == 0 && MainWindow.langue == 1)
            {
                MainWindow.langue = 0;
                MainWindow.quizFrame.Content = new SignIN();
            }
            else if (langueCmb.SelectedIndex == 1 && MainWindow.langue == 0)
            {
                MainWindow.langue = 1;
                MainWindow.quizFrame.Content = new SignIN();
            }
        }
    }
}
