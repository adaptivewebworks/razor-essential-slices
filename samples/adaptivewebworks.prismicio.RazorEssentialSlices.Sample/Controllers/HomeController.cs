using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using adaptivewebworks.prismicio.RazorEssentialSlices.Sample.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace adaptivewebworks.prismicio.RazorEssentialSlices.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var sep = Path.DirectorySeparatorChar;
            var path = $"{Directory.GetCurrentDirectory()}{sep}fixtures{sep}document.json";
            var fileContents = await System.IO.File.ReadAllTextAsync(path);
            var token = JToken.Parse(fileContents);
            var document = prismic.Document.Parse(token);
            return View(document);
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
