using System;
using Npgsql;
using StudyGroup.Script;
namespace StudyGroup.Models
{
    public class Homework
    {
        private int id ;
        public  string title;
        public  string dateEnd;
        public  int assessment;

        public Homework(int idHomework)
        {
            this.id = idHomework;
        }
        public int GetHomeworkId => id;
    }
}