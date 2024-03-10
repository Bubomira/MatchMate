
using MatchMate.Helpers;
using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    public class UserController : Controller
    {
        private readonly IUserInterface _userService;
        private readonly IInterestInterface _interestService;
        private readonly IProfilePictureInterface _profilePictureService;
        public UserController(IUserInterface matchingService,
            IInterestInterface interestInterface,
            IProfilePictureInterface profilePictureInterface)
        {
            _userService = matchingService;
            _interestService = interestInterface;
            _profilePictureService = profilePictureInterface;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetCurrentUserInfo(User.Id());
            user.Interests = await _interestService.GetAllInterestsForCurrentUserAsync(User.Id());
            user.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(User.Id());

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            UserMatchList userPage = new UserMatchList()
            {
                Users = await _userService.GetUsersWithTheSameInterests(User.Id(), pageNumber - 1),
                CurrentPageNumber = pageNumber,
                PrevoiusPageNumber = pageNumber - 1,
                NextPageNumber = pageNumber + 1
            };

            if (userPage.Users.Count() == 0)
            {
                return RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            foreach (var user in userPage.Users)
            {
                user.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(user.UserId);
            }

            return View(userPage);
        }

        [HttpGet]
        public async Task<IActionResult> SetUpBio()
        {
            UserBioModel userBioModel = new UserBioModel() { HasBio = false };

            return View("_SetUpBioPartial", userBioModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SetUpBio(UserBioModel userBioModel)
        {
            await _userService.AddUserBio(userBioModel.Bio, User.Id());

            if (userBioModel.HasBio)
            {
                return RedirectToAction("Profile", "User");
            }

            return RedirectToAction(nameof(SetUpInterests));
        }
        [HttpGet]
        public async Task<IActionResult> SetUpInterests()
        {
            if (await _interestService.CheckIfUserHasAtLeastXInterests(User.Id(), 3))
            {
                return RedirectToAction("Profile", "User");
            }

            var interests = await _interestService.GetAllInterestsForCurrentUserAsync(User.Id());
            return View(interests);
        }

        [HttpGet]
        public async Task<IActionResult> SetUpProfilePicture()
        {
            return View("_SetUpProfilePicturePartial",true);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> SetUpProfilePicture(IFormFile file)
        {
            if (file == null)
            {
                return View();
            }
            string stringFile = FileConverter.ConvertFormFileToString(file);

            await _profilePictureService.SaveProfilePictureToMongoAsync(User.Id(), stringFile);

            return RedirectToAction(nameof(Index), new {pageNumber=1});
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile file)
        {
            if (file != null)
            {
                string stringFile = FileConverter.ConvertFormFileToString(file);
                await _profilePictureService.ChangeProfilePictureMongoAsync(User.Id(), stringFile);
            }

            return RedirectToAction(nameof(Profile));
        }
    }
}
