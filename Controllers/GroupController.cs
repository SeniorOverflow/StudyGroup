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
using Microsoft.AspNetCore.Routing;
using System.Drawing;


namespace StudyGroup.Controllers
{
    public class GroupController : Controller
    {   

        [HttpGet]
        public async Task<IActionResult> CreateGroup()
        {
            return await Task.Run(() => View());
        } 
       


        private Guid GetIdForPic()
        {
            var db = new DbConfig();
            var sr = new Screening();
            var NewIdPic = Guid.NewGuid();
            while(true)
            {
                 var sqlQuary = $@"  SELECT guid 
                                    From Pictures 
                                WHERE guid = {sr.GetScr() + NewIdPic + sr.GetScr()}";
                var picId = db.GetSqlQuaryData(sqlQuary);

                if(picId.Count() > 0)
                    NewIdPic = Guid.NewGuid();
                else
                    break; 
            }
            return NewIdPic;
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateGroup(string title, string description, IFormFile  photoGroup = null)
        {
            
            var idPic = GetIdForPic();
            var downloadCode = await Picture.Download(idPic, photoGroup);
            if(downloadCode == Picture.DownloadCodes.Fine)
            {
                var db = new DbConfig();
                var sr = new Screening();
                var login = 
                    HttpContext.Session.GetString("login") ;
                var userId = UserModel.GetId(login);

                var photoType = sr.GetScr() +  photoGroup.FileName.Split(".").Last() + sr.GetScr();
                var sqlQuaryCreateGroup = $@"
                    INSERT INTO groups(
                        title, description, id_pic , id_founder)
                    VALUES ({sr.GetScr() +title +sr.GetScr()}, 
                            {sr.GetScr() + description + sr.GetScr()}, 
                            {sr.GetScr() + idPic + sr.GetScr() } ,{userId}); ";
               
                var sqlQuaryCreatePicture = $@"
                    INSERT INTO pictures(guid, type_pic)
                    VALUES ({sr.GetScr() + idPic + sr.GetScr() }, {photoType});";

                var sqlQuaryGetIdGroup = $@"
                    SELECT id 
                        FROM groups 
                    WHERE id_founder = {userId}  
                    ORDER BY id DESC
                    LIMIT 1 ;";
                    
                db.UseSqlQuary(sqlQuaryCreatePicture);
                db.UseSqlQuary(sqlQuaryCreateGroup);
                var groupData = db.GetSqlQuaryData(sqlQuaryGetIdGroup);
                var idGroup = -1;
                foreach(var item in groupData)
                    idGroup = Convert.ToInt32(item[0]);


                return await Task.Run(() => RedirectToAction("ShowGroup", new RouteValueDictionary( 
                            new { controller = "Group", action = "ShowGroup", idGroup = idGroup } )));
            }
            return await Task.Run(() => RedirectToAction("Index", new RouteValueDictionary( 
                            new { controller = "Home", action = "Index"} )));
        } 

        public async Task<IActionResult> ShowGroup(int idGroup)
        {
            var db = new DbConfig();
            var sqlQuaryShowGroup = $@"
                 SELECT id, title, description, id_founder, pictures.guid , type_pic 
                    FROM groups inner join pictures on
                        groups.id_pic = pictures.guid  WHERE groups.id = {idGroup}
            ";
            GroupModel group = new GroupModel();
            var idFounder = -1;
            foreach(var item in db.GetSqlQuaryData(sqlQuaryShowGroup))
            {
                idFounder = Int32.Parse(item[3]);
                group  = new GroupModel(item[4], item[5]);
                group.idGroup = Int32.Parse(item[0]);
                group.title = item[1];
                group.description = item[2];
            }
            // тут надо доделать возможности пользователя 
            // 17.06.2020 Igor Popov


            var sqlQuaryGetPostGroup = $@"
                SELECT id, title, description, date, id_file 
                    FROM  group_post 
                WHERE id_group = {idGroup}
            ";
            var groupPosts = db.GetSqlQuaryData(sqlQuaryGetPostGroup);
            var posts = new List<PostModel>();

            foreach(var item in groupPosts)
            {
                var post = new PostModel();
                post.id = Int32.Parse(item[0]);
                post.title = item[1];
                post.text = item[2];
                post.date = item[3];
                posts.Add(post);
                
            }
            ViewBag.Posts = posts;
            ViewBag.Group = group;
            
            return await Task.Run(() => View());
        } 

        [HttpGet]
        public async Task<IActionResult> AddMaterial()
        {
            return await Task.Run(() => View());
        } 
        [HttpPost]
        public async Task<IActionResult> AddMaterial(string name)
        {
            return await Task.Run(() => View());
        } 

        
        [HttpPost]
        public async Task<IActionResult> AddPost(int idGroup, string title, string text , IFormFile file = null )
        {
            //here need check (is user group )
            //add change to upload file with some material .doc  ect
            // here a early version. Please if u want  upgrade the func  u can do it :)
            // Last date edit 21.06.2020 
            
            var db = new DbConfig();
            var sr = new Screening();
            
            var sqlQuaryAddPost = $@"
               INSERT INTO public.group_post( id_group, title, description)
	            VALUES ( {sr.GetScr() + idGroup + sr.GetScr()}, 
                         {sr.GetScr() + title + sr.GetScr()},
                         {sr.GetScr() + text + sr.GetScr()});";
            db.UseSqlQuary(sqlQuaryAddPost);

            return await Task.Run(() => RedirectToAction("ShowGroup", new RouteValueDictionary( 
                            new { controller = "Group", action = "ShowGroup", idGroup = idGroup } )));
        } 

        [HttpGet]
        public async Task<IActionResult> AddHomeWork()
        {
            return await Task.Run(() => View());
        } 
        [HttpPost]
        public async Task<IActionResult> AddHomeWork(string title)
        {
            return await Task.Run(() => View());
        }
        
        [HttpGet]
        public async Task<IActionResult> ShowForum()
        {
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            return await Task.Run(() => View());
        } 
        [HttpPost]
        public async Task<IActionResult> AddQuestion(string title)
        {
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<IActionResult> AddTest()
        {
            return await Task.Run(() => View());
        } 

        [HttpPost]
        public async Task<IActionResult> AddTest(string title)
        {
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<IActionResult> ShowQuestion()
        {
            return await Task.Run(() => View());
        }

        [HttpPost]
        public async Task<IActionResult> AnswerOnQuestion(int idQuestion)
        {
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<IActionResult> ShowGroupAssessments()
        {
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<IActionResult> SearchGroup(string name = null)
        {
            var  sr = new Screening();
            var db = new DbConfig();
            var sqlQuarySelectGroupByName = "";
            if(string.IsNullOrEmpty(name))
                sqlQuarySelectGroupByName = $@"
                SELECT id, title, description, pictures.guid , type_pic 
                    FROM groups inner join pictures on
                        groups.id_pic = pictures.guid 
                ";
            else
                sqlQuarySelectGroupByName = $@"
                SELECT id, title, description, pictures.guid , type_pic 
                    FROM groups inner join pictures on
                        groups.id_pic = pictures.guid  WHERE title = {sr.GetScr() + name + sr.GetScr()}
                ";
            var groups = new List<GroupModel>();
            var groupsData = db.GetSqlQuaryData(sqlQuarySelectGroupByName);


            foreach(var item in groupsData)
                {
                    var group = new GroupModel(item[3], item[4]);
                    group.idGroup = Int32.Parse(item[0]);
                    group.title = item[1];
                    group.description = item[2];
                    groups.Add(group);
                }
            ViewBag.Groups = groups;
            ViewBag.SearchName = name;
            return await Task.Run(() => View());
        }
        
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return await Task.Run(() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }
    }
}
