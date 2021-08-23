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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewLocation(string path = "")
        {
            if (path.EndsWith(@"\"))
            {
                return RedirectToAction(nameof(ViewDirectory), new { directoryPath = path });
            }
            else
            {
                return RedirectToAction(nameof(ViewFile), new { filePath = path });
            }
        }

        public IActionResult ViewParentDirectory(string path = "")
        {
            string parentPath = path;
            try
            {
                parentPath = System.IO.Directory.GetParent(path).FullName;
            }
            catch (NullReferenceException)
            {
                // If the path is the root of the file system, the above code will throw this error
                // In this case, just let the thing view the current directory
                // (using the default value set at the top of this method)
            }
            return RedirectToAction(nameof(ViewDirectory), new { directoryPath = parentPath });
        }

        public IActionResult ViewFile(string filePath = "")
        {
            var viewModel = new Kontrol.ViewModels.ViewFile();
            viewModel.filePath = filePath;
            viewModel.fileReadState = Helpers.FileReadState.NOT_FOUND;
            try
            {
                viewModel.fileType = Helpers.FileHelpers.FileTypeFromExtension(filePath);
                viewModel.fileContent = System.IO.File.ReadAllBytes(filePath);
                viewModel.fileReadState = Helpers.FileReadState.READ;
            }
            catch (System.IO.FileNotFoundException) { }
            catch (System.IO.DirectoryNotFoundException) { }
            catch (System.UnauthorizedAccessException)
            {
                viewModel.fileReadState = Helpers.FileReadState.PERMISSION_DENIED;
            }
            return View(viewModel);
        }

        public IActionResult ViewDirectory(string directoryPath = "")
        {
            var viewModel = new Kontrol.ViewModels.ViewDirectory();
            viewModel.directoryReadState = Helpers.FileReadState.NOT_FOUND;
            viewModel.directoryPath = directoryPath;
            try
            {
                string[] files = System.IO.Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    viewModel.childFiles.Add(file);
                }
                string[] directories = System.IO.Directory.GetDirectories(directoryPath);
                foreach (var directory in directories)
                {
                    viewModel.childDirectories.Add(directory);
                }
                viewModel.directoryReadState = Helpers.FileReadState.READ;
            }
            catch (System.IO.FileNotFoundException) { }
            catch (System.IO.DirectoryNotFoundException) { }
            catch (System.UnauthorizedAccessException)
            {
                viewModel.directoryReadState = Helpers.FileReadState.PERMISSION_DENIED;
            }
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
