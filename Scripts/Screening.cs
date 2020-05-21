using System;
using System.Collections.Generic;
using Npgsql;
namespace  StudyGroup.Script
{
     class   Screening
    {
        private string scr ;

        void SetScr()
        {
            Random rnd = new Random();
            var randomValue = rnd.Next()+ 99999;
            this.scr = "$a" + randomValue + "a$";
        }

        public string GetScr() => this.scr;
        public Screening() =>  SetScr();
    }
}