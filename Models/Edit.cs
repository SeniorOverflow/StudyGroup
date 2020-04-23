using System;
using System.Collections.Generic;

namespace StudyGroup.Models
{
    public class Edit
    {
      public List<List<string>> list = new List<List<string>>();
      public int currect_number;
      public int count_pages;

      public  Edit()
      {
        this.list.Clear();
        this.currect_number = 0;
        this.count_pages = 0;
      }
    }
}