using System;
using System.Collections.Generic;

namespace StudyGroup.Models
{
    public class User
    {
      public  int id_user ;
      public  string login ;
      public  string first_name ;
      public  string second_name ;
      public bool is_dev;
      public string company;
      public  int lvl;
      public  double score ;
      public  double bonus_score ;
      public string picture_profile;

      public int id_dev;
      public string name_of_company;
      
      public  List<int> types_user  = new List<int>();

      public int quantity_cart_places ;

      public int type_language;

       
    }

}