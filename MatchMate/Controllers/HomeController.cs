using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
