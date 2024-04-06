using MatchMateInfrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MatchMate.Filters
{
    public class AttachClaims:ResultFilterAttribute
    {
        private readonly string _customClaimType;
        public AttachClaims(string customClaimType)
        {
            _customClaimType = customClaimType;
        }
        public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var httpUser = context.HttpContext.User;
            var _userManager = context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var _signInManager = context.HttpContext.RequestServices.GetService<SignInManager<ApplicationUser>>();

            if (httpUser == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            else
            {
               var user = await _userManager.FindByIdAsync(httpUser.Id());
               var claim = new Claim(_customClaimType, "true", "Boolean");
               await _userManager.AddClaimAsync(user, claim);
               await _signInManager.RefreshSignInAsync(user);
             
               await next();
            }

        }
    }
}
