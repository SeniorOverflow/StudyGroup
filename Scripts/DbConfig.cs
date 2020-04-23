using System;
using System.Collections.Generic;
using Npgsql;
namespace  StudyGroup.Script
{
     class   DbConfig
    {
        static NpgsqlConnection conn;
        public static void SetStringConnection(string unSaveStringConnection)
        {
            conn = new NpgsqlConnection(unSaveStringConnection);
        }
        public static void OpenConnection()
        {
            conn.Open();
        }
        public List<List<string>> GetSqlQuaryData(string _sqlCommand) 
        {
            var sqlData = new List<List<string>>();
            var row = new List<string>();
            var command = new NpgsqlCommand(_sqlCommand, conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            while(dr.Read())
            {
                var count = dr.FieldCount;
                row = new List<string>();
                for(int i =0 ; i< count ;i++)
                    row.Add(dr[i].ToString());
                sqlData.Add(row);
            }
            dr.Close();
            return sqlData;
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