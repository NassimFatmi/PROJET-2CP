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

namespace PROJET_2CP.Pages
{
    /// <summary>
    /// Logique d'interaction pour Bilan.xaml
    /// </summary>
    public partial class Bilan : Page
    {
        public Bilan( int bonne,int mauvaise)
        {
            InitializeComponent();
           // Lbl1.Content = "Bonnes reponses : " + (bonne + TestNiveau1.nbBonneReponse).ToString();
            //lbl2.Content = "Mauvaises reponses : " +( mauvaise + 5-TestNiveau1.nbBonneReponse).ToString();
            //saveNote((bonne + TestNiveau1.nbBonneReponse),1,Tests1._testChoisi,"Test 1");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Home.mainFrame.Content = new Tests1();
            Tests1.testActuel++;
        }
        /// <summary>
        /// partie pour le sauvegrade des reponses pour construire les statistiques 
        /// </summary>
        /// <param name="reponse"></param> la reponse de l'apprenant
        /// <param name="niveau"></param> niveau de la qst
        /// <param name="code"></param> Numero de test
        /// <param name="theme"></param> le theme que la qst apartient
        private void saveNote(int note, int niveau, int code, string theme)
        {
            // Code == ID //
            string connString = $@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = {System.IO.Directory.GetCurrentDirectory()}\Trace\Save.mdf; Integrated Security = True";

            DataTable savedData = new DataTable();

            string query = "SELECT * FROM " + LogIN.LoggedUser.UtilisateurID + "NoteTest WHERE niveau = '" + niveau.ToString() + "' AND Test = '" + code.ToString() + "'";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(savedData);
                da.Dispose();

                if (savedData.Rows.Count == 1)
                {
                    // Si l'apprenant a répondu a cette question on fait la maj dans sa Table dans Save BDD
                    query = "UPDATE " + LogIN.LoggedUser.UtilisateurID + "NoteTest SET Note='" + note + "' WHERE niveau = '" + niveau.ToString() + "' AND Test = '" + code.ToString() + "'";
                    cmd = new SqlCommand(query, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                else
                {

                    //Si l'apprenant n'a pas répondu a cette question on l'insert sa réponse
                    query = "INSERT INTO " + LogIN.LoggedUser.UtilisateurID + "NoteTest(Niveau,Theme,Test,Note) VALUES('" + niveau.ToString() + "','" + theme + "','" + code.ToString() + "','" + note + "')";
                    cmd = new SqlCommand(query, conn);

                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                MessageBox.Show("error save Db bilan Testniveau 1 ");
            }
        }
    }
}
