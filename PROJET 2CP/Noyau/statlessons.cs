using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJET_2CP.Noyau
{
    class statLesson
    {
        public string Lesson { get; set; }
        public bool IsGoodAnswer { get; set; }

        public statLesson(string a, bool b)
        {
            Lesson = a;
            IsGoodAnswer = b;
        }

    }
}
