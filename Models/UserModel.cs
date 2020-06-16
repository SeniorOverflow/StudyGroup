using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using StudyGroup.Script;

namespace StudyGroup.Models
{
    public class UserModel
    {
      public  int id_user ;
      public  string login ;
      public  string first_name ;
      public  string second_name ;

      public string picture_profile;
      

      public int type_language;
    

      static  public int GetId(string login ) 
        {
            var db = new DbConfig();
            var sr  = new Screening();
  
            var sqlQuary = "select id from users where login = " +sr.GetScr()+login+sr.GetScr();
            var idUser = -1;
            foreach(var userData in db.GetSqlQuaryData(sqlQuary))
                idUser = Convert.ToInt32(userData[0]);
            return idUser;
        }
    }
}