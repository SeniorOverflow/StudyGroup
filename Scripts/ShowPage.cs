using System;
using System.Collections.Generic;
using System.Linq;
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
            var tmp_data = 
            db.GetSqlQuaryData("SELECT count(id) from  " + name_table).First();
           
            if(tmp_data.Count > 0)
            {
                edit.count_pages = Convert.ToInt32(tmp_data[0]) / count_items_on_page;
                int items = Convert.ToInt32(tmp_data[0]);
                if((items % count_items_on_page != 0)&&(items > count_items_on_page))
                {
                   edit.count_pages++;
                }
            }
            
            foreach (var item in db.GetSqlQuaryData(sql_com)) //XXX
                edit.list.Add(item);

            edit.currect_number = numb_page;
            return edit;
        }
        
    }
}