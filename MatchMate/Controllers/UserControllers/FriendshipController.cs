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
            var activeFriends = await _friendshipService.GetActiveFriendsAsync(User.Id(), pageNumber - 1);

            activeFriends.TotalPagesCount = Math.Ceiling(((double)activeFriends.TotalFriends / 12));
            activeFriends.NextPage = pageNumber + 1;
            activeFriends.PrevousPage = pageNumber - 1;
            activeFriends.CurrentPage = pageNumber;

            if (activeFriends.TotalPagesCount<pageNumber && pageNumber!=1)
            {
                return RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            foreach (var friend in activeFriends.Friends)
            {
                friend.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(friend.UserId);
            }

            return View(activeFriends);
        }

        [HttpGet]
        public async Task<IActionResult> Pending(int pageNumber)
        {
            var pendingRequests = await _friendshipService.GetPendingRequestsAsync(User.Id(), pageNumber - 1);
           
            if (pendingRequests.Friends.Count == 0 && pageNumber!=1)
            {
                return RedirectToAction(nameof(Pending), new { pageNumber = 1 });
            }

            pendingRequests.TotalPagesCount = Math.Ceiling(((double)pendingRequests.TotalFriends / 12));
            pendingRequests.NextPage = pageNumber + 1;
            pendingRequests.PrevousPage = pageNumber - 1;
            pendingRequests.CurrentPage = pageNumber;

            foreach (var pending in pendingRequests.Friends)
            {
                pending.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(pending.UserId);
            }
            return View(pendingRequests);
        }


        public async Task<IActionResult> SendFriendShipRequest(string id)
        {
            if (!await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(User.Id(), id))
            {
                await _friendshipService.SendFriendRequestAsync(User.Id(), id);
            }
            return RedirectToAction("Index", "User", new { pageNumber = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> Accept(string id)
        {
            if (await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(User.Id(), id))
            {
                await _friendshipService.AcceptFriendRequestAsync(id, User.Id());
            }
            return RedirectToAction(nameof(Index), new { pageNumber = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> Reject(string id)
        {
            if (await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(User.Id(), id))
            {
                await _friendshipService.RejectFriendRequestAsync(id, User.Id());
            }
            return RedirectToAction("Index", "User", new { pageNumber = 1 });
        }

        public async Task<IActionResult> RemoveFriend(string id)
        {
            if (await _friendshipService.CheckIfThereIsARelationShipBetweenUsersAsync(User.Id(), id))
            {
                await _friendshipService.RemoveFriendAsync(id, User.Id());
               return RedirectToAction("Index", "User", new { pageNumber = 1 });
            }

            return RedirectToAction(nameof(Index), new { pageNumber = 1 });
        }

    }
}
