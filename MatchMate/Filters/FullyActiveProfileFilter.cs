using MatchMate.Areas.Matcher.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

using static MatchMateInfrastructure.DataConstants.CustomClaimsType;

namespace MatchMate.Filters
{
    public class FullyActiveProfileFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var user = context.HttpContext.User;
            var controller = (BaseUserController)context.Controller;

            if(user == null || controller==null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }

            if (user.FindFirstValue(HasBio)==null || !bool.Parse(user.FindFirstValue(HasBio)))
            {
                context.Result = controller.RedirectToAction("SetUpBio","User");
            }

            if (user.FindFirstValue(HasInterests) == null || !bool.Parse(user.FindFirstValue(HasInterests)))
            {
                context.Result = controller.RedirectToAction("SetUpInterests", "User");
            }

            if (user.FindFirstValue(HasPfp) == null || !bool.Parse(user.FindFirstValue(HasPfp)))
            {
                context.Result = controller.RedirectToAction("SetUpProfilePicture", "User");
            }
        }
    }
}
