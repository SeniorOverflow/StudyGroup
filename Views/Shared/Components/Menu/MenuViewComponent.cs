using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using StudyGroup.Models;
using StudyGroup.Script;

namespace  StudyGroup.Views.Components.Menu
{
    public class MenuViewComponent: ViewComponent
    {
        static private List<List<string>> userMenu = new List<List<string>>()
        {
            
            new List<string> {"Sing In","User" , "Authorization"},
            new List<string> {"Sing Up","User" ,  "Reg"},
            new List<string> {"", "User" ,  "Profile"},
            new List<string> {"Sign out","User" ,  "LogOut"},
            new List<string> {"Homeworks", "User" , "YourHomeworks"},
            new List<string> {"Groups", "User" ,     "Groups"},
            new List<string> {"Assessents", "User" ,        "Assessents"},
            new List<string> {"Timetable of classes", "User" , "Timetable"}
        };
        public  IViewComponentResult Invoke()
        {
            var sr = new Screening();
            var db = new DbConfig();
            var menu = new List<List<string>>();
            var login = HttpContext.Session.GetString("login");
            var isAuthorizationUser = false;
            if (string.IsNullOrEmpty(login))
            {
                menu.Add(userMenu[0]);
                menu.Add(userMenu[1]);   
            }
            else
            {
                var userProfile = userMenu[2];
                userProfile[0] = "" + login;
                isAuthorizationUser = true;
                menu.Add(userProfile);
                menu.Add(userMenu[3]);
                menu.Add(userMenu[4]);
                menu.Add(userMenu[5]);
                menu.Add(userMenu[6]);
            }
            ViewBag.isAuthorizationUser = isAuthorizationUser;
            ViewBag.Menu = menu;
            return View();
        }
    }
}