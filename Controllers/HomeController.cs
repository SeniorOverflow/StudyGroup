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
