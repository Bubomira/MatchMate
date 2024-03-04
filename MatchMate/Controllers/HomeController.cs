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

        public async  Task<IActionResult> Privacy()
        {
           var profilePicture=await _profilePictureService.GetProfilePictureFromMongoAsync("13");

            return View("Privacy",profilePicture);

        }

        public async Task<IActionResult> Save(IFormFile file)
        {
          string stringFile= FileConverter.ConvertFormFileToString(file);

          await _profilePictureService.SaveProfilePictureToMongoAsync("12", stringFile);

            return RedirectToAction(nameof(Privacy));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { /*RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier */});
        }
    }
}