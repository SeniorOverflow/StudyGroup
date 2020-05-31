using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyGroup.Models;
using StudyGroup.Script;

namespace StudyGroup.Controllers
{
    public class UserController : Controller
    {
        private int GetUserAssessment(int idUser, int idHomework)
        { 
            var db = new  DbConfig();
            var sr = new  Screening();
            var assessment = 0;
            var sqlQuary = "SELECT assessment " + 
	                        " FROM h_assessment "  +
	                        " WHERE id_user   =  " + sr.GetScr() + idUser + sr.GetScr() + 
                            " and  id_homework = " + sr.GetScr() + idHomework+ sr.GetScr();
            
            foreach(var item in db.GetSqlQuaryData(sqlQuary))
                assessment += Int32.Parse(item[0]);

            return assessment;   
        }
        private int GetUserId(string login = null) 
        {
            var db = new DbConfig();
            var sr  = new Screening();
            login = 
                string.IsNullOrEmpty(login)
                ? HttpContext.Session.GetString("login") 
                : login;
                
            var sqlQuary = "select id from users where login = " +sr.GetScr()+login+sr.GetScr();
            var idUser = -1;
            foreach(var userData in db.GetSqlQuaryData(sqlQuary))
                idUser = Convert.ToInt32(userData[0]);
            return idUser;
        }

        private bool IsRegisterEmail(string Email)
        {
            var db = new DbConfig();
            var sr  = new Screening();
            var sqlQuary = "select id from users where mail = " +sr.GetScr()+Email+sr.GetScr(); 
            var userData = db.GetSqlQuaryData(sqlQuary);
            if(userData.Count() > 0)
                return true;
            return false;
        }
      
         [HttpGet]
        public async Task<IActionResult> Authorization(int ex = 0)
        {
            var count = 0;
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("CountIn")))
                HttpContext.Session.SetString("CountIn","0");
            else
            {
                count = Convert.ToInt32( HttpContext.Session.GetString("CountIn"));
                count ++;
                HttpContext.Session.SetString("CountIn",""+count);
            }
            var data = new Authorization();
            data.ex = ex;
            data.count = count;
            var data_names_on_lg = Language_Settings.GetWords(1);
            ViewBag.Data_name = data_names_on_lg;
            ViewBag.AutoData = data;
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<IActionResult> LogOut() 
        {
            HttpContext.Session.Clear();
             return await Task.Run(() => RedirectToAction("Index", new RouteValueDictionary( 
                        new { controller = "Home", action = "Index"} )));
        }

        [HttpGet]
        public async Task<IActionResult> Reg(int type = 0)
        {
            var data_words = Language_Settings.GetWords(1);
            var _mail = "" + HttpContext.Session.GetString("reg_mail");
            var _log = "" + HttpContext.Session.GetString("reg_login");

            ViewBag.reg_login = _log;
            ViewBag.reg_mail  = _mail;
            ViewBag.Data_name = data_words;
            return await Task.Run(() => View(type));
        }

        [HttpGet]
        public async Task<IActionResult> Homeworks () // Можно переделать  через группу но и так работает
        {
            var db = new DbConfig();
            var userId = GetUserId(); 
            var sqlCommand = 
                "SELECT homeworks.id, title, deadline, homeworks.url_on_file, type , assessment " + 
	                " FROM homeworks inner join  h_assessment on homeworks.id = h_assessment.id_homework " + 
	            " WHERE id_user = " + userId;
            var homeworks  = new List<Homework>();

            foreach(var item in db.GetSqlQuaryData(sqlCommand))
            {
                var idHomework = Int32.Parse(item[0]);
                var homework = new Homework(idHomework);
                homework.title = item[1];
                homework.dateEnd =  item[2];

                homeworks.Add(homework);
            }

            foreach(var homework in homeworks)
            {
                var idHomework = homework.GetHomeworkId;
                homework.assessment = GetUserAssessment(userId, idHomework );
            }

            ViewBag.Homeworks = homeworks;
            return await Task.Run(() => View());
        }

