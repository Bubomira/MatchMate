using MatchMateCore.Dtos.AuthenticationViewModels;
using MatchMateInfrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

using static MatchMateInfrastructure.DataConstants;

namespace MatchMate.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<AuthenticationController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            RegisterModel registerModel = new RegisterModel();
            registerModel.ReturnUrl = returnUrl;
            registerModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            return View(registerModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            registerModel.ReturnUrl ??= Url.Content("~/");
            registerModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                var birthdate = DateTime.ParseExact(registerModel.Birthday, BirthdateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

                user.Birthday = birthdate;

                await _userStore.SetUserNameAsync(user, registerModel.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("SetUpBio", "User");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerModel);
        }

        [HttpGet]
        public Task<IActionResult> Login()
        {
            throw new NotImplementedException();

        }

        [HttpPost]
        public Task<IActionResult> Login(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        [HttpGet]

        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
