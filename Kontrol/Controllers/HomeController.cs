using Kontrol.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrol.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string basePath = @"C:\Users\SirPies (Ravi)\Documents\";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewFile(string filename = "")
        {
            var vm = new Kontrol.ViewModels.ViewFile();
            vm.FileFound = false;
            if (filename.Length > 0)
            {
                string path = System.IO.Path.Join(basePath, filename);
                vm.FilePath = path;
                try
                {
                    vm.FileContent = System.IO.File.ReadAllText(path);
                    vm.FileFound = true;
                }
                catch
                {
                    // pass - file was not found, and this was set up above
                }
            }
            else
            {
                vm.FilePath = "";
                vm.FileContent = "";
            }
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
