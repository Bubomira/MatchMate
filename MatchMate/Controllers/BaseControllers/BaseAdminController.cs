using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MatchMate.Controllers.BaseControllers
{
    [Authorize(Roles = "Administrator")]
    public class BaseAdminController : Controller
    {

    }
}
