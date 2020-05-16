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
        public  IViewComponentResult Invoke()
        {
            var sr = new Screening();
            var user = new User();
            var db = new DbConfig();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("login")))
            {
                user.types_user.Clear(); 
                user.types_user.Add(-1);
            }
            else
            {
                var _login = HttpContext.Session.GetString("login");
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("roles")))
                {
                    
                    var sqlCommand = 
                    "SELECT roles.id " +
                                            " from " +
                                                " users inner join user_role on users.id = user_role.id_user " +
                                                " inner join roles on user_role.id_role = roles.id " +
                                            " WHERE login = "+sr.GetScr() + _login +sr.GetScr();
                    var rolesId = new List<string>();

                    foreach (var role in db.GetSqlQuaryData(sqlCommand))
                    {
                        rolesId.Add(role[0]);
                    }
                   

                    if(rolesId.Count>0)
                    {
                        var role_ids ="0";
                        foreach(var str in rolesId)
                            role_ids = role_ids  + "," + str[0];

                        HttpContext.Session.SetString("roles", role_ids); //Save Roles

                        var _roles = new List<int>();
                        var str_roles = HttpContext.Session.GetString("roles").Split(","); //Load Roles
                        
                        foreach(var role in str_roles)
                        {
                            var _role = Convert.ToInt32(role);
                            _roles.Add(_role);
                        }
                        
                        user.types_user = _roles;
                    }
                    else
                    {
                        user.types_user.Clear();
                        user.types_user.Add(-1);
                    }
                }
                
                var tmpData = 
                    db.GetSqlQuaryData("select id from users where login =" +sr.GetScr()+_login +sr.GetScr()).First();
                if(tmpData.Count > 0)
                {
                    var id_user = Convert.ToInt32(tmpData[0][0]);
                    user.id_user = id_user;

                   
                    tmpData = db.GetSqlQuaryData("select count(id) from  shopping_cart where id_user = " + user.id_user).First();
                        user.quantity_cart_places =Convert.ToInt32(tmpData[0][0]);

                    var _roles = new List<int>();
                    var str_roles = HttpContext.Session.GetString("roles").Split(","); //Load Roles
                    

                    if(str_roles.Length > 2)
                    {
                        _roles.Add(1);
                        _roles.Add(2);
                    }
                    if(str_roles.Length == 2)
                    {
                        _roles.Add(1);
                    }
                    
                    user.types_user = _roles;
                }
                else
                {
                    user.types_user.Clear();
                    user.types_user.Add(-1);
                }
            }
                var buttons_names = new List<string>();
                buttons_names = Language_Settings.GetWords(1);
                ViewBag.Bn_names = buttons_names;
                ViewBag.User = user;

                var search_product_name = HttpContext.Session.GetString("search_porduct_name");
                ViewBag.Search_product_name = search_product_name;
            
            return View();
        }
    }
}