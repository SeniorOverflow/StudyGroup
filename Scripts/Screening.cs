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
            var GUIDCode = new Guid();
            this.scr = "$a" + GUIDCode + "a$";
        }

        public string GetScr() => this.scr;
        public Screening() =>  SetScr();
    }
}