using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() =>  View());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult LogIn(string login ="", string password ="") 
        {
            var db = new DbConfig();
            var sr = new Screening();
           
            
            if(string.IsNullOrEmpty(login))
            {
                return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "Home", action = "Authorization", ex= 4} ));
            }
            else
            {
                var tmp_data = new List<List<string>>();
                tmp_data.Clear();
                tmp_data = 
                db.GetSqlQuaryData("select id from users where login =  lower(" +sr.GetScr()+login+sr.GetScr()+") "+ 
                                    "AND  password = crypt(" + sr.GetScr() + password + sr.GetScr() + ", password)" );
                if(tmp_data.Count ==0)
                {
                    return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "Home", action = "Authorization", ex= 1} ));

                }
                else
                {

                    if(string.IsNullOrEmpty(password))
                    {
                        return RedirectToAction("Authorization", new RouteValueDictionary( 
                                new { controller = "Home", action = "Authorization", ex= 5} ));
                    }

                    

                    login =  login.ToLower();
                    HttpContext.Session.SetString("login",""+login);
                    HttpContext.Session.SetString("password",""+ password);
                }
                
            }
           
            
            return RedirectToAction("Profile", new RouteValueDictionary( 
                new { controller = "User", action = "Profile"   } ));
            
        }

        public IActionResult Profile() 
        {
            List<List<string>> tmp_data = new List<List<string>>();

            Screening sr  = new Screening();
            DbConfig db = new DbConfig();
            string login = HttpContext.Session.GetString("login");
            
            if(string.IsNullOrEmpty( login))
            {
                return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "Home", action = "Authorization", ex= 0} ));
            }
            else
            {
                tmp_data.Clear();
                tmp_data = 
                db.GetSqlQuaryData("select id from users where login =  lower(" +sr.GetScr()+login+sr.GetScr()+")" );
                int id_user = Convert.ToInt32(tmp_data[0][0]);
           

                var _user = new User();
                _user.is_dev = false;
                var tmp_login = new List<List<string>>();
              
                
                
                var tmp = new List<List<string>>();
                tmp = 
                db.GetSqlQuaryData("SELECT id,login,score,first_name,second_name,bonus_score,picture_profile "+
                                        "FROM users WHERE users.id = "+id_user );
                if(tmp.Count()>0)
                {
                        _user.id_user= Convert.ToInt32(tmp[0][0]);
                        _user.login= Convert.ToString(tmp[0][1]);
                        
                        _user.score= Convert.ToDouble(tmp[0][2]);
                        _user.first_name = tmp[0][3];
                        _user.second_name= tmp[0][4];
                        _user.bonus_score = Convert.ToDouble(tmp[0][5]);
                        _user.picture_profile = tmp[0][6];
                       
                       
                       
                        ViewBag.User_data = _user;
                        
                        List<string> translate_words =  Language_Settings.GetWords(1);
                        ViewBag.Translate_words = translate_words;
                    return View();
                
                }
            }

            return RedirectToAction("Authorization", new RouteValueDictionary( 
                        new { controller = "Home", action = "Authorization", ex= 0} ));
        }
  
        public IActionResult UpdateInfopPage()
        {
            var sr = new Screening();
            var db = new DbConfig();
            var tmp_user = new List<List<string>>();
            tmp_user = 
            db.GetSqlQuaryData("SELECT first_name,second_name,picture_profile FROM users "+ 
                    " WHERE users.login = "+sr.GetScr()+HttpContext.Session.GetString("login")+sr.GetScr());
            var user = new User();
            
            user.first_name = tmp_user[0][0];
            user.second_name =tmp_user[0][1];
            user.picture_profile = tmp_user[0][2];
            ViewBag.User_data = user;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInfo(string first_name, string second_name,  IFormFile u_file = null)
        {
            string filePath = "";
            string file_name="";
            Screening sr = new Screening();
            DbConfig db = new DbConfig();
            
            string _login = HttpContext.Session.GetString("login") ;

            if(_login == "")
            {
                  return RedirectToAction("Authorization", new RouteValueDictionary( 
                                new { controller = "Home ", action = "Profile"} ));

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
                        int im_w = myBitmap.Width;
                        int im_h = myBitmap.Height;

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
