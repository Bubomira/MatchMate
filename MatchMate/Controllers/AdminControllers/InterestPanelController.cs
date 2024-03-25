using MatchMateCore.Dtos.InterestViewModels.AdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.AdminControllers
{
    [Authorize(Roles = "Administrator")]
    public class InterestPanelController : BaseController
    {
        private readonly IAdminInterestInterface _adminInterestService;
        private readonly IInterestInterface _interestService;
        public InterestPanelController(IAdminInterestInterface adminInterestInterface,
            IInterestInterface interestInterface)
        {
            _adminInterestService = adminInterestInterface;
            _interestService = interestInterface;
        }
        public async Task<IActionResult> Index(int pageNumber)
        {
            InterestPanelList interestPanelModel = new InterestPanelList(pageNumber);

            interestPanelModel.Interests = await _adminInterestService.GetAllInterestsAsync(interestPanelModel);

            if (interestPanelModel.Interests.Count == 0)
            {
                return RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            interestPanelModel.TotalPagesCount = Math.Ceiling((double)interestPanelModel.TotalInterestsCount / InterestPanelList.CountOnPage);

            return View(interestPanelModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(InterestPostFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminInterestService.CheckIfThereIsAnInterestByNameAsync(model.Name.ToLower()))
                {
                    await _adminInterestService.AddNewInterestAsync(model);
                }
            }

            return RedirectToAction(nameof(Index), new { pageNumber = 1 });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InterestEditFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminInterestService.CheckIfThereIsAnInterestByNameAsync(model.Name.ToLower())
                    || await _interestService.CheckIfInterestExists(model.Id))
                {
                    await _adminInterestService.EditInterestAsync(model);
                }
            }

            return RedirectToAction(nameof(Index), new { pageNumber = model.CurrentPage });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(await _interestService.CheckIfInterestExists(id) && await _adminInterestService.CheckIfThereAreAtLeastThreeInterestsAsync())
            {
                await _adminInterestService.DeleteInterestAsync(id);
            }
            return RedirectToAction(nameof(Index), new { pageNumber = 1 });
        }

    }
}