        [HttpGet]
         public async Task<IActionResult> Groups ()
         {
            var db = new DbConfig();
            
            return await Task.Run(() => View());
         }
         public async Task<IActionResult> Assessents ()
         {

             return await Task.Run(() => View());
         }
         public async Task<IActionResult> Timetable ()
         {
             return await Task.Run(() => View());
         }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reg(string _mail ,string _login , string _password , string _password2)
        {
            var db = new DbConfig();
            HttpContext.Session.SetString("reg_mail","" + _mail );
            HttpContext.Session.SetString("reg_login","" + _login );
            if(_password != _password2 )
                return await Task.Run(() => RedirectToAction("Reg",new {type  = 1}));
            else
            {
                if( _password.Length > 8)
                {
                    var sr = new Screening();
                    var usersId = GetUserId(_login);
                    if(usersId == -1)
                    {
                        var ex = new Regex("^[0-9A-Za-z]{1}[0-9A-Za-z_-]*@{1}[A-Za-z]+[.][A-Za-z]+$");
                        if(ex.IsMatch(_mail))
                        {
                            if(IsRegisterEmail(_mail))
                                return await Task.Run(() => RedirectToAction("Reg",new {type  = 3}));
                           
                            db.UseSqlQuary("INSERT INTO users(mail,login,password) " +
                                                        " VALUES "+
                                                        "       (" +sr.GetScr()+_mail     +sr.GetScr()+
                                                        " ,lower(" +sr.GetScr()+_login    +sr.GetScr()+ 
                                                        "),crypt(" +sr.GetScr()+_password +sr.GetScr()+", gen_salt('bf', 8)))");

                            return await Task.Run(() => RedirectToAction("Authorization", new{ex  = 3}));
                        }
                        else
                            return await Task.Run(() => RedirectToAction("Reg",new {type  = 4}));
                    }
                    else
                    {
                        Console.WriteLine("2");
                        return await Task.Run(() => RedirectToAction("Reg",new {type  = 2}));
                    }
                }
                else
                    return await  Task.Run(() =>RedirectToAction("Reg",new {type  = 5}));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult LogIn(string login ="", string password ="") //Переделать название функции 
        {
            var db = new DbConfig();
            var sr = new Screening();
           
            
            if(string.IsNullOrEmpty(login)||string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "User", action = "Authorization", ex= 4} ));
            }
            else
            {
                var userId = 
                db.GetSqlQuaryData("select id from users where login =  lower(" +"'"+login+"'"+") "+ 
                                    "AND  password = crypt(" + "'" + password + "'" + ", password)");
                if(userId.Count() == 0)
                {
                    return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "User", action = "Authorization", ex= 1} ));
                }
                else
                {
                    login =  login.ToLower();
                    HttpContext.Session.SetString("login",""+login);
                    HttpContext.Session.SetString("password",""+ password);
                }
                
            }
           
            
            return RedirectToAction("Profile", new RouteValueDictionary( 
                new { controller = "User", action = "Profile"   } ));
            
        }

        public IActionResult Profile() // добавить возможность просмотра других пользователей
        {
            var sr  = new Screening();
            var db = new DbConfig();
            string login = HttpContext.Session.GetString("login");
            
            if(string.IsNullOrEmpty( login))
            {
                return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "User", action = "Authorization", ex= 0} ));
            }
            else
            {
                var id_user = GetUserId();
                var _user = new User();
                var tmp_login = new List<List<string>>();
                Console.WriteLine(GetUserId());
                var  sqlQuary = "SELECT id,login,first_name,second_name,picture_profile "+
                                        "FROM users WHERE users.id = "+id_user;
                var userData = new List<string>();
                foreach (var item in db.GetSqlQuaryData(sqlQuary))
                    userData = item;

                Console.WriteLine(" # " + userData[0] + " - "  + userData[1] + " - "  + userData[2] +" - "  + userData[3] + " - "  + userData[4] );
                
                if(userData.Count()>0)
                {
                        _user.id_user= Convert.ToInt32(userData[0]);
                        _user.login= Convert.ToString(userData[1]);

                        _user.first_name = userData[2];
                        _user.second_name= userData[3];
                        _user.picture_profile = userData[4];
                        ViewBag.User_data = _user;
                    return View();
                }
            }

            return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "User", action = "Authorization", ex= 0} ));
        }
  
        public IActionResult UpdateInfopPage()
        {
            var sr = new Screening();
            var db = new DbConfig();
            var login = HttpContext.Session.GetString("login");
            var usersData = 
            db.GetSqlQuaryData("SELECT first_name,second_name,picture_profile FROM users "+ 
                    " WHERE users.login = "+sr.GetScr()+login+sr.GetScr());
            var userData  = new List<string>();
            foreach (var item in usersData)
                userData = item;
                
            
            var user = new User();
            
            user.first_name = userData[0];
            user.second_name = userData[1];
            user.picture_profile = userData[2];
            ViewBag.User_data = user;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInfo(string first_name, string second_name,  IFormFile u_file = null)
        {
            string filePath = "";
            string file_name="";
            var sr = new Screening();
            var db = new DbConfig();
            
            var _login = HttpContext.Session.GetString("login") ;

            if(_login == "")
            {
                  return RedirectToAction("Authorization", new RouteValueDictionary( 
                                new { controller = "User ", action = "Profile"} ));

            }
            else
            {
                if(u_file != null)
                {
                    string [] type = u_file.FileName.Split('.');

                    type[1] = type[1].ToUpper();
                    if(( type[1] == "PNG")||( type[1] == "JPEG")||( type[1] == "JPG"))
                    {
                        string [] type_pic = u_file.FileName.Split('.');
                        Guid id_pic = Guid.NewGuid();
                        filePath = "wwwroot\\Pictures\\"+id_pic + "." + type_pic[1];
                        file_name = ""  +id_pic + "." + type_pic[1];

                        Console.WriteLine("2 -- -- -- "+ filePath  + " -- " + id_pic );
                        Bitmap myBitmap;
                        using (Stream  fs = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                await u_file.CopyToAsync(fs);
                                Console.WriteLine(fs.Position  + "  ---  - - - -- " +  u_file.Length );
                                while(fs.Position != u_file.Length) {} // ожидание  загрузки изображения

                                myBitmap = new Bitmap(fs);
                                Console.WriteLine(myBitmap.Width + " -+_+-" + myBitmap.Height);
                            }

                        Image img = myBitmap;
                        var im_w = myBitmap.Width;
                        var im_h = myBitmap.Height;

                        if(im_w > im_h)
                        {
                            im_w = im_h;
                           
                        }
                        else
                            im_h = im_w ;
                        //XXX  тут надо нормально пререписать как вырезать изображение :) 
                        
                        img = img.Crop(new Rectangle(0, 0,  im_w,  im_h));
                        img.Save(filePath);
                    }
                    else
                    {
                        return RedirectToAction("UpdateInfo_page", new RouteValueDictionary( 
                                new { controller = "User", action = "UpdateInfo_page"} )); 
                    }
                    db.UseSqlQuary("UPDATE users set first_name =  "+sr.GetScr()+first_name
                            +sr.GetScr()+" ,second_name = "+sr.GetScr()+second_name+sr.GetScr()+"," + 
                            "picture_profile = "+ sr.GetScr()+file_name+sr.GetScr() +
                            " WHERE users.login = "+sr.GetScr()+_login+sr.GetScr() );
                }
                else 
                {
                  
                db.UseSqlQuary("UPDATE users set first_name =  "+sr.GetScr()+first_name
                 +sr.GetScr()+" ,second_name = "+sr.GetScr()+second_name+sr.GetScr()+" " + 
                " WHERE users.login = "+sr.GetScr()+_login+sr.GetScr() );
                }
            }
           
            return RedirectToAction("Profile", new RouteValueDictionary( 
                                new { controller = "User", action = "Profile"} ));
        }

       
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
