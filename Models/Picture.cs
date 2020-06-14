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

using StudyGroup.Controllers;
namespace StudyGroup.Models
{
    public class Picture
    {

        public enum DownloadCodes { Fine = 0, NotImage = 1}

        
        static bool  CheckTypePicture(string type) => type == "png" ||  type == "jpeg" ||  type == "jpg" ;
        public static async Task<DownloadCodes> Download(Guid idPic, IFormFile photo)
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
                return DownloadCodes.Fine;
            } 
            return DownloadCodes.NotImage; 
        }
    }
} 