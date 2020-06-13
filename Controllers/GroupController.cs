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
        bool CheckTypePicture(string type) => type == "png" ||  type == "jpeg" ||  type == "jpg" ;
        public async Task<IActionResult> ReadUserImage(Guid idPic, IFormFile photo)
        {
          
            var type = photo.FileName.Split('.').Last();
            var NewNamePicture = "" + idPic + "." + type;
            var filePath = "wwwroot\\Pictures\\"+ NewNamePicture;
            
            if(CheckTypePicture(type.ToLower()) && photo.Length > 0)
            {
                using (var s = new FileStream(filePath,FileMode.OpenOrCreate))
                {
                    await photo.CopyToAsync(s);
                }
            } 
            return Ok(new {photo.Length});
        } 

        [HttpPost]
        public async Task<IActionResult> CreateGroup(string title, string description, IFormFile  photoGroup = null)
        {
            Console.WriteLine(photoGroup.Length);
            var idPic = new Guid();
            await ReadUserImage(idPic, photoGroup);
         
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
        public async Task<IActionResult> AddGroupPost()
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
