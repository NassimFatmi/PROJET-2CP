using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace WpfApp1.Classes
{
    class TestFinal
    {
        private int _numeroTest;
        private string _intersectionBDD; 
        private DBAccess _BDDconnect;

        private ArrayList _ListeQuestions;

        public TestFinal(int nmr)
        {
            _numeroTest = nmr;
            _intersectionBDD = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC\source\repos\WpfApp1\WpfApp1\IntersectionsBDD.mdf;Integrated Security=True";
            _BDDconnect = new DBAccess(_intersectionBDD);
            _ListeQuestions = new ArrayList();
            
            GetQuests(nmr);
        }
        public ArrayList ListeQuestions { get => _ListeQuestions; set => _ListeQuestions = value; }
        public int NumeroTest { get => _numeroTest; set => _numeroTest = value; }

        // numero de test dans la base des données
        public void GetQuests( int numeroTest )
        {
            string selectionQuery = "SELECT * FROM IntersectionsQst WHERE( Test=" + numeroTest.ToString() + ")";
            DataTable QuestionsTable = new DataTable() ;

            try
            {
                _BDDconnect.readDatathroughAdapter(selectionQuery, QuestionsTable);

                // copie les resultats dans les attributs
                Question qst;
                ArrayList qstProp;

                for (int i = 0; i < 8; i++)
                {
                    qstProp = new ArrayList();

                    if (!QuestionsTable.Rows[i]["proposition1"].ToString().Equals(""))
                    {
                        qstProp.Add(QuestionsTable.Rows[i]["proposition1"].ToString());
                    }
                    if (!QuestionsTable.Rows[i]["proposition2"].ToString().Equals(""))
                    {
                        qstProp.Add(QuestionsTable.Rows[i]["proposition2"].ToString());
                    }
                    if (!QuestionsTable.Rows[i]["proposition3"].ToString().Equals(""))
                    {
                        qstProp.Add(QuestionsTable.Rows[i]["proposition3"].ToString());
                    }
                    if (!QuestionsTable.Rows[i]["proposition4"].ToString().Equals(""))
                    {
                        qstProp.Add(QuestionsTable.Rows[i]["proposition4"].ToString());
                    }
                    if (!QuestionsTable.Rows[i]["proposition5"].ToString().Equals(""))
                    {
                        qstProp.Add(QuestionsTable.Rows[i]["proposition5"].ToString());
                    }

                    qst = new Question(Int32.Parse(QuestionsTable.Rows[i]["Test"].ToString()), Int32.Parse(QuestionsTable.Rows[i]["Code"].ToString()), QuestionsTable.Rows[i]["Explication"].ToString(), QuestionsTable.Rows[i]["Reponse"].ToString(), qstProp);
                    _ListeQuestions.Add(qst);
               
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Erreur lors de l'access a la base des données testfinal");
            }
        }
    }
}
