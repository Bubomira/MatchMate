﻿using MatchMate.Helpers;
using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MatchMateInfrastructure.Models;

using static MatchMateInfrastructure.DataConstants.CustomClaimsType;
using MatchMate.Filters;

namespace MatchMate.Areas.Matcher.Controllers
{
    public class UserController : BaseUserController
    {
        private readonly IUserInterface _userService;
        private readonly IInterestInterface _interestService;
        private readonly IProfilePictureInterface _profilePictureService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserInterface matchingService,
            IInterestInterface interestInterface,
            IProfilePictureInterface profilePictureInterface,
             UserManager<ApplicationUser> userManager)
        {
            _userService = matchingService;
            _interestService = interestInterface;
            _profilePictureService = profilePictureInterface;
            _userManager = userManager;
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
        [FullyActiveProfileFilter]
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
            if (await _userService.CheckIfUserHasBio(User.Id()))
            {
                return RedirectToAction(nameof(Profile));
            }
            UserBioModel userBioModel = new UserBioModel() { HasBio = false };

            return View("/Areas/Matcher/Views/Shared/UserSetUpProfilePartials/_SetUpBioPartial.cshtml", userBioModel);
        }

        [HttpPost]
        [AttachClaims(HasBio)]
        public async Task<IActionResult> SetUpBio(UserBioModel userBioModel)
        {
            await _userService.AddUserBio(userBioModel.Bio, User.Id());

            if (userBioModel.HasBio)
            {
                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction(nameof(SetUpInterests));
        }
        [HttpGet]
        public async Task<IActionResult> SetUpInterests()
        {
            if (await _interestService.CheckIfUserHasAtLeastXInterests(User.Id(), 3))
            {
                return RedirectToAction(nameof(Profile));
            }

            var interests = await _interestService.GetAllInterestsForCurrentUserAsync(User.Id());
            return View(interests);
        }

        [HttpGet]
        public async Task<IActionResult> SetUpProfilePicture()
        {
            var pfp = await _profilePictureService.GetProfilePictureFromMongoAsync(User.Id());
            if (pfp != null)
            {
                return RedirectToAction(nameof(Profile));
            }
            return View("/Areas/Matcher/Views/Shared/UserSetUpProfilePartials/_SetUpProfilePicturePartial.cshtml", true);
        }

        [HttpPost]
        [AttachClaims(HasPfp)]
        public async Task<IActionResult> SetUpProfilePicture(IFormFile file)
        {
            if (file == null)
            {
                return View();
            }
            string stringFile = FileConverter.ConvertFormFileToString(file);

            await _profilePictureService.SaveProfilePictureToMongoAsync(User.Id(), stringFile);

            return RedirectToAction(nameof(Index), new { pageNumber = 1 });
        }

        [HttpPost]
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
