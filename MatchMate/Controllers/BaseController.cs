using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
