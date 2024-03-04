using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Dtos.InterestViewModels;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    [Authorize]
    public class InterestController : Controller
    {
        private readonly IInterestInterface _interestInterface;
        public InterestController(IInterestInterface interestInterface)
        {
            _interestInterface = interestInterface;
        }
        public async Task<IActionResult> Index()
        {
            var interests = await _interestInterface.GetAllInterestsForCurrentUserAsync(GetUserId());

            return View(interests);
        }

        public async Task<IActionResult> Add(int id)
        {
            if (!await _interestInterface.CheckIfInterestIsAttachedToUser(id, GetUserId()))
            {
                await _interestInterface.AddInterestToUserCollectionAsync(id,GetUserId());
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            if (await _interestInterface.CheckIfInterestIsAttachedToUser(id, GetUserId()) &&
                await _interestInterface.CheckIfUserHasAtLeastThreeInterests(GetUserId()))
            {
                await _interestInterface.RemoveInterestFromUserCollectionAsync(id, GetUserId());

            }
            return RedirectToAction(nameof(Index));
        }

        private string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier).Value;

    }
}
