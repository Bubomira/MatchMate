using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MatchMate.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BaseAdminController : Controller
    {

    }
}
