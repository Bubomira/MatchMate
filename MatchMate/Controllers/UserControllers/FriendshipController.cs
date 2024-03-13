using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    public class FriendshipController : BaseController
    {
        private readonly IFriendshipInterface _friendshipService;
        private readonly IProfilePictureInterface _profilePictureService;
        public FriendshipController(IFriendshipInterface friendshipInterface
            , IProfilePictureInterface profilePictureInterface)
        {
            _friendshipService = friendshipInterface;
            _profilePictureService = profilePictureInterface;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            var friendsList = new UserMatchList()
            {
                PrevoiusPageNumber = pageNumber -= 1,
                CurrentPageNumber = pageNumber,
                NextPageNumber = pageNumber += 1,
                Users = await _friendshipService.GetActiveFriendsAsync(User.Id(), pageNumber - 1)
            };

            if (friendsList.Users.Count == 0)
            {
                RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            foreach (var friend in friendsList.Users)
            {
                friend.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(friend.UserId);
            }

            return View(friendsList);
        }

        [HttpGet]
        public async Task<IActionResult> Pending(int pageNumber)
        {
            var pendingList = new UserMatchList()
            {
                PrevoiusPageNumber = pageNumber -= 1,
                CurrentPageNumber = pageNumber,
                NextPageNumber = pageNumber += 1,
                Users = await _friendshipService.ViewPendingRequestsAsync(User.Id(), pageNumber - 1)
            };

            if (pendingList.Users.Count == 0)
            {
                RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            foreach (var pending in pendingList.Users)
            {
                pending.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(pending.UserId);
            }
            return View(pendingList);
        }


        public async Task<IActionResult> SendFriendShipRequest(string id)
        {
            if (await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(User.Id(), id))
            {
                return RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            await _friendshipService.SendFriendRequestAsync(User.Id(), id);

            return RedirectToAction(nameof(Pending), new { pageNumber = 1 });
        }



    }
}
