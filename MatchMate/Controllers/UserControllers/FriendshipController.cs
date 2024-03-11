using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.UserControllers
{
    public class FriendshipController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
