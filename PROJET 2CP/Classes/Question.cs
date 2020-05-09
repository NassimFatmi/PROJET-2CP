using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;

namespace PROJET_2CP.Classes
{
    class Question
    {
        private int _Test;
        private int _Code;
        private string _content;
        private string _explication;
        private string _Reponse;
        private ArrayList _propositions;

        public Question( int test,int code , string explication , string reponse , ArrayList propositions )
        {
            this._Test = test;
            this._Code = code;
            this._explication = explication;
            this._Reponse = reponse;
            this._propositions = propositions;
        }

        public int Test { get => _Test; set => _Test = value; }
        public int Code { get => _Code; set => _Code = value; }
        public string Explication { get => _explication; set => _explication = value; }
        public string Content { get => _content; set => _content = value; }
        public string Reponse { get => _Reponse; set => _Reponse = value; }
        public ArrayList Propositions { get => _propositions; set => _propositions = value; }

        public void afficher()
        {
            MessageBox.Show(_Test.ToString()+"_"+_Code.ToString()+".png\n"+_Reponse+"\n"+_explication+"");
        }
    }
}
