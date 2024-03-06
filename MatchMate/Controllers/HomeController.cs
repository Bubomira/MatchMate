using MatchMate.Helpers;
using MatchMate.Models;
using MatchMateCore.Interfaces.MongoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MatchMate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProfilePictureInterface _profilePictureService;

        public HomeController(ILogger<HomeController> logger
            , IProfilePictureInterface profilePictureInterface)
        {
            _logger = logger;
            _profilePictureService = profilePictureInterface;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { /*RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier */});
        }
    }
}