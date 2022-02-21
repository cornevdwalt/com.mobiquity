using com.mobiquity.packer.Packer;
using com.mobiquity.web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace com.mobiquity.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPacker _packerService;

        public HomeController(ILogger<HomeController> logger, IPacker packerService)
        {
            _logger = logger;
            _packerService = packerService;
        }

        public IActionResult ParseFile()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ParseFile(DataFileViewModel dataFile)
        {
            if (ModelState.IsValid)
            {
                dataFile.ParseResults = _packerService.pack(dataFile.FilePath, true);
                ViewBag.Response = dataFile.ParseResults;
            }

            return View(dataFile);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}