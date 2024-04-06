using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Areas.Matcher.Controllers
{
    [Authorize(Roles = "User")]
    [Area("Matcher")]
    public class BaseUserController : Controller
    {

    }
}
