using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
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

namespace PROJET_2CP.Pages
{
    /// <summary>

    /// </summary>

    public partial class Bilan3 : Page
    {
        private SoundPlayer soundPlayer;
        public Bilan3(int bonne, int mauvaise)
        {
            InitializeComponent();
            if(MainWindow.langue==0)
            {
                Lbl1.Content = "Bonnes reponses : " + (TestNiveau3.nbBonneReponse).ToString();
                lbl2.Content = "Mauvaises reponses : " + (6 - TestNiveau3.nbBonneReponse).ToString();
            }
            else
            {
                Lbl1.Content = (TestNiveau3.nbBonneReponse).ToString()+ ": الاجابات الصحيحة";
                lbl2.Content = (6 - TestNiveau3.nbBonneReponse).ToString()+ ": الاجابات الخاطئة";
                quitter.Content = "خروج";
                remarque.Content = "ملاحظات";
            }

            saveNote((TestNiveau3.nbBonneReponse), 3, Tests3._testChoisi, "Test 3");
            
            if (TestNiveau3.nbBonneReponse < 4)
            {
                try
                {
                    reactionBilan.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/bad.png", UriKind.RelativeOrAbsolute));
                    soundPlayer = new SoundPlayer(@"SoundsEffects\disappointment.wav");
                    soundPlayer.Play();
                }
                catch (Exception)
                {

                }
            }
            else
            {
                Random random = new Random();
                int rnd = random.Next(2);
                try
                {
                    if (rnd == 0)
                        reactionBilan.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/200iq.jpg", UriKind.RelativeOrAbsolute));
                    else
                        reactionBilan.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "/icons/brain.png", UriKind.RelativeOrAbsolute));

                    soundPlayer = new SoundPlayer(@"SoundsEffects\clap.wav");
                    soundPlayer.Play();
                }
                catch (Exception)
                {

                }
            }
            TestNiveau3.nbBonneReponse = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool incrementer = true;
            Home.mainFrame.Content = new Tests3();
            for (int i = 0; i < Tests3.testDejaPasse.Length; i++)
            {
                if (Tests3._testChoisi == Tests3.testDejaPasse[i])
                {
                    incrementer = false;
                }
            }

            if (incrementer)
            {
                Tests3.testActuel++;
            }
            try { soundPlayer.Stop(); } catch (Exception) { }
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
                MessageBox.Show("error save Db bilan Testniveau 3 ");
            }
        }
     
    }
}