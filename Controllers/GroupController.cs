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
using static StudyGroup.Models.Picture;

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
            var NewIdPic = Guid.NewGuid();
            while(true)
            {
                 var sqlQuary = $@"  SELECT guid 
                                    From Pictures 
                                WHERE guid = {NewIdPic}";
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
            if(downloadCode == DownloadCodes.Fine)
            {
                var db = new DbConfig();
                var sr = new Screening();
                var photoType =  sr.GetScr() + ""+ photoGroup.ContentType + sr.GetScr();
                var sqlQuaryCreateGroup = $@"
                    INSERT INTO public.groups(
                        title, description, id_pic)
                    VALUES ({title}, {description}, {idPic}); ";

                var sqlQuaryCreatePicture = $@"
                    INSERT INTO public.pictures(guid, type_pic)
                    VALUES ({idPic}, {photoType});";


                var sqlQuarySetUserAdminGroup = $@"
                    INSERT INTO 
                ";
                 // memberships
                 // group_roles

                db.UseSqlQuary(sqlQuaryCreatePicture);
                db.UseSqlQuary(sqlQuaryCreateGroup);
                
                return await Task.Run(() => RedirectToAction("Index", new RouteValueDictionary( 
                            new { controller = "Home", action = "Index"} )));
            }
            return await Task.Run(() => RedirectToAction("Index", new RouteValueDictionary( 
                            new { controller = "Home", action = "Index"} )));
        } 

        public async Task<IActionResult> ShowGroup(int idGroup)
        {
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

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {
            return await Task.Run(() => View());
        } 
        [HttpPost]
        public async Task<IActionResult> AddPost(string title)
        {
            return await Task.Run(() => View());
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
            return await Task.Run(() => View());
        }
        
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return await Task.Run(() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
        }
    }
}
