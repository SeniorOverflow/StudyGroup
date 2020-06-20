using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using StudyGroup.Script;

namespace StudyGroup.Models
{
    public class GroupModel
    {
      public  int idGroup ;
      public  string title ;
      public  string description ;
      public  string picture ;

      public GroupModel(string GUID =  null, string typePic = null)
      {
          picture = GUID + "." + typePic;
      }
      
    }
}