using MatchMateCore.Dtos.BlockedUserModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Areas.Matcher.Controllers
{
    public class BlockController : BaseUserController
    {
        private readonly IUserBlockInterface _blockService;
        private readonly IProfilePictureInterface _profilePictureService;
        public BlockController(IUserBlockInterface userBlockInterface,
            IProfilePictureInterface profilePictureInterface)
        {
            _blockService = userBlockInterface;
            _profilePictureService = profilePictureInterface;
        }
        public async Task<IActionResult> Index(int pageNumber)
        {
            BlockedUserList blockedUserList = new BlockedUserList(pageNumber);
            blockedUserList.BlockedUsers = await _blockService.ShowAllBlockedUsers(blockedUserList, User.Id());

            foreach (var blockedUser in blockedUserList.BlockedUsers)
            {
                blockedUser.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(blockedUser.Id);
            }

            blockedUserList.TotalPages = Math.Ceiling((double)blockedUserList.TotalBlockedUsersCount / BlockedUserList.BlockedUsersOnPage);

            return View(blockedUserList);
        }

        public async Task<IActionResult> BlockUser(string toBeBlockedUserId)
        {
            if (!await _blockService.CheckIfUserHasBeenBlocked(User.Id(), toBeBlockedUserId))
            {
                await _blockService.BlockUser(toBeBlockedUserId, User.Id());
            }

            return RedirectToAction("Index", "User", new { pageNumber = 1 });
        }

        public async Task<IActionResult> UnblockUser(string toBeUnblockedUserId)
        {
            if (await _blockService.CheckIfUserHasBeenBlocked(User.Id(), toBeUnblockedUserId))
            {
                await _blockService.UnblockUser(toBeUnblockedUserId, User.Id());
            }

            return RedirectToAction("Index", "User", new { pageNumber = 1 });
        }
    }
}
