using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    public class InterestController : BaseController
    {
        private readonly IInterestInterface _interestInterface;
        public InterestController(IInterestInterface interestInterface)
        {
            _interestInterface = interestInterface;
        }

        public async Task<IActionResult> Add(int id)
        {
            if (!await _interestInterface.CheckIfInterestIsAttachedToUser(id, User.Id())
                && await _interestInterface.CheckIfInterestExists(id))
            {
                await _interestInterface.AddInterestToUserCollectionAsync(id, User.Id());
            }
            return RedirectToAction("Profile","User");
        }

        [HttpPost]
        public async Task<IActionResult> AddMany(IFormCollection formCollection)
        {
            if (formCollection.Keys.Count-1<3)
            {
                return RedirectToAction("SetUpInterests","User");
            }
            for (int i = 0; i < formCollection.Keys.Count-1; i++)
            {
                int interestId = int.Parse(formCollection.Keys.ToArray()[i]);
                await _interestInterface.AddInterestToUserCollectionAsync(interestId, User.Id());
            }
            return RedirectToAction("SetUpProfilePicture", "User");
        }

        public async Task<IActionResult> Remove(int id)
        {
            if (await _interestInterface.CheckIfInterestIsAttachedToUser(id, User.Id()) &&
                await _interestInterface.CheckIfUserHasAtLeastXInterests(User.Id(),4))
            {
                await _interestInterface.RemoveInterestFromUserCollectionAsync(id, User.Id());

            }
            return RedirectToAction("Profile", "User");
        }

    }
}
