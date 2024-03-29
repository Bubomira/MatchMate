using MatchMate.Controllers.BaseControllers;
using MatchMate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseUserController
    {
        public HomeController()
        {

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