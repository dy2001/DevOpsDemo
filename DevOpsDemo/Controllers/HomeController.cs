using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DevOpsDemo.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DevOpsDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _host;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment Host)
        {
            _logger = logger;
            _host = Host;
        }

        public IActionResult Index()
        {
            var Files = Directory.GetFiles(_host.WebRootPath + @"/images/");
            for (int i = 0; i < Files.Count(); i++)
            {
                Files[i] = "/images/" + Files[i].Split('/')[Files[i].Split('/').Length - 1];
            }
            ViewBag.Files = Files;

            return View();
        }
		//file
        public IActionResult File()
        {
            return View();
        }
        public IActionResult FilePost(IFormCollection formCollection)
        {
            var file = formCollection.Files;
            if (file.Count > 0)
            {
                string webRootPath = _host.WebRootPath;
                string imagePath = $"/images/";
                string absolutePath = webRootPath + imagePath;
                if (!Directory.Exists(absolutePath))
                    Directory.CreateDirectory(absolutePath);
                var filePath = absolutePath + "3.png";
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file[0].CopyTo(stream);
                }
            }
            return Redirect("~/Home");
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
