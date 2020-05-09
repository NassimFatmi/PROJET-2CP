using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_2CP.Classes
{
    class Apprenant
    {
        private int _ID;
        private string _UtilisateurID;
        private string _Nom;
        private string _Prenom;
        private string _Password;
        private string _Image;
        private bool _theme;

        public Apprenant(int iD, string utilisateurID, string nom, string prenom , string password , string image )
        {
            _ID = iD;
            _UtilisateurID = utilisateurID;
            _Nom = nom;
            _Prenom = prenom;
            _Password = password;
            _Image = image;
        }

        public int ID { get => _ID; set => _ID = value; }
        public string UtilisateurID { get => _UtilisateurID; set => _UtilisateurID = value; }
        public string Nom { get => _Nom; set => _Nom = value; }
        public string Prenom { get => _Prenom; set => _Prenom = value; }
        public string Password { get => _Password; set => _Password = value; }
        public string Image { get => _Image; set => _Image = value; }
        public bool Theme { get => _theme; set => _theme = value; }
    }
}
