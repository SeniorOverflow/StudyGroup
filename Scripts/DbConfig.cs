using System;
using System.Collections.Generic;
using Npgsql;
namespace  StudyGroup.Script
{
     class   DbConfig
    {
        static int idOp ;
        static NpgsqlConnection conn;
        public static void SetStringConnection(string unSaveStringConnection)
        {
            conn = new NpgsqlConnection(unSaveStringConnection);
        }
        public static void OpenConnection()
        {
            conn.Open();
        }
        public IEnumerable<List<string>> GetSqlQuaryData(string _sqlCommand)  // here need use yeld return 
        {
            int thisId= idOp;
            idOp++;
            var sqlData = new List<List<string>>();
            var row = new List<string>();
            var command = new NpgsqlCommand(_sqlCommand, conn);
            Console.WriteLine("Open " + thisId + " - "  + _sqlCommand);
            var dr = command.ExecuteReader();

            while(dr.Read())
            {
                var count = dr.FieldCount;
                row = new List<string>();
                for(int i =0 ; i< count ;i++)
                    row.Add(dr[i].ToString());
                yield return row;
            }
            Console.WriteLine("Close " + thisId);
            dr.Close();
        }
       

        public void UseSqlQuary(string _sqlCommand) {
           var command =  new NpgsqlCommand(_sqlCommand, conn);
           var dr  = command.ExecuteReader();
           dr.Close();
        }
        
        static void  CloseConnection()
        {
            conn.Close();
        }
    }
}