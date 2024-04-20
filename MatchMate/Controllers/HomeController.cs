using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
