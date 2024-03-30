using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.BaseControllers
{
    [Authorize(Roles = "User")]
    public class BaseUserController : Controller
    {

    }
}
