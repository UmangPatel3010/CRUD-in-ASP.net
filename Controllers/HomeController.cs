using Microsoft.AspNetCore.Mvc;
using StaticCRUD.Models;
using System.Diagnostics;

namespace NiceAdminDemo_3.Controllers
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
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Error404()
        {
            return View();
        }
        public IActionResult BlankPage()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult TableData()
        {
            return View();
        }
        public IActionResult TableGeneral()
        {
            return View();
        }
        public IActionResult IconsBootstrap()
        {
            return View();
        }
        public IActionResult IconsBox()
        {
            return View();
        }
        public IActionResult IconsRemix()
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
