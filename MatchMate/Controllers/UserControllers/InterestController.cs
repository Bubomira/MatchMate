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
            var interests = await _interestInterface.GetAllInterestsForCurrentUserAsync(User.Id());

            return View(interests);
        }

        public async Task<IActionResult> Add(int id)
        {
            if (!await _interestInterface.CheckIfInterestIsAttachedToUser(id, User.Id())
                && await _interestInterface.CheckIfInterestExists(id))
            {
                await _interestInterface.AddInterestToUserCollectionAsync(id, User.Id());
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            if (await _interestInterface.CheckIfInterestIsAttachedToUser(id, User.Id()) &&
                await _interestInterface.CheckIfUserHasAtLeastThreeInterests(User.Id()))
            {
                await _interestInterface.RemoveInterestFromUserCollectionAsync(id, User.Id());

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
