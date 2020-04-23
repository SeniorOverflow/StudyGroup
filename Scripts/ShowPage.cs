using System;
using System.Collections.Generic;
using Npgsql;
using StudyGroup.Models;
namespace  StudyGroup.Script
{
    static class   ShowPage
    {
        
        public static Edit TakePages(string name_table,string sql_com,int numb_page,int count_items_on_page )
        {
            var db = new DbConfig();
            var edit = new Edit();
            var tmp_data = new List<List<string>>();
            tmp_data = 
            db.GetSqlQuaryData("SELECT count(id) from  " + name_table);
           
            if(tmp_data.Count > 0)
            {
                edit.count_pages = Convert.ToInt32(tmp_data[0][0]) / count_items_on_page;
                int items = Convert.ToInt32(tmp_data[0][0]);
                if((items % count_items_on_page != 0)&&(items > count_items_on_page))
                {
                   edit.count_pages++;
                }
            }
            edit.list = 
            db.GetSqlQuaryData(sql_com);
            edit.currect_number = numb_page;
            
            return edit;
        }
        
    }
}