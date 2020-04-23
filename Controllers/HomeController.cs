using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyGroup.Models;
using Npgsql;
using StudyGroup.Script;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.IO;

namespace StudyGroup.Controllers
{
    public class HomeController : Controller
    {   
        public async Task<IActionResult> Index(int numb_page = 0)
        {
            return await Task.Run(() => View());
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
            return await Task.Run(() => RedirectToAction("Index"));
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
                    var usersId = new List<List<string>>();
                    usersId = db.GetSqlQuaryData("Select id FROM users WHERE login = lower("+sr.GetScr()+_login+sr.GetScr()+")" );
                    if(usersId.Count == 0)
                    {
                        Console.WriteLine("1");
                        var ex = new Regex("^[0-9A-Za-z]{1}[0-9A-Za-z_-]*@{1}[A-Za-z]+[.][A-Za-z]+$");
                        if(ex.IsMatch(_mail))
                        {
                            usersId.Clear();
                            usersId = db.GetSqlQuaryData("select id FROM users  where mail="+sr.GetScr()+_mail +sr.GetScr());
                            if(usersId.Count != 0)
                                return await Task.Run(() => RedirectToAction("Reg",new {type  = 3}));
                            Console.WriteLine("3");
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
        
        [HttpGet]
        public async Task<IActionResult> Privacy()
        {
            return await Task.Run(() => View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return await Task.Run(() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }
    }
}
