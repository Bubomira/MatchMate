using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.BaseControllers
{
    public class BaseAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
